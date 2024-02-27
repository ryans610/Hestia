using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using RyanJuan.Hestia;

namespace RyanJuan.Hestia.Test.DotNetCore.Collections
{
    [TestClass]
    public class TestMoreThanOne
    {
        [TestMethod]
        public void Foo()
        {
            var r = new[] { 1, 2, 3, 4, 5, 6 }.Batch(2).Skip(2).ToArray();
        }
        [TestMethod]
        public void TestNull()
        {
            int[] arr = null;
            try
            {
                arr.MoreThanOne();
            }
            catch (ArgumentNullException)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void TestZero()
        {
            Assert.IsFalse(Enumerable.Empty<decimal>().MoreThanOne());
        }

        [TestMethod]
        public void TestOne()
        {
            Assert.IsFalse(GetEnumerableOfOne().MoreThanOne());

            IEnumerable<TestMoreThanOne> GetEnumerableOfOne()
            {
                yield return this;
            }
        }

        [TestMethod]
        public void TestTwo()
        {
            var list = new List<string>
            {
                "ABC",
                "ddddd",
            };
            Assert.IsTrue(list.MoreThanOne());
        }

        [TestMethod]
        public void TestMany()
        {
            Assert.IsTrue(Enumerable.Range(0, new Random().Next(2, 100)).MoreThanOne());
        }
    }
}
