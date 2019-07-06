using LightInject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StonePaymentsServer.Controllers;
using StonePaymentsServer.Dal;
using StonePaymentsServer.Services;
using StonePaymentsServer.Tests.Controllers;

namespace StonePaymentsServer.Tests
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

            serviceContainer.Register<ITransactionDao, TransactionDao>();

            serviceContainer.Register<ITransactionService, TransactionService>();

            serviceContainer.Register<ITransactionController, TransactionController>();
        }
    }
}