using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RyanJuan.Hestia.Test.DotNetCore.Reflection
{
    [TestClass]
    public class TestGetDefaultValue
    {
        [DataTestMethod]
        [DataRow(typeof(int), 0)]
        [DataRow(typeof(string), null)]
        [DataRow(typeof(object), null)]
        [DataRow(typeof(MyClass), null)]
        public void TestGetDefaultValueForTypes(Type type, object expectedResult)
        {
            var result = type.GetDefaultValue();
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestGetDefaultValueForStructs()
        {
            var types = new (Type type, object expectedResult)[]
            {
                (typeof(decimal), 0m),
                (typeof(DateTime), default(DateTime)),
                (typeof(MyStruct), default(MyStruct)),
            };
            foreach (var (type, expectedResult) in types)
            {
                var result = type.GetDefaultValue();
                Assert.AreEqual(expectedResult, result);
            }
        }

        private struct MyStruct
        {
        }

        private class MyClass
        {
        }
    }
}
