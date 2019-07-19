using Microsoft.VisualStudio.TestTools.UnitTesting;
using StonePayments.Util;

namespace StonePayments.UnitTests
{
    [TestClass()]
    public class CryptographyTest
    {
        private string keyString { get; set; }

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
            string EncryptedPassword = Cryptography.Encrypt(@"data source=DESKTOP-QGINSHI\SQLEXPRESS;initial catalog=StonePayments;user id=USUARIO;Password=SENHA;", keyString);

            Assert.IsNotNull(EncryptedPassword);
        }

        [TestMethod]
        public void TestDecrypt()
        {
            string EncryptedPassword = Cryptography.Encrypt(@"data source=DESKTOP-QGINSHI\SQLEXPRESS;initial catalog=StonePayments;user id=USUARIO;Password=SENHA;", keyString);

            string DecryptedPassword = Cryptography.Decrypt(EncryptedPassword, keyString);

            Assert.IsNotNull(DecryptedPassword);
        }

        [TestMethod]
        public void TestEncryptPassword()
        {
            string encryptedPassword = Cryptography.Encrypt("654321", keyString);

            Assert.IsNotNull(encryptedPassword);
        }
    }
}