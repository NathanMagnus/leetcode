using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode
{
    // Difficulty: 
    // Time: 
    public class Problem1444_NumberOfWaysOfCuttingAPizza
    {
        [Test]
        [TestCase("A..|AAA|...", 3, 3)]
        [TestCase("A..|AA.|...", 3, 1)]
        [TestCase("A..|A..|...", 3, 1)]
        public void Test(string s, int k, int expected)
        {
            var ways = s.ToStringArray('|');

            var sut = new Problem1444_NumberOfWaysOfCuttingAPizza();
            var result = sut.Ways(ways, k);
            Assert.AreEqual(expected, result);
        }

        public int Ways(string[] pizza, int k)
        {
            var grid = new int[pizza.Length][];
            for (var i = 0; i < pizza.Length; i++)
                grid[i] = new int[pizza[i].Length];

            var max = 0;

            for (var i = grid.Length - 1; i >= 0; i--)
            {
                for (var j = grid[i].Length - 1; j >= 0; j--)
                {
                    var horizontalCutValid = ContainsApple(pizza[..i]) // row and before
                        && ContainsApple(pizza[(i + 1)..]); // after row
                    var verticalCutValid = ContainsApple(pizza[i..].Select(row => row[..j])) // column and before
                        && ContainsApple(pizza[i..].Select(piece => piece[(j + 1)..]).ToArray()); // column and after
                    if (horizontalCutValid)
                        grid[i][j]++;
                    if (verticalCutValid)
                        grid[i][j]++;
                }
            }

            return max;
        }

        private bool ContainsApple(IEnumerable<string> pizza)
            => pizza.Any(row => row.Contains('A'));

        //private int ComputeCuts(string[] pizza, int row, int col, int k)
        //{
        //    for(var i = row; i < pizza.Length; i++)
        //    {
        //        for(var j = col; j < pizza[i].Length; j++)
        //        {
        //            if()
        //        }
        //    }
        //}
    }
}