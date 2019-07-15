using System;
using System.Windows.Input;
using System.Windows;
using StonePayments.Util;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using StonePayments.Business;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;

namespace StonePayments.Client.ViewModels
{
    public class SendTransactionCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly ISendTransactionViewModel sendTransactionViewModel;

        public SendTransactionCommand(ISendTransactionViewModel sendTransactionViewModel)
        {
            this.sendTransactionViewModel = sendTransactionViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        private void SendTransaction()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(StonePaymentServerResource.Server);

                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync("stone/sendTransaction", 
                                                      sendTransactionViewModel.TransactionModel).Result;

                if (response.IsSuccessStatusCode)
                {

                    var readTask = response.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var result = readTask.Result;

                    if(result.StartsWith("{") && result.EndsWith("}") || 
                       result.StartsWith("[") && result.EndsWith("]"))
                    {
                        sendTransactionViewModel.TransactionModelList =
                            JsonConvert.DeserializeObject<ObservableCollection<TransactionModel>>(result);

                        sendTransactionViewModel.ViewObservable.SendResultMessage(
                            StonePaymentResource.TransactionSendOk, StonePaymentResource.ResultTitle);
                    }
                    else
                    {
                        sendTransactionViewModel.ViewObservable.SendResultMessage(
                            result.Replace("\"",""), StonePaymentResource.ResultTitle);
                    }
                }
                else
                {
                    sendTransactionViewModel.ViewObservable.SendResultMessage("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                }
            }
        }

        private bool isValidProperties()
        {
            ValidationErrorList errorList;

            bool isValid = ValidationProperties.IsValid(sendTransactionViewModel.TransactionModel, out errorList);

            if (!isValid)
            {
                MessageBox.Show(errorList.ToString(), StonePaymentResource.ValidationTitle);
                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            if(!isValidProperties())
            {
                return;
            }

            try
            {
                sendTransactionViewModel.ViewObservable.StartProcess();

                SendTransaction();
            }
            finally
            {
                sendTransactionViewModel.ViewObservable.EndProcess();
            }
        }
    }
}
