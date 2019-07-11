using LightInject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StonePayments.Server.Controllers;
using StonePayments.Server.Dal;
using StonePayments.Server.Services;
using StonePayments.Server.Tests.Controllers;

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

            serviceContainer.Register<ITransactionDao, TransactionDao>();

            serviceContainer.Register<ITransactionService, TransactionService>();

            serviceContainer.Register<ITransactionController, TransactionController>();
        }
    }
}