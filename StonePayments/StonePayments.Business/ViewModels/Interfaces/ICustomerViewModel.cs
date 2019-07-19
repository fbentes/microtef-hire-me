using StonePayments.Business.Commands;
using StonePayments.Business.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StonePayments.Business.ViewModels
{
    public interface ICustomerViewModel
    {
        ICustomerModel CustomerModel { get; set; }

        IViewObservable MainViewObservable { get; set; }

        ObservableCollection<CustomerModel> CustomerModelList { get; set; }

        GetAllCustomerCommand GetAllCustomerCommand { get; }

        GetCustomerCommand GetCustomerCommand { get; }

        DeleteCustomerCommand DeleteCustomerCommand { get; }

        InsertCustomerCommand InsertCustomerCommand { get; }

        UpdateCustomerCommand UpdateCustomerCommand { get; }

        event PropertyChangedEventHandler PropertyChanged;

        string Name { get; set; }

        double CreditLimit { get; set; }
    }
}