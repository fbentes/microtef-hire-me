﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using StonePayments.Server.Services;
using StonePayments.Server.Tests;
using System.Threading.Tasks;
using LightInject;
using StonePayments.Business;
using System;
using StonePayments.Util.Services;

namespace StonePaymentsServer.Tests.Services
{
    [TestClass]
    public class CardServiceTest : BaseTest
    {
        public IBaseCRUDServiceBridge<CardModel> CardService { get; set; }

        public CardServiceTest()
        {
            this.CardService = serviceContainer.GetInstance<IBaseCRUDServiceBridge<CardModel>>();
        }

        [TestMethod]
        public async Task InsertCardSucess()
        {
            var r = new Random();

            var CardModel = new CardModel
            {
                Id = Guid.NewGuid(),
                Customer = new CustomerModel
                {
                    Id = Guid.Parse("3B751149-2CE7-4DC2-A244-46AECC73169B")
                },
                CardBrand = CardBrand.AmericanExpress,
                ExpirationDate = DateTime.Now,
                HasPassword = true,
                Number = Convert.ToInt64(r.Next(1,4000).ToString() + r.Next(4001,8000).ToString() + r.Next(8001,12000).ToString() + r.Next(12001, 16000).ToString()),
                Password = "963258",
                Type = CardType.Chip                
            };

            try
            {
                await CardService.Insert(CardModel);

                Assert.IsTrue(true);
            }
            catch (CardException)
            {
                Assert.IsFalse(true);
            }
        }

        [TestMethod]
        public async Task InsertCardSomeFail()
        {
            var CardModel = new CardModel
            {
                Id = Guid.NewGuid(),
                Customer = new CustomerModel
                {
                    Id = Guid.Parse("3B751149-2CE7-4DC2-A244-46AECC73169B")
                },
                CardBrand = CardBrand.AmericanExpress,
                ExpirationDate = DateTime.Now,
                HasPassword = null,
                Number = 96325874,
                Password = "963258",
                Type = CardType.Chip
            };

            try
            {
                await CardService.Insert(CardModel);

                Assert.IsTrue(false);
            }
            catch (CardException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
