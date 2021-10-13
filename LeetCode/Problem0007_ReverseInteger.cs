using NUnit.Framework;
using System;
using System.Linq;

namespace LeetCode
{
    // Difficulty: Medium
    public class Problem0007_ReverseInteger
    {
        [Test]
        [TestCase(123, 321)]
        [TestCase(-123, -321)]
        [TestCase(120, 21)]
        [TestCase(0, 0)]
        public void Test(int s, int expected)
        {
            var sut = new Problem0007_ReverseInteger();
            var result = sut.Reverse(s);
            Assert.AreEqual(expected, result);
        }

        public int Reverse(int x)
        {
            var str = x.ToString();

            var negative = str[0] == '-';
            if(negative)
                str = str.Substring(1);

            var newStr = new String(str.Reverse().ToArray());
            return int.TryParse(newStr, out var returnValue) ? returnValue * (negative ? -1 : 1) : 0;
        }
    }
}