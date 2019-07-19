using LightInject;
using StonePayments.Business;
using StonePayments.Business.Interfaces;
using StonePayments.Business.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Web.UI;

namespace StonePayments.ClientWeb
{
    public partial class CustomerWebView : Page, IViewObservable
    {
        [Inject]
        public ICustomerViewModel CustomerViewModel { get; set; }

        public CustomerWebView()
        {
            CustomerViewModel = new CustomerViewModel
            {
                MainViewObservable = this
            };
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            gridViewCustomer.RowEditing += GridViewCustomer_RowEditing;
            gridViewCustomer.RowDeleting += GridViewCustomer_RowDeleting;
            btnSalvar.Click += BtnSalvar_Click;

            if (!IsPostBack)
            {
                gridViewCustomer.TypeEntityModel = typeof(CustomerModel);
                SetDataSource();
            }
        }

        private void SetDataSource()
        {
            gridViewCustomer.EditIndex = -1;
            gridViewCustomer.DataSource = GetCustomers();
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            var customerModel = new CustomerModel
            {
                Name = txtName.Text
            };

            if(string.IsNullOrEmpty(txtCreditLimit.Text))
            {
                customerModel.CreditLimit = null;
            }
            else
            {
                double result;

                if(double.TryParse(txtCreditLimit.Text, out result))
                {
                    customerModel.CreditLimit = result;
                }
                else
                {
                    Response.Write("<script language='javascript'>alert('"+StonePaymentResource.CreditLimitAllowOnlyNumber+"');</script>");
                    return;
                }
            };

            CustomerViewModel.CustomerModel = customerModel;

            if (string.IsNullOrEmpty(lblId.Text)) // Insert Customer
            {
                CustomerViewModel.InsertCustomerCommand.Execute(null);
            }
            else   // Update Customer
            {
                CustomerViewModel.CustomerModel.Id = Guid.Parse(lblId.Text);

                CustomerViewModel.UpdateCustomerCommand.Execute(null);
            }

            SetDataSource();
        }

        private void GridViewCustomer_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            string id = gridViewCustomer.DataKeys[e.NewEditIndex].Value.ToString();

            var customerModel = new CustomerModel
            {
                Id = Guid.Parse(id.ToString())
            };

            CustomerViewModel.CustomerModel = customerModel;

            CustomerViewModel.GetCustomerCommand.Execute(null);

            if(!string.IsNullOrEmpty(CustomerViewModel.CustomerModel.Name))
            {
                lblId.Text = CustomerViewModel.CustomerModel.Id.ToString();
                txtName.Text = CustomerViewModel.CustomerModel.Name;
                txtCreditLimit.Text = CustomerViewModel.CustomerModel.CreditLimit.ToString();
            }

            e.Cancel = true;
        }

        private void GridViewCustomer_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            object id = gridViewCustomer.DataKeys[e.RowIndex].Value;

            var customerModel = new CustomerModel
            {
                Id = Guid.Parse(id.ToString())
            };

            CustomerViewModel.CustomerModel = customerModel;

            CustomerViewModel.DeleteCustomerCommand.Execute(null);

            SetDataSource();
        }

        public ObservableCollection<CustomerModel> GetCustomers()
        {
            CustomerViewModel.GetAllCustomerCommand.Execute(null);

            var customerModels = CustomerViewModel.CustomerModelList;

            return customerModels;
        }

        public void StartProcess()
        {
            // Implementação para mostrar para o usuário que está processsando.
        }

        public void EndProcess()
        {
            // Encerramento da mensagem de processando para o usuário.
            //gridViewCustomer.DataSource = GetCustomers();
        }

        public void SendResultMessage(string message, string caption = "")
        {
            // Mensagem de sucesso ou fracasso após o processamento do comando.

            Response.Write("<script language='javascript'>alert('"+ message + "');</script>");
        }

        public void Shutdown()
        {
            // Encerramento do caso de uso, mas para Web não é necessário.
        }
    }
}