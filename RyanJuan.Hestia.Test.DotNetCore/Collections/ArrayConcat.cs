using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using RyanJuan.Hestia;

namespace RyanJuan.Hestia.Test.DotNetCore.Collections
{
    [TestClass]
    public class TestArrayConcat
    {
        [TestMethod]
        public void TestNull()
        {
            int[] arr1 = null;
            var arr2 = new int[5];
            try
            {
                arr1.Concat(arr2);
            }
            catch (ArgumentNullException)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void TestConcatNull()
        {
            var arr1 = new int[5];
            try
            {
                arr1.Concat(null);
            }
            catch (ArgumentNullException)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void TestConcatAnyNull()
        {
            var arr1 = new int[5];
            try
            {
                arr1.Concat(new int[3], null);
            }
            catch (ArgumentNullException)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void TestConcatEmpty()
        {
            var arr1 = CreateRandomArray();
            var arr2 = new int[0];
            var result1 = arr1.Concat(arr2);
            AssertTwoArrayEquals(result1, arr1);
            var result2 = arr2.Concat(arr1);
            AssertTwoArrayEquals(result2, arr1);
            var result3 = arr1.Concat(arr2, arr2, arr2);
            AssertTwoArrayEquals(result3, arr1);
        }

        [TestMethod]
        public void TestConcatTwo()
        {
            var arr1 = CreateRandomArray();
            var arr2 = CreateRandomArray();
            var result1 = arr1.Concat(arr2);
            var result2 = arr1.AsEnumerable().Concat(arr2).ToArray();
            AssertTwoArrayEquals(result1, result2);
        }

        [TestMethod]
        public void TestConcatThree()
        {
            var arr1 = CreateRandomArray();
            var arr2 = CreateRandomArray();
            var arr3 = CreateRandomArray();
            var result1 = arr1.Concat(arr2, arr3);
            var result2 = arr1.AsEnumerable().Concat(arr2).Concat(arr3).ToArray();
            AssertTwoArrayEquals(result1, result2);
        }

        [TestMethod]
        public void TestConcatMany()
        {
            var rand = new Random();
            var arr1 = CreateRandomArray();
            var arrOther = new int[rand.Next(20)][];
            for (int i = 0; i < arrOther.Length; i++)
            {
                arrOther[i] = CreateRandomArray();
            }
            var result1 = arr1.Concat(arrOther);
            var resultEnumerable = arr1.AsEnumerable();
            for (int i = 0; i < arrOther.Length; i++)
            {
                resultEnumerable = resultEnumerable.Concat(arrOther[i]);
            }
            var result2 = resultEnumerable.ToArray();
            AssertTwoArrayEquals(result1, result2);
        }

        private static int[] CreateRandomArray()
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var arr = new int[random.Next(100)];
            for (int i = 0; i < arr.Length; i += 1)
            {
                arr[i] = random.Next();
            }
            return arr;
        }

        private static void AssertTwoArrayEquals(int[] result1, int[] result2)
        {
            Assert.AreEqual(result1.Length, result2.Length);
            for (int i = 0; i < result1.Length; i += 1)
            {
                Assert.AreEqual(result1[i], result2[i]);
            }
        }
    }
}
