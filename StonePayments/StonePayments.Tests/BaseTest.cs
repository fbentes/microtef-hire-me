using LightInject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StonePayments.Server.Controllers;
using StonePayments.Server.Repositories;
using StonePayments.Server.Services;

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


            serviceContainer.Register<ICustomerRepository, CustomerRepository>();

            serviceContainer.Register<ICustomerService, CustomerService>();

            serviceContainer.Register<ICardRepository, CardRepository>();

            serviceContainer.Register<ICardService, CardService>();

        }
    }
}