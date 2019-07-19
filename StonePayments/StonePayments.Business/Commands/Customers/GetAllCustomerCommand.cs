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
    /// Classe responsável para solicitar do servidor a lista de todos os clientes.
    /// </summary>
    public class GetAllCustomerCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly ICustomerViewModel customerViewModel;

        public GetAllCustomerCommand(ICustomerViewModel customerViewModel)
        {
            this.customerViewModel = customerViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        private void GetAllCustomers()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(StonePaymentResource.Server);

                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("stone/customers").Result;

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var result = readTask.Result;

                    customerViewModel.CustomerModelList =
                        JsonConvert.DeserializeObject<ObservableCollection<CustomerModel>>(result);

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

                GetAllCustomers();
            }
            finally
            {
                customerViewModel.MainViewObservable.EndProcess();
            }
        }
    }
}
