using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using HestiaReflectionCenter = RyanJuan.Hestia.ReflectionCenter;

namespace RyanJuan.Hestia.Test.DotNetCore.Reflection.ReflectionCenter
{
    [TestClass]
    public class TestCreateInstance
    {
        [TestMethod]
        public void TestCreateInstanceForPublicCtor()
        {
            var objA = HestiaReflectionCenter.CreateInstance<TestClassA>();
            Assert.IsInstanceOfType(objA, typeof(TestClassA));
            Assert.AreEqual(objA.II, ConstInt32 * 2);
        }

        [TestMethod]
        public void TestCreateInstanceForPrivateCtor()
        {
            var objB = HestiaReflectionCenter.CreateInstance<TestClassB>();
            Assert.IsInstanceOfType(objB, typeof(TestClassB));
            Assert.AreEqual(objB.II, ConstInt32 * 2);
        }

        private const int ConstInt32 = 15;

        private class TestClassA
        {
            public TestClassA()
            {
                II = ConstInt32 * 2;
            }

            public int II { get; }
        }

        private class TestClassB
        {
            private TestClassB()
            {
                II = ConstInt32 * 2;
            }

            public int II { get; }
        }
    }
}
