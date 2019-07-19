using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using LightInject;
using StonePayments.Business.ViewModels;
using StonePayments.Business;

namespace DataObjectLayer.View.Web.Estoque
{
    public class Global : System.Web.HttpApplication
    {
        protected void Session_Start(object sender, EventArgs e)
        {
            DependencyInjection();
        }

        protected void Application_Start(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        private void DependencyInjection()
        {
            var container = new ServiceContainer();

            container.EnableAnnotatedPropertyInjection();

            container.EnablePerWebRequestScope();

            container.Register<ICustomerViewModel, CustomerViewModel>();

            container.Register<ICustomerModel, CustomerModel>();

            Session["container"] = container;

        }

    }
}