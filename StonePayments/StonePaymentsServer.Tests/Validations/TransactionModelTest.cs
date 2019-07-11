using Microsoft.VisualStudio.TestTools.UnitTesting;
using StonePayments.Business;
using StonePayments.Util;
using System;

namespace StonePayments.Server.Tests.Validations
{
    [TestClass]
    public class TransactionModelTest
    {
        [TestMethod]
        public void TestValidationPropertiesWithAtLeastOneError()
        {
            var transaction = new TransactionModel
            {
                Id = Guid.NewGuid(),
                Amount = 0.05,
                Card = null,
                Senha = "123",
                Type = TransactionType.Credit,
                Number = null                
            };

            ErrorList errorList = new ErrorList();

            bool isValid = ValidationProperties.IsValid(transaction, out errorList);

            if(!isValid)
            {
                foreach (var error in errorList)
                {
                    Console.WriteLine(error);
                }
            }

            Assert.IsTrue(errorList.Count > 0);
        }

        [TestMethod]
        public void TestValidationPropertiesWithoutSomeError()
        {
            var transaction = new TransactionModel
            {
                Id = Guid.NewGuid(),
                CardNumber = 65478912365496,
                Amount = 120,
                Senha = "12345",
                Type = TransactionType.Credit,
                Number = 4
            };

            ErrorList errorList = new ErrorList();

            bool isValid = ValidationProperties.IsValid(transaction, out errorList);

            if (!isValid)
            {
                foreach (var error in errorList)
                {
                    Console.WriteLine(error);
                }
            }

            Assert.IsTrue(errorList.Count > 0);
        }
    }
}
