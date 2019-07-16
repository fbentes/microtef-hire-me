using Newtonsoft.Json;
using StonePayments.Business.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Input;

namespace StonePayments.Business.Commands
{
    /// <summary>
    /// Classe responsável para solicitar do servidor a lista de todas as transações efetuadas.
    /// </summary>
    public class GetTransactionsCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly ITransactionViewModel transactionViewModel;

        public GetTransactionsCommand(ITransactionViewModel transactionViewModel)
        {
            this.transactionViewModel = transactionViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        private void getTransactions()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(StonePaymentResource.Server);

                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("stone/transactions").Result;

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var result = readTask.Result;

                    transactionViewModel.AllTransactionModelList =
                        JsonConvert.DeserializeObject<ObservableCollection<TransactionModel>>(result);

                }
                else
                {
                    transactionViewModel.GetTransactionsViewObservable.SendResultMessage("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                }
            }
        }
        public void Execute(object parameter)
        {
            transactionViewModel.GetTransactionsViewObservable.Show();

            try
            {
                transactionViewModel.GetTransactionsViewObservable.StartProcess();

                getTransactions();
            }
            finally
            {
                transactionViewModel.GetTransactionsViewObservable.EndProcess();
            }
        }
    }
}
