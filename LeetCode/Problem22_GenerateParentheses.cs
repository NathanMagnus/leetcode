using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode
{
    public class Problem22_GenerateParentheses
    {
        [Test]
        [TestCase(1, "()")]
        [TestCase(3, "((()))", "(()())", "(())()", "()(())", "()()()")]
        public void Test(int n, params string[] expected)
        {
            var sut = new Problem22_GenerateParentheses();
            var result = sut.GenerateParenthesis(n).ToList();

            foreach(var e in expected)
            {
                if(!result.Remove(e))
                    throw new Exception("Could not remove " + e);
            }
            Assert.AreEqual(0, result.Count);
        }

        public IList<string> GenerateParenthesis(int n)
            => GenerateParenthesisInternal(n).Distinct().ToList();

        public IEnumerable<string> GenerateParenthesisInternal(int n)
        {
            if (n == 1)
            {
                return new[] { "()" }.AsEnumerable();
            }

            if (n == 2)
            {
                return new[] { "()()", "(())" }.AsEnumerable();
            }

            var items = new System.Collections.Generic.List<string>(Array.Empty<string>());
            AddToList(items, GenerateParenthesisInternal(n - 1).Select(x => $"({x})"));

            if (n > 2)
            {
                AddToList(items, GenerateParenthesisInternal(n - 1).SelectMany(x => GenerateParenthesisInternal(n - 2).Select(y => $"{x}{y}")));
                AddToList(items, GenerateParenthesisInternal(n - 2).SelectMany(x => GenerateParenthesisInternal(n - 1).Select(y => $"{x}{y}")));
                AddToList(items, GenerateParenthesisInternal(n - 2).SelectMany(x => GenerateParenthesisInternal(n - 2).Select(y => $"(){x}{y}")));
            }

            return items.AsEnumerable();
        }

        private static void AddToList(List<string> l, IEnumerable<string> str)
        {
            foreach(var s in str)
                l.Add(s);
        }
    }
}