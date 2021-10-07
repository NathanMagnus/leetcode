using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode
{
    public class Problem10_RegularExpressionMatching
    {
        [Test]
        [TestCase("bbbba", ".*a*a", true)]
        [TestCase("aaaaaaaaaaaaab", "a*a*a*a*a*a*a*a*a*a*c", false)]
        [TestCase("aaa", "aaaa", false)]
        [TestCase("aa", "a", false)]
        [TestCase("aaa", "a*a", true)]
        [TestCase("aaa", "ab*a*c*a", true)]
        [TestCase("aa", "a*", true)]
        [TestCase("ab", ".*", true)]
        [TestCase("aab", "c*a*b", true)]
        [TestCase("mississippi", "mis*is*p", false)]
        [TestCase("mississippi", "mis*is*ip*.", true)]
        public void Test(string s, string p, bool expected)
        {
            var sut = new Problem10_RegularExpressionMatching();

            var result = sut.IsMatch(s, p);

            Assert.AreEqual(expected, result);
        }

        private bool IsMatch(string s, string p)
        {
            if(p.Length == 0)
                return s.Length == 0;
            
            var charMatches = s.Length > 0 && (s[0] == p[0] || p[0] == '.');
            if (p.Length > 1 && p[1] == '*')
                return IsMatch(s, p.Substring(2)) || (charMatches && IsMatch(s.Substring(1), p));
            return charMatches && IsMatch(s.Substring(1), p.Substring(1));

        }

        private bool IsMatch2(string s, string p)
        {
            var tokens = Tokenize(p).ToArray();
            return IsMatchInternal(s, tokens);
        }

        private IEnumerable<Token> Tokenize(string p)
        {
            for (var i = 0; i < p.Length; i++)
            {
                if (p[i] == '*')
                    continue;

                yield return new Token(p[i], (i < p.Length - 1 && p[i + 1] == '*'));
            }
        }

        private class Token
        {
            public char Value { get; }
            public bool ZeroOrMore { get; }

            public Token(char value, bool zeroOrMore)
            {
                Value = value;
                ZeroOrMore = zeroOrMore;
            }

            public bool Matches(string s)
                => (s.Length == 1 && (s == Value.ToString() || Value == '.'))
                    || (ZeroOrMore && s.All(v => v == Value || Value == '.'));
        }

        private bool IsMatchInternal(string s, Token[] tokens)
        {
            if (s.Length == 0)
                return tokens.All(t => t.ZeroOrMore);
            if (tokens.Length == 0)
                return s.Length == 0;

            var token = tokens[0];
            var c = s[0];
            if (token.Matches(c.ToString()))
            {
                if (token.ZeroOrMore && (IsMatchInternal(s.Substring(1), tokens) || IsMatchInternal(s, tokens[1..])))
                    return true;
                return IsMatchInternal(s.Substring(1), tokens[1..]);
            }
            else
            {
                if(token.ZeroOrMore)
                    return IsMatchInternal(s, tokens[1..]);
            }
            return false;
        }

        public bool IsMatch1(string s, string p)
        {
            var stringIndex = 0;
            for (var patternIndex = 0; patternIndex < p.Length; patternIndex++)
            {
                var (result, resultStringIndex) = IsPatternMatch1(s, p, stringIndex, patternIndex);
                if (!result)
                    return false;
                stringIndex = resultStringIndex;
            }

            return (stringIndex == s.Length);
        }

        private (bool, int) IsPatternMatch1(string s, string p, int stringIndex, int patternIndex)
        {
            if (stringIndex >= s.Length)
                return (false, stringIndex);

            var patternChar = p[patternIndex];
            if (patternChar == '*')
            {
                while (stringIndex < s.Length && IsCharMatch(s[stringIndex], p[patternIndex - 1]))
                    stringIndex++;
                return (true, stringIndex);
            }
            if (IsCharMatch(s[stringIndex], patternChar))
                return (true, stringIndex + 1);

            if (patternIndex > 0 && p[patternIndex - 1] == '*')
                return (true, stringIndex - 1);

            return (patternIndex < p.Length - 1 && p[patternIndex + 1] == '*', stringIndex);
        }

        private bool IsCharMatch(char v1, char v2)
            => v1 == v2 || v2 == '.';
    }
}