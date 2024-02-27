using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RyanJuan.Hestia;

namespace RyanJuan.Hestia.Test.DotNetFramework
{
    [TestClass]
    public class Contains
    {
        [TestMethod]
        public void TestContains()
        {
            var str = "ABC123abc";
            //Assert.IsTrue(str.Contains("Abc", StringComparison.InvariantCultureIgnoreCase));
            //Assert.IsFalse(str.Contains("Abc", StringComparison.InvariantCulture));
        }
    }
}
