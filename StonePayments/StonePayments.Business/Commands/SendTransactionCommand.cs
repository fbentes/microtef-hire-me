using System;
using System.Windows.Input;
using StonePayments.Util;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace StonePayments.Business.ViewModels
{
    /// <summary>
    /// Classe responsável para envio dos dados da transação para o servidor.
    /// </summary>
    public class SendTransactionCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly ITransactionViewModel sendTransactionViewModel;

        public SendTransactionCommand(ITransactionViewModel sendTransactionViewModel)
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
                client.BaseAddress = new Uri(StonePaymentResource.Server);

                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync("stone/sendTransaction", 
                                                      sendTransactionViewModel.TransactionModel).Result;

                if (response.IsSuccessStatusCode)
                {

                    var readTask = response.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var result = readTask.Result;

                    // Checa se é uma string json válida...

                    if(result.StartsWith("{") && result.EndsWith("}") || 
                       result.StartsWith("[") && result.EndsWith("]"))
                    {
                        sendTransactionViewModel.TransactionModelList =
                            JsonConvert.DeserializeObject<ObservableCollection<TransactionModel>>(result);

                        sendTransactionViewModel.MainViewObservable.SendResultMessage(
                            StonePaymentResource.TransactionSendOk, StonePaymentResource.ResultTitle);
                    }
                    else // ... senão é uma mensagem string de resultado para exibição para o usuário.
                    {
                        sendTransactionViewModel.MainViewObservable.SendResultMessage(
                            result.Replace("\"",""), StonePaymentResource.ResultTitle);
                    }
                }
                else
                {
                    sendTransactionViewModel.MainViewObservable.SendResultMessage("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                }
            }
        }
        
        /// <summary>
        /// Método para validar as propriedades do objeto antes de enviá-lo para o servidor.
        /// </summary>
        /// <returns>Retorna true se passou na validação, e false caso contrário.</returns>
        private bool isValidProperties()
        {
            ValidationErrorList errorList;

            bool isValid = ValidationProperties.IsValid(sendTransactionViewModel.TransactionModel, out errorList);

            if (!isValid)
            {
                sendTransactionViewModel.MainViewObservable.SendResultMessage(
                    errorList.ToString(), StonePaymentResource.ValidationTitle);

                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            if (!isValidProperties())
            {
                return;
            }

            try
            {
                sendTransactionViewModel.MainViewObservable.StartProcess();

                SendTransaction();
            }
            finally
            {
                sendTransactionViewModel.MainViewObservable.EndProcess();
            }
        }
    }
}
