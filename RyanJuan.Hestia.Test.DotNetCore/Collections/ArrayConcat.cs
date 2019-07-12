using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RyanJuan.Hestia;
using System.Linq;

namespace RyanJuan.Hestia.Test.DotNetCore.Collections
{
    [TestClass]
    public class ArrayConcat
    {
        [TestMethod]
        public void TestArrayConcatTwo()
        {
            var rand = new Random();
            var arr1 = new int[rand.Next(100)];
            var arr2 = new int[rand.Next(100)];
            for (int i = 0; i < arr1.Length; i++)
            {
                arr1[i] = rand.Next();
            }
            for (int i = 0; i < arr2.Length; i++)
            {
                arr2[i] = rand.Next();
            }
            var result1 = arr1.Concat(arr2);
            var result2 = arr1.AsEnumerable().Concat(arr2).ToArray();
            Assert.AreEqual(result1.Length, result2.Length);
            for (int i = 0; i < result1.Length; i++)
            {
                Assert.AreEqual(result1[i], result2[i]);
            }
        }

        [TestMethod]
        public void TestArrayConcatThree()
        {
            var rand = new Random();
            var arr1 = new int[rand.Next(100)];
            var arr2 = new int[rand.Next(100)];
            var arr3 = new int[rand.Next(100)];
            for (int i = 0; i < arr1.Length; i++)
            {
                arr1[i] = rand.Next();
            }
            for (int i = 0; i < arr2.Length; i++)
            {
                arr2[i] = rand.Next();
            }
            for (int i = 0; i < arr3.Length; i++)
            {
                arr3[i] = rand.Next();
            }
            var result1 = arr1.Concat(arr2, arr3);
            var result2 = arr1.AsEnumerable().Concat(arr2).Concat(arr3).ToArray();
            Assert.AreEqual(result1.Length, result2.Length);
            for (int i = 0; i < result1.Length; i++)
            {
                Assert.AreEqual(result1[i], result2[i]);
            }
        }

        [TestMethod]
        public void TestArrayConcatMany()
        {
            var rand = new Random();
            var arr1 = new int[rand.Next(100)];
            var arrOther = new int[rand.Next(20)][];
            for (int i = 0; i < arr1.Length; i++)
            {
                arr1[i] = rand.Next();
            }
            for (int i = 0; i < arrOther.Length; i++)
            {
                arrOther[i] = new int[rand.Next(100)];
                for (int j = 0; j < arrOther[i].Length; j++)
                {
                    arrOther[i][j] = rand.Next();
                }
            }
            var result1 = arr1.Concat(arrOther);
            var resultEnumerable = arr1.AsEnumerable();
            for (int i = 0; i < arrOther.Length; i++)
            {
                resultEnumerable = resultEnumerable.Concat(arrOther[i]);
            }
            var result2 = resultEnumerable.ToArray();
            Assert.AreEqual(result1.Length, result2.Length);
            for (int i = 0; i < result1.Length; i++)
            {
                Assert.AreEqual(result1[i], result2[i]);
            }
        }
    }
}
