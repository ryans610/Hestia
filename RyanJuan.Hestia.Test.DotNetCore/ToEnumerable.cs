using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RyanJuan.Hestia;
using System.Linq;

namespace RyanJuan.Hestia.Test.DotNetCore
{
    [TestClass]
    public class ToEnumerable
    {
        [TestMethod]
        public void TestToEnumerable()
        {
            int origin = 5;
            var result1 = origin.ToEnumerable();
            Assert.IsInstanceOfType(result1, typeof(IEnumerable<int>));
            Assert.AreEqual(result1.Count(), 1);
            Assert.AreEqual(result1.First(), origin);

            var obj = new object();
            var result2 = obj.ToEnumerable();
            Assert.IsInstanceOfType(result2, typeof(IEnumerable<object>));
            Assert.AreEqual(result2.Count(), 1);
            Assert.ReferenceEquals(result2.First(), obj);
        }
    }
}
