using StonePayments.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using StonePayments.Util;

namespace StonePayments.Server.Repository
{
    /// <summary>
    /// Classe responsável por todas as transações de cartão com o banco de dados.
    /// </summary>
    public class TransactionRepository : ITransactionRepository
    {
        /// <summary>
        /// Método responsável para enviar uma transação para o servidor, validando o número do cartão
        /// e a senha informada pelo cliente.
        /// </summary>
        /// <param name="transactionModel">Objeto enviado pelo cliente com os parâmetros setados da 
        /// transação para o servidor.</param>
        /// <returns>Após enviar uma transação com sucesso, retorna a lista de transações efetuadas.</returns>
        public async Task<List<TransactionModel>> SendTransaction(TransactionModel transactionModel)
        {
            using (var context = new StonePaymentsEntitiesCustom())
            {
                var trans = context.Database.BeginTransaction();

                try
                {
                    // A aplicação cliente informa apenas o CarnNumber, então é necessário buscar 
                    // a instância do Card para setar seu Id para a entidade Transaction e validar 
                    // a senha do cartão se está correta.

                    Card card = await (from c in context.Cards
                                           where c.Number == transactionModel.CardNumber
                                           select c).FirstOrDefaultAsync<Card>();

                        if(card == null)
                        {
                            throw new SendTransactionException(StonePaymentResource.CardNotFound);
                        }

                        bool hasPassword = bool.Parse(card.HasPassword);

                        if (hasPassword)
                        {
                            string passWordDecrypted = Cryptography.Decrypt(card.Password.Trim(), KeyStringCryptography.VALUE);

                            if (passWordDecrypted != transactionModel.Password)
                            {
                                throw new SendTransactionException(StonePaymentResource.PasswordInvalid);
                            }
                        }
    
                        // Pesquisa o portador do cartão para avaliar seu limite de crédito para a transação.

                        var customer = await (from c in context.Customers
                                              where c.Id == card.Customer
                                              select c).FirstOrDefaultAsync<Customer>();

                        if(customer != null)
                        {
                            if(customer.CreditLimit < transactionModel.Amount.Value)
                            {
                                throw new SendTransactionException(StonePaymentResource.CustomerHasNoAvailableBalance);
                            }

                            // Reduz do seu limite de crédito o valor informado na transação.

                            customer.CreditLimit -= transactionModel.Amount.Value;
                        }
                        else
                        {
                            throw new SendTransactionException(StonePaymentResource.CustomerNotFound);
                        }

                        var transaction = new Transaction
                        {
                            Id = Guid.NewGuid(),
                            Amount = transactionModel.Amount.Value,
                            Card = card.Id,
                            Number = transactionModel.Number.Value,
                            Type = transactionModel.Type.ToString()
                        };

                        context.Transactions.Add(transaction);

                        await context.SaveChangesAsync();

                        trans.Commit();

                        // Após inserir um novo registro de transação, retorna a lista de transações feitas
                        // para o cliente pelo seu cartão, evitando assim, mais uma chamada remota para obter 
                        // essa lista e exibí-la na tela.

                        return await GetTransactions(transactionModel.CardNumber);
                }
                catch (Exception E)
                {
                    trans.Rollback();

                    throw new SendTransactionException(E.Message);
                }
            }
        }

        /// <summary>
        /// Retorna a lista de transações efetuadas por cartão, ou todas as transações.
        /// </summary>
        /// <returns>Lista de objetos Transaction</returns>
        public async Task<List<TransactionModel>> GetTransactions(long? cardNumber = null)
        {
            using (var context = new StonePaymentsEntitiesCustom())
            {
                var results =  ( from t in 
                                      await (
                                         from t0 in context.Transactions
                                         where cardNumber != null && t0.Card1.Number == cardNumber ||
                                               cardNumber == null
                                         orderby t0.Card1.Number
                                         select t0
                                       ).ToListAsync<Transaction>()
                                     select new TransactionModel()
                                     {
                                         Id = t.Id,
                                         Amount = t.Amount,
                                         Number = t.Number,
                                         Type = (TransactionType)Enum.Parse(typeof(TransactionType), t.Type, true),
                                         Card = new CardModel
                                         {
                                             Id = t.Card1.Id,
                                             CardBrand = (CardBrand)Enum.Parse(typeof(CardBrand), t.Card1.CardBrand, true),
                                             Customer = new CustomerModel
                                             {
                                                 Id = t.Card1.Customer1.Id,
                                                 Name = t.Card1.Customer1.Nome,
                                                 CreditLimit = t.Card1.Customer1.CreditLimit
                                             },
                                             ExpirationDate = t.Card1.ExpirationDate,
                                             HasPassword = bool.Parse(t.Card1.HasPassword),
                                             Number = t.Card1.Number,
                                             Password = t.Card1.Password,
                                             Type = (CardType)Enum.Parse(typeof(CardType), t.Card1.Type, true)
                                         },
                                         
                                     }).ToList<TransactionModel>();

                return results;
            }
        }
    }
}