using NUnit.Framework;
using System;

namespace LeetCode
{
    // Difficulty: Medium
    public class Problem0005_LongestPalindromicSubstring
    {
        [Test]
        [TestCase("bb", "bb")]
        [TestCase("ccc", "ccc")]
        [TestCase("babad", "bab")]
        [TestCase("babaddtattarrattatddetartrateedredividerb", "ddtattarrattatdd")]
        [TestCase("zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz", "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz")]
        public void Test(string s, string expected)
        {
            var sut = new Problem0005_LongestPalindromicSubstring();
            var result = sut.LongestPalindrome(s);
            Assert.AreEqual(expected, result);
        }

        public string LongestPalindrome(string s)
        {
            var longestPalindrome = s[0].ToString();
            for (int i = 0; i < s.Length; i++)
            {
                longestPalindrome = GetLongest(LongestPalindromeInternal(GetLeft(s, i), s[i].ToString(), GetRight(s, i)), longestPalindrome);
                if (i < s.Length - 1 && s[i] == s[i+1])
                    longestPalindrome = GetLongest(LongestPalindromeInternal(GetLeft(s, i), s.Substring(i, 2), GetRight(s, i + 1)), longestPalindrome);
            }

            return $"{longestPalindrome}";
        }

        private static string GetLongest(string str1, string str2)
            => (str1?.Length ?? 0) > (str2?.Length ?? 0) ? str1 : str2;

        private static string GetLeft(string s, int position)
            => s.Substring(0, Math.Max(0, position));

        private static string GetRight(string s, int position)
            => s.Substring(Math.Min(position + 1, s.Length));

        private static string LongestPalindromeInternal(string left, string mid, string right)
        {
            if (left.Length <= 0 || right.Length <= 0 || left[left.Length - 1] != right[0])
                return mid;

            var newLeft = GetLeft(left, left.Length - 1);
            var newMid = left[left.Length - 1] + mid + right[0];
            var newRight = GetRight(right, 0);

            return LongestPalindromeInternal(newLeft, newMid, newRight);
        }

        private static bool IsPalindrome(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != s[s.Length - 1 - i])
                    return false;
            }
            return true;
        }
    }
}