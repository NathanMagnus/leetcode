using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode
{
    // Difficulty: Hard
    // Time: ??
    public class Problem1444_NumberOfWaysOfCuttingAPizza
    {
        [Test]
        [TestCase("A..|AAA|...", 3, 3)]
        [TestCase("A..|AA.|...", 3, 1)]
        [TestCase("A..|A..|...", 1, 1)]
        public void Test(string s, int k, int expected)
        {
            var ways = s.ToStringArray('|');

            var sut = new Problem1444_NumberOfWaysOfCuttingAPizza();
            var result = sut.Ways(ways, k);
            Assert.AreEqual(expected, result);
        }

        private readonly int _mod = (int)Math.Pow(10, 9) + 7;
        private readonly IDictionary<(int Row, int Col, int CutsLeft), int> _memory = new Dictionary<(int Row, int Col, int CutsLeft), int>();

        public int Ways(string[] pizza, int k)
        {
            if (k == 0 || pizza.Length + pizza[0].Length < k)
                return 0;

            var apples = new int[pizza.Length][];
            for (var i = pizza.Length - 1; i >= 0; i--)
            {
                apples[i] = new int[pizza[i].Length];
                for (var j = pizza[i].Length - 1; j >= 0; j--)
                {
                    if (i < apples.Length - 1)
                        apples[i][j] += apples[i + 1][j];
                    if (j < apples[i].Length - 1)
                        apples[i][j] += apples[i][j + 1];
                    if (i < apples.Length - 1 && j < apples[i].Length - 1)
                        apples[i][j] -= apples[i + 1][j + 1];
                    if (pizza[i][j] == 'A')
                        apples[i][j]++;
                }
            }

            return Cut(pizza, apples, k);
        }

        private int Cut(string[] pizza, int[][] apples, int k)
        {
            if (k <= 1)
                return apples[0][0] > 0 ? 1 : 0;
            
            var key = (pizza.Length, pizza[0].Length, k);
            if(_memory.ContainsKey(key))
                return _memory[key];

            var count = 0;
            for (var i = 1; i < pizza.Length; i++)
            {
                if (apples[0][0] - apples[i][0] > 0)
                    count += Cut(pizza[i..], apples[i..], k - 1);
            }
            for (var j = 1; j < pizza[0].Length; j++)
            {
                if (apples[0][0] - apples[0][j] > 0)
                    count += Cut(pizza.Select(r => r[j..]).ToArray(), apples.Select(a => a[j..]).ToArray(), k - 1);
            }

            _memory[key] = count % _mod;
            return count;
        }
    }
}