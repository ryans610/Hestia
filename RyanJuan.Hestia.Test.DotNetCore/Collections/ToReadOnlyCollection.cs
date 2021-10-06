using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RyanJuan.Hestia.Test.DotNetCore.Collections
{
    [TestClass]
    public class TestToReadOnlyCollection
    {
        [TestMethod]
        public void TestNull()
        {
            try
            {
                IEnumerable<long> enumerable = null;
                var roc = enumerable.ToReadOnlyCollection();
            }
            catch (ArgumentNullException)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void TestList()
        {
            var list = new List<byte>
            {
                1,
                5,
                6,
                99,
            };
            var roc = list.ToReadOnlyCollection();
            Assert.IsInstanceOfType(roc, typeof(ReadOnlyCollection<byte>));
            TestReadOnlyCollection(roc, list);
        }

        [TestMethod]
        public void TestEnumerable()
        {
            var enumerable = Enumerable.Range(53, 26);
            var roc = enumerable.ToReadOnlyCollection();
            Assert.IsInstanceOfType(roc, typeof(ReadOnlyCollection<int>));
            TestReadOnlyCollection(roc, enumerable);
        }

        private void TestReadOnlyCollection<T>(
            ReadOnlyCollection<T> roc,
            IEnumerable<T> enumerable)
        {
            using var iterator = enumerable.GetEnumerator();
            foreach (var a in roc)
            {
                if (!iterator.MoveNext())
                {
                    Assert.Fail();
                }
                Assert.AreEqual(a, iterator.Current);
            }
            if (iterator.MoveNext())
            {
                Assert.Fail();
            }
        }
    }
}
