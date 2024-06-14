using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using HestiaReflectionCenter = RyanJuan.Hestia.ReflectionCenter;

namespace RyanJuan.Hestia.Test.DotNetCore.Reflection.ReflectionCenter
{
    [TestClass]
    public class TestGetValue
    {
        private readonly TestClassA _obj = new TestClassA();

        [TestMethod]
        public void TestGetValueForPublicStatic()
        {
            var type = typeof(TestClassA);
            var prop = HestiaReflectionCenter.GetProperty(
                type,
                "PublicStaticProp");
            var value = HestiaReflectionCenter.GetValue(
                prop,
                null);
            Assert.AreEqual(value, "Foo");
        }

        [TestMethod]
        public void TestGetValueForPrivateStatic()
        {
            var type = typeof(TestClassA);
            var prop = HestiaReflectionCenter.GetProperty(
                type,
                "PrivateStaticProp");
            var value = HestiaReflectionCenter.GetValue(
                prop,
                null);
            Assert.AreEqual(value, "Bar");
        }

        [TestMethod]
        public void TestGetValueForPublicInstance()
        {
            var type = typeof(TestClassA);
            var prop = HestiaReflectionCenter.GetProperty(
                type,
                "PublicInstanceProp");
            var value = HestiaReflectionCenter.GetValue(
                prop,
                _obj);
            Assert.AreEqual(value, "FooI");
        }

        [TestMethod]
        public void TestGetValueForPrivateInstance()
        {
            var type = typeof(TestClassA);
            var prop = HestiaReflectionCenter.GetProperty(
                type,
                "PrivateInstanceProp");
            var value = HestiaReflectionCenter.GetValue(
                prop,
                _obj);
            Assert.AreEqual(value, "BarI");
        }

        private class TestClassA
        {
            public static string PublicStaticProp { get; set; } = "Foo";

            private static string PrivateStaticProp { get; set; } = "Bar";

            public string PublicInstanceProp { get; set; } = "FooI";

            private string PrivateInstanceProp { get; set; } = "BarI";
        }
    }
}
