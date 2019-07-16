using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using LightInject;
using StonePayments.Business;
using StonePayments.Server.Services;

namespace StonePayments.Server.Controllers
{
    public class TransactionController : ApiController, ITransactionController
    {
        [Inject]
        public ITransactionService TransactionService { get; set; }


        /// <summary>
        /// Retorna a lista de todas as transações efetuadas.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("stone/transactions")]
        public async Task<IHttpActionResult> GetTransactions()
        {
            var transactions = await TransactionService.GetTransactions();

            return Ok<List<TransactionModel>>(transactions);
        }

        /// <summary>
        /// Retorna a lista de transações efetuadas por número do cartão.
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("stone/transactions/{cardNumber}")]
        public async Task<IHttpActionResult> GetTransactions(long cardNumber)
        {
            var transactions = await TransactionService.GetTransactions(cardNumber);

            return Ok<List<TransactionModel>>(transactions);
        }


        /// <summary>
        /// Envia uma instanção da transação para ser persistida no banco de dados.
        /// </summary>
        /// <param name="transactionModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("stone/sendTransaction")]
        public async Task<IHttpActionResult> SendTransaction([FromBody]TransactionModel transactionModel)
        {
            try
            {
                List<TransactionModel> transactionModelList  = 
                    await TransactionService.SendTransaction(transactionModel);

                return Ok<List<TransactionModel>>(transactionModelList);
            }
            catch (Exception E)
            {
                return Ok<string>(E.Message);
            }
        }
    }
}
