using LightInject;
using LightInject.Web;
using StonePayments.Server.Controllers;
using StonePayments.Server.Dal;
using StonePayments.Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace StonePaymentsServer
{
    public class WebApiApplication : System.Web.HttpApplication
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

            container.Register<ITransactionDao, TransactionDao>();

            container.Register<ITransactionService, TransactionService>();

            container.Register<ITransactionController, TransactionController>();
        }
    }
}
