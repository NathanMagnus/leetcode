using NUnit.Framework;
using System;

namespace LeetCode
{
    public class Template
    {
        [Test]
        [TestCase("bb", "bb")]
        public void Test(string s, string expected)
        {
            var sut = new Template();
            var result = sut.MethodName(s);
            Assert.AreEqual(expected, result);
        }

        public string MethodName(string s)
        {
            return String.Empty;
        }
    }
}