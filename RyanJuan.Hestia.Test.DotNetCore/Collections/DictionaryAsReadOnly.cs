using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using RyanJuan.Hestia;

namespace RyanJuan.Hestia.Test.DotNetCore.Collections
{
    [TestClass]
    public class TestDictionaryAsReadOnly
    {
        [TestMethod]
        public void TestNull()
        {
            Dictionary<int, string> dict = null;
            try
            {
                dict.AsReadOnly();
            }
            catch (ArgumentNullException)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void TestAsReadOnly()
        {
            var dict = new Dictionary<int, string>();
            var random = new Random();
            int length = random.Next(100);
            for (int i = 0; i < length; i += 1)
            {
                dict[random.Next()] = random.NextDouble().ToString();
            }
            var result = dict.AsReadOnly();
            Assert.IsInstanceOfType(result, typeof(ReadOnlyDictionary<int, string>));
            Assert.AreEqual(result.Count, dict.Count);
            foreach (var key in dict.Keys)
            {
                Assert.AreEqual(dict[key], result[key]);
            }
        }
    }
}
