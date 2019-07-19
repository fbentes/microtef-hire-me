using Newtonsoft.Json;
using StonePayments.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Input;

namespace StonePayments.Business.Commands
{
    /// <summary>
    /// Classe responsável para solicitar do servidor a lista de todos os clientes.
    /// </summary>
    public class GetCustomerCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly ICustomerViewModel customerViewModel;

        public GetCustomerCommand(ICustomerViewModel customerViewModel)
        {
            this.customerViewModel = customerViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        private void getCustomer()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(StonePaymentResource.Server);

                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                var id = customerViewModel.CustomerModel.Id.ToString();

                var response = client.GetAsync("stone/customer/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var result = readTask.Result;

                    customerViewModel.CustomerModel = JsonConvert.DeserializeObject<CustomerModel>(result);
                }
                else
                {
                    customerViewModel.MainViewObservable.SendResultMessage("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                }
            }
        }
        public void Execute(object parameter)
        {
            try
            {
                customerViewModel.MainViewObservable.StartProcess();

                getCustomer();
            }
            finally
            {
                customerViewModel.MainViewObservable.EndProcess();
            }
        }
    }
}
