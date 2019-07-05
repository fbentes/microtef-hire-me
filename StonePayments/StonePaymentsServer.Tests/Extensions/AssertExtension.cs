using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonePaymentsServer.Tests
{
    public class AssertExtension
    {
        public static void DoesNotThrows<T>(Action expressionUnderTest) where T : Exception
        {
            try
            {
                expressionUnderTest();
            }
            catch (T)
            {
                Assert.IsFalse(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }

            Assert.IsTrue(true);
        }
    }
}
