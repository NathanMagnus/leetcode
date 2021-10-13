using NUnit.Framework;
using System;
using System.Linq;

namespace LeetCode
{
    // Difficulty: Medium
    public class Problem0002_AddTwoNumbers
    {
        [Test]
        [TestCase("2,4,3", "5,6,4", "7,0,8")]
        public void Test(string l1String, string l2String, string expected)
        {
            var l1 = l1String.ToListNodeLinkedList();
            var l2 = l2String.ToListNodeLinkedList();

            var sut = new Problem0002_AddTwoNumbers();
            var result = sut.AddTwoNumbers(l1, l2);
            Assert.AreEqual(expected, result);
        }

        private static ListNode _zero = new ListNode();
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            return AddTwoNumbersInternal(l1, l2, 0);
        }

        public ListNode AddTwoNumbersInternal(ListNode l1, ListNode l2, int carryOver)
        {
            if (l1 == null && l2 == null && carryOver == 0)
                return null;

            var result = (l1?.val ?? 0) + (l2?.val ?? 0) + carryOver;
            return new ListNode(result % 10, AddTwoNumbersInternal(l1?.next, l2?.next, result / 10));
        }
    }
}