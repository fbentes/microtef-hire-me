﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StonePayments.Util;
using Newtonsoft.Json;

namespace StonePayments.Server.Tests.Util
{
    [TestClass]
    public class DataBaseConnectionTest
    {
        [TestMethod]
        public void TestGetConnectionString()
        {
            string directory = Environment.CurrentDirectory;

            string path = directory + "\\DataBaseConnection.json";

            string s = DataBaseConnectionDecryptedString.Execute(path, KeyStringCryptography.VALUE);

            Assert.IsNotNull(s);
        }
    }
}
