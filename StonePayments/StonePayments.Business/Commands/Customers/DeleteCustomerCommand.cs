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
    public class DeleteCustomerCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly ICustomerViewModel customerViewModel;

        public DeleteCustomerCommand(ICustomerViewModel customerViewModel)
        {
            this.customerViewModel = customerViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        private void DeleteCustomer()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(StonePaymentResource.Server);

                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                var id = customerViewModel.CustomerModel.Id.ToString();

                var response = client.DeleteAsync("stone/deleteCustomer/"+ id).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {

                    var readTask = response.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var result = readTask.Result;

                    customerViewModel.MainViewObservable.SendResultMessage(
                        result.Replace("\"",""), StonePaymentResource.ResultTitle);
                }
                else
                {
                    customerViewModel.MainViewObservable.SendResultMessage("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
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

            bool isValid = ValidationProperties.IsValid(customerViewModel.CustomerModel, out errorList);

            if (!isValid)
            {
                customerViewModel.MainViewObservable.SendResultMessage(
                    errorList.ToString(), StonePaymentResource.ValidationTitle);

                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                customerViewModel.MainViewObservable.StartProcess();

                DeleteCustomer();
            }
            finally
            {
                customerViewModel.MainViewObservable.EndProcess();
            }
        }
    }
}
