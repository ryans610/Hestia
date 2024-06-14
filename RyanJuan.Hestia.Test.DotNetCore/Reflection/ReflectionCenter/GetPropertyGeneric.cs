using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using HestiaReflectionCenter = RyanJuan.Hestia.ReflectionCenter;

namespace RyanJuan.Hestia.Test.DotNetCore.Reflection.ReflectionCenter
{
    [TestClass]
    public class TestGetPropertyGeneric
    {
        [TestMethod]
        public void TestGetPropertyForPublicStatic()
        {
            var prop = HestiaReflectionCenter.GetProperty<TestClassA>(
                "PublicStaticProp");
            Assert.IsNotNull(prop);
            Assert.AreEqual(prop.Name, "PublicStaticProp");
        }

        [TestMethod]
        public void TestGetPropertyForPrivateStatic()
        {
            var prop = HestiaReflectionCenter.GetProperty<TestClassA>(
                "PrivateStaticProp");
            Assert.IsNotNull(prop);
            Assert.AreEqual(prop.Name, "PrivateStaticProp");
        }

        [TestMethod]
        public void TestGetPropertyForPublicInstance()
        {
            var prop = HestiaReflectionCenter.GetProperty<TestClassA>(
                "PublicInstanceProp");
            Assert.IsNotNull(prop);
            Assert.AreEqual(prop.Name, "PublicInstanceProp");
        }

        [TestMethod]
        public void TestGetPropertyForPrivateInstance()
        {
            var prop = HestiaReflectionCenter.GetProperty<TestClassA>(
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
