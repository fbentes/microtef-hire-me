using LightInject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StonePayments.Business;
using StonePayments.Server.Controllers;
using StonePayments.Server.Repositories;
using StonePayments.Server.Services;
using StonePayments.Util.Repositories;
using StonePayments.Util.Services;

namespace StonePayments.Server.Tests
{
    [TestClass]
    public class BaseTest
    {
        protected static ServiceContainer serviceContainer;

        [AssemblyInitialize]
        public static void dependencyInjection(TestContext context)
        {
            serviceContainer = new ServiceContainer();

            serviceContainer.EnableAnnotatedPropertyInjection();

            serviceContainer.Register<ITransactionRepository, TransactionRepository>();

            serviceContainer.Register<ITransactionService, TransactionService>();

            serviceContainer.Register<ITransactionController, TransactionController>();


            serviceContainer.Register<IBaseCRUDRepository<CustomerModel>, CustomerRepository>();

            serviceContainer.Register<IBaseCRUDRepository<CardModel>, CardRepository>();

            serviceContainer.Register<IBaseCRUDServiceBridge<CardModel>, CardService>();

            serviceContainer.Register<IBaseCRUDServiceBridge<CustomerModel>, CustomerService>();
        }
    }
}