using System;
using System.Windows.Input;
using System.Windows;
using StonePayments.Util;
using System.Net.Http;
using System.Net.Http.Headers;

namespace StonePayments.Client.ViewModels
{
    public class SendTransactionCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly TransactionViewModel transactionViewModel;

        public SendTransactionCommand(TransactionViewModel transactionViewModel)
        {
            this.transactionViewModel = transactionViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        private void SendTransaction()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.0.11:9090/");

                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                ErrorList errorList;

                bool isValid = ValidationProperties.IsValid(transactionViewModel.TransactionModel, out errorList);

                if (!isValid)
                {
                    MessageBox.Show(errorList.ToString());
                    return;
                }

                var response = client.PostAsJsonAsync("stone/SendTransaction", 
                                                      transactionViewModel.TransactionModel).Result;

                if (response.IsSuccessStatusCode)
                {

                    var readTask = response.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var message = readTask.Result;

                    MessageBox.Show(message);
                }
                else
                {
                    MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                }
            }
        }

        public void Execute(object parameter)
        {
            SendTransaction();
            //SendTransaction().GetAwaiter().GetResult();
        }
    }
}
