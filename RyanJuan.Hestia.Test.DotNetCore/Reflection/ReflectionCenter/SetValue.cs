using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using HestiaReflectionCenter = RyanJuan.Hestia.ReflectionCenter;

namespace RyanJuan.Hestia.Test.DotNetCore.Reflection.ReflectionCenter
{
    [TestClass]
    public class TestSetValue
    {
        private readonly TestClassA _obj = new TestClassA();

        [TestMethod]
        public void TestSetValueForPublicStatic()
        {
            var type = typeof(TestClassA);
            var prop = HestiaReflectionCenter.GetProperty(
                type,
                "PublicStaticProp");
            HestiaReflectionCenter.SetValue(
                prop,
                null,
                "Foo");
            Assert.AreEqual(TestClassA.PublicStaticProp, "Foo");
        }

        [TestMethod]
        public void TestSetValueForPrivateStatic()
        {
            var type = typeof(TestClassA);
            var prop = HestiaReflectionCenter.GetProperty(
                type,
                "PrivateStaticProp");
            HestiaReflectionCenter.SetValue(
                prop,
                null,
                "Foo");
            Assert.AreEqual(TestClassA.PrivateStaticProp, "Foo");
        }

        [TestMethod]
        public void TestSetValueForPublicInstance()
        {
            var type = typeof(TestClassA);
            var prop = HestiaReflectionCenter.GetProperty(
                type,
                "PublicInstanceProp");
            HestiaReflectionCenter.SetValue(
                prop,
                _obj,
                "Foo");
            Assert.AreEqual(_obj.PublicInstanceProp, "Foo");
        }

        [TestMethod]
        public void TestSetValueForPrivateInstance()
        {
            var type = typeof(TestClassA);
            var prop = HestiaReflectionCenter.GetProperty(
                type,
                "PrivateInstanceProp");
            HestiaReflectionCenter.SetValue(
                prop,
                _obj,
                "Foo");
            Assert.AreEqual(_obj.PrivateInstanceProp, "Foo");
        }

        private class TestClassA
        {
            public static string PublicStaticProp { get; set; }

            public static string PrivateStaticProp { get; private set; }

            public string PublicInstanceProp { get; set; }

            public string PrivateInstanceProp { get; private set; }
        }
    }
}
