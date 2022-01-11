using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode
{
    // Difficulty: 
    // Time: 
    public class Template
    {
        [Test]
        [TestCase("-10,10,-10,10", 10000)]
        [TestCase("0,-4,0,-8", 0)]
        public void Test(string s, int expected)
        {
            var sut = new Template();
            var result = sut.maxProduct(s.ToIntArray().ToList());
            Assert.AreEqual(expected, result);
        }

        public class Holder
        {
            public int Max { get; set; }
            public int Min { get; set; }
        }

        public int maxProduct(List<int> A)
        {
            // max product of i to j for all i<j
            // 2, 3
            // 2, 3, -2     3, -2     -2
            var max = int.MinValue;
            var min = int.MaxValue;
            var store = new Holder[A.Count];

            for (var i = 0; i < A.Count; i++)
            {
                var localMin = A[i]; // 10
                var localMax = A[i]; // 10
                if (i > 0)
                {
                    var small = A[i] * store[i - 1].Min; // -100 * 10 = -1000
                    var large = A[i] * store[i - 1].Max; // 1000 * 10 = 10000
                    localMax = Math.Max(localMax, Math.Max(small, large)); // 10000
                    localMin = Math.Min(localMin, Math.Min(small, large)); // -1000
                }

                //localMax = Math.Max(localMax, localMax * A[i]); // 10000
                //localMin = Math.Min(localMin, localMin * A[i]); // -1000
                store[i] = new Holder() { Max = localMax, Min = localMin }; // 10000 -1000
                max = Math.Max(localMax, max); // 10000
            }


            // var store = new int[A.Count][];
            // for(var i = 0; i < A.Count; i++)
            // {
            //     var product = 1;
            //     store[i] = new int[A.Count];
            //     for (var j = i; j < A.Count; j++)
            //     {
            //         product *= A[j];
            //         store[i][j] = product;
            //         max = Math.Max(max, product);
            //     }
            // }

            return max;
        }
    }

}