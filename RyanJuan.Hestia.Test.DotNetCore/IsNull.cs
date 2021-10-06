using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using RyanJuan.Hestia;

namespace RyanJuan.Hestia.Test.DotNetCore
{
    [TestClass]
    public class TestIsNull
    {
        private static readonly string s_stringNotNull = "ABC123abc";

        private static readonly string s_stringNull = null;

        [TestMethod]
        public void TestStringIsNull()
        {
            Assert.IsFalse(s_stringNotNull.IsNull());
            Assert.IsTrue(s_stringNull.IsNull());
        }

        [TestMethod]
        public void TestStringIsNotNull()
        {
            Assert.IsTrue(s_stringNotNull.IsNotNull());
            Assert.IsFalse(s_stringNull.IsNotNull());
        }
    }
}
