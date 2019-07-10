using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library;
using Library.Util;

namespace StonePaymentsServer.Tests
{
    [TestClass]
    public class CryptographyTest
    {
        private String keyString { get; set; }

        private Cryptography cryptography;

        public CryptographyTest()
        {
            cryptography = new Cryptography();

            keyString = cryptography.GenerateAPassKey("Teste para a Stone");
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestEncrypt()
        {
            String EncryptedPassword = Cryptography.Encrypt(@"data source=DESKTOP-QGINSHI\SQLEXPRESS;initial catalog=StonePayments;user id=sa;Password=senha;", keyString);

            Assert.IsNotNull(EncryptedPassword);
        }

        [TestMethod]
        public void TestDecrypt()
        {
            String EncryptedPassword = Cryptography.Encrypt(@"data source=DESKTOP-QGINSHI\SQLEXPRESS;initial catalog=StonePayments;user id=sa;Password=senha;", keyString);

            String DecryptedPassword = Cryptography.Decrypt(EncryptedPassword, keyString);

            Assert.IsNotNull(DecryptedPassword);
        }
    }
}
