using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

namespace LeetCode
{
    public class Problem65_ValidNumber
    {
        [Test]
        [TestCase("0", true)]
        [TestCase("e", false)]
        [TestCase(".", false)]
        [TestCase(".1", true)]
        [TestCase("2e0", true)]
        [TestCase(".8+", false)]
        [TestCase("44e016912630333", true)]
        [TestCase("1E9", true)]
        public void Test(string s, bool expected)
        {
            var sut = new Problem65_ValidNumber();
            var result = sut.IsNumber(s);
            Assert.AreEqual(expected, result);
        }

        public bool IsNumber(string s)
        {
            var ePosition = s.ToLower().IndexOf('e');

            if (ePosition == s.Length - 1)
                return false;

            var frontString = ePosition > 0 ? s[..ePosition] : s;
            var backString = ePosition > 0 && ePosition < s.Length - 1 ? s[(ePosition + 1)..] : String.Empty;

            if(!IsValidString(frontString))
                return false;


            if (!String.IsNullOrWhiteSpace(frontString) && !decimal.TryParse(frontString, out var beforeEValue))
                return false;

            if (!String.IsNullOrWhiteSpace(backString) && !Int64.TryParse(backString, out var afterEValue))
                return false;


            return true;
        }

        private bool IsValidString(string frontString)
            => new System.Text.RegularExpressions.Regex(@"^([+-]*([0-9]*(\.)*[0-9]*)|([0-9]+(\.)*[0-9]*)((e|E)*[0-9]+)*)$").IsMatch(frontString);
    }
}