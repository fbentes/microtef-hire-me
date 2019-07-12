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
                Password = "123",
                Type = TransactionType.Credit,
                Number = null                
            };

            ValidationErrorList errorList = new ValidationErrorList();

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
                Password = "12345",
                Type = TransactionType.Credit,
                Number = 4
            };

            ValidationErrorList errorList = new ValidationErrorList();

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
