using LightInject;
using LightInject.Web;
using StonePayments.Business;
using StonePayments.Server.Controllers;
using StonePayments.Server.Repositories;
using StonePayments.Server.Services;
using StonePayments.Util.Repositories;
using StonePayments.Util.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace StonePayments.Server
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            dependencyInjection();
        }

        private void dependencyInjection()
        {
            var container = new ServiceContainer();

            container.EnableAnnotatedPropertyInjection();

            container.RegisterApiControllers();

            container.EnablePerWebRequestScope();

            container.EnableWebApi(GlobalConfiguration.Configuration);

            #region Injetar objetos relacionados a entidade Card

            container.Register<IBaseCRUDRepository<CardModel>, CardRepository>();
            container.Register<IBaseCRUDServiceBridge<CardModel>, CardService>();

            #endregion

            #region Injetar objetos relacionados a entidade Customer

            container.Register<IBaseCRUDRepository<CustomerModel>, CustomerRepository>();
            container.Register<IBaseCRUDServiceBridge<CustomerModel>, CustomerService>();

            #endregion

            #region Injetar objetos relacionados a entidade Transaction

            container.Register<ITransactionRepository, TransactionRepository>();
            container.Register<ITransactionService, TransactionService>();
            container.Register<ITransactionController, TransactionController>();

            #endregion
        }
    }
}
