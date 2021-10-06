using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RyanJuan.Hestia.Test.DotNetCore.Collections
{
    [TestClass]
    public class TestToList
    {
        [TestMethod]
        public void TestNull()
        {
            try
            {
                int[] arr = null;
                var list = arr.ToList(3);
            }
            catch (ArgumentNullException)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void TestNegative()
        {
            try
            {
                var dict = new Dictionary<int, string>
                {
                    [3] = "123",
                    [44] = "GEGE",
                    [20] = "fj93",
                };
                var list = dict.ToList(-5);
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void TestLess()
        {
            var list = Enumerable.Range(0, 20).ToList(0);
            Assert.IsTrue(list.Capacity >= 20);
        }

        [TestMethod]
        public void TestGreater()
        {
            var list = Enumerable.Range(0, 20).ToList(33);
            Assert.AreEqual(list.Capacity, 33);
        }
    }
}
