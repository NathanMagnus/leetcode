using NUnit.Framework;
using System;
using System.Linq;
using System.Text;

namespace LeetCode
{
    // Difficulty: Easy
    public class Problem0014_LongestCommonPrefix
    {
        [Test]
        [TestCase("flower,flow,flight", "fl")]
        [TestCase("dog,racecar,car", "")]
        public void Test(string s, string expected)
        {
            var sut = new Problem0014_LongestCommonPrefix();

            var strings = s.ToStringArray();

            var result = sut.LongestCommonPrefix(strings);
            Assert.AreEqual(expected, result);
        }

        public string LongestCommonPrefix(string[] strings)
        {
            var builder = new StringBuilder();
            var minLength = strings.Min(x => x.Length);
            for(var i = 0; i < minLength; i++)
            {
                var c = strings[0][i];
                if(!strings.All(str => str[i] == c))
                    break;
                builder.Append(c);
            }

            return builder.ToString();
        }
    }
}