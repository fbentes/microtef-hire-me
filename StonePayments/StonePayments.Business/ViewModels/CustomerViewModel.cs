using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using LightInject;
using StonePayments.Business.Commands;
using StonePayments.Business.Interfaces;
using StonePayments.Util.ViewModels;

namespace StonePayments.Business.ViewModels
{
    public class CustomerViewModel : BaseViewModel, ICustomerViewModel
    {
        [Inject]
        public ICustomerModel CustomerModel { get; set; }
        public IViewObservable MainViewObservable { get; set; }

        private ObservableCollection<CustomerModel> customerModelList;

        public GetAllCustomerCommand GetAllCustomerCommand => new GetAllCustomerCommand(this);

        public GetCustomerCommand GetCustomerCommand => new GetCustomerCommand(this);

        public DeleteCustomerCommand DeleteCustomerCommand => new DeleteCustomerCommand(this);

        public InsertCustomerCommand InsertCustomerCommand => new InsertCustomerCommand(this);

        public UpdateCustomerCommand UpdateCustomerCommand => new UpdateCustomerCommand(this);

        public string Name { get; set; }
        public double CreditLimit { get; set; }


        public ObservableCollection<CustomerModel> CustomerModelList
        {
            get
            {
                return customerModelList;
            }
            set
            {
                if (value == null)
                {
                    customerModelList = new ObservableCollection<CustomerModel>();
                }
                else
                {
                    customerModelList = value;
                }

                OnPropertyChange("CustomerModelList");
            }
        }
    }
}
