using System;
using System.Windows.Input;
using Library.Util.Attributes;
using System.Windows;
using Library.Util;
using System.Net.Http;

namespace StonePayments.ViewModels
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
            ErrorList errorList;

            bool isValid = ValidationProperties.IsValid(transactionViewModel.TransactionModel, out errorList);

            if(!isValid)
            {
                MessageBox.Show(errorList.ToString());
            }

            return isValid;
        }

        public void Execute(object parameter)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://192.168.0.11:9090/stone/");
                //HTTP GET
                var responseTask = client.GetAsync("sendTransaction");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var message = readTask.Result;

                    MessageBox.Show(message);
                }
            }
        }
    }
}
