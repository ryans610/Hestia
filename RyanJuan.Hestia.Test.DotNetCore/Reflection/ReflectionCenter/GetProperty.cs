using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using HestiaReflectionCenter = RyanJuan.Hestia.ReflectionCenter;

namespace RyanJuan.Hestia.Test.DotNetCore.Reflection.ReflectionCenter
{
    [TestClass]
    public class TestGetProperty
    {
        [TestMethod]
        public void TestGetPropertyForPublicStatic()
        {
            var type = typeof(TestClassA);
            var prop = HestiaReflectionCenter.GetProperty(
                type,
                "PublicStaticProp");
            Assert.IsNotNull(prop);
            Assert.AreEqual(prop.Name, "PublicStaticProp");
        }

        [TestMethod]
        public void TestGetPropertyForPrivateStatic()
        {
            var type = typeof(TestClassA);
            var prop = HestiaReflectionCenter.GetProperty(
                type,
                "PrivateStaticProp");
            Assert.IsNotNull(prop);
            Assert.AreEqual(prop.Name, "PrivateStaticProp");
        }

        [TestMethod]
        public void TestGetPropertyForPublicInstance()
        {
            var type = typeof(TestClassA);
            var prop = HestiaReflectionCenter.GetProperty(
                type,
                "PublicInstanceProp");
            Assert.IsNotNull(prop);
            Assert.AreEqual(prop.Name, "PublicInstanceProp");
        }

        [TestMethod]
        public void TestGetPropertyForPrivateInstance()
        {
            var type = typeof(TestClassA);
            var prop = HestiaReflectionCenter.GetProperty(
                type,
                "PrivateInstanceProp");
            Assert.IsNotNull(prop);
            Assert.AreEqual(prop.Name, "PrivateInstanceProp");
        }

        private class TestClassA
        {
            public static string PublicStaticProp { get; set; }

            private static string PrivateStaticProp { get; set; }

            public string PublicInstanceProp { get; set; }

            private string PrivateInstanceProp { get; set; }
        }
    }
}
