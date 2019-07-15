using Newtonsoft.Json;
using StonePayments.Business;
using StonePayments.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StonePayments.Client.Commands
{
    public class GetTransactionsCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly IGetTransactionsViewModel getTransactionsViewModel;

        public GetTransactionsCommand(IGetTransactionsViewModel getTransactionsViewModel)
        {
            this.getTransactionsViewModel = getTransactionsViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        private void getTransactions()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(StonePaymentServerResource.Server);

                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("stone / sendTransaction").Result;

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var result = readTask.Result;

                    getTransactionsViewModel.TransactionModelList =
                        JsonConvert.DeserializeObject<ObservableCollection<TransactionModel>>(result);

                }

            }
        }
        public void Execute(object parameter)
        {
            getTransactions();
        }
    }
}
