using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LeetCodes
{
    public class Problem91_DecodeWays
    {
        [Test]
        [TestCase("12", 2)]
        [TestCase("226", 3)]
        [TestCase("0", 0)]
        [TestCase("06", 0)]
        [TestCase("2101", 1)]
//        [TestCase("111111111111111111111111111111111111111111111", 0)]
        public void Test(string s, int expected)
        {
            var sut = new Problem91_DecodeWays();
            var result = sut.NumDecodings(s);
            Assert.AreEqual(expected, result);
        }

        private static int GetInt(string s)
            => int.Parse(s);

        public int NumDecodings3(string s)
        {
            var arr = new int[s.Length];
            for (var i = s.Length - 1; i >= 0; i--)
            {
                var first = GetInt(s[i].ToString());
                if (!IsValid(first) && !(i > 0 && IsValid(GetInt(s.Substring(i - 1, 2)))))
                {
                    arr[i] = 0;
                    continue;
                }
                arr[i] = (i < s.Length - 1 ? arr[i + 1] : 1);
                if (i < s.Length - 1)
                {
                    var second = GetInt(s.Substring(i, 2));
                    if (IsValid(second))
                    {
                        arr[i] = (i < s.Length - 1 ? arr[i + 1] : 1);
                        if (IsValid(first))
                            arr[i]++;
                    }
                }
            }
            return arr[0];
        }

        private static bool IsValid(int i)
            => i > 0 && i <= 26;

        public int NumDecodings(string s)
        {
            return NumDecodingsInternal(s, new int?[s.Length]);
        }

        public int NumDecodingsInternal(string s, int?[] arr)
        {
            if (String.IsNullOrWhiteSpace(s))
                return 1;
            if (arr[arr.Length - s.Length] != null)
                return arr[arr.Length - s.Length].Value;

            var count = 0;
            var firstChar = GetInt(s[0].ToString());
            if (firstChar != 0)
            {
                count += NumDecodingsInternal(s.Substring(1), arr);
            }
            if (s.Length > 1)
            {
                var secondChar = GetInt(s.Substring(0, 2));
                if (secondChar != 0 && secondChar <= 26 && firstChar != 0)
                {
                    count += NumDecodingsInternal(s.Substring(2), arr);
                }
            }
            arr[arr.Length - s.Length] = count;
            return count;
        }
    }
}