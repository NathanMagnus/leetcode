using NUnit.Framework;
using System;
using System.Linq;

namespace LeetCode
{
    // Difficulty: Easy
    public class Problem0021_MergeTwoSortedLists
    {
        [Test]
        [TestCase("1,2,4", "1,3,4", "1,1,2,3,4,4")]
        public void Test(string l1, string l2, string expected)
        {
            var sut = new Problem0021_MergeTwoSortedLists();

            var l1Nodes = l1.ToIntArray().Select(i => new ListNode(i)).ToArray();
            var l2Nodes = l2.ToIntArray().Select(i => new ListNode(i)).ToArray();

            for (var i = 0; i < l1Nodes.Length - 1; i++)
                l1Nodes[i].next = l1Nodes[i + 1];
            for (var i = 0; i < l2Nodes.Length - 1; i++)
                l2Nodes[i].next = l2Nodes[i + 1];

            var result = sut.MergeTwoLists(l1Nodes[0], l2Nodes[0]);
            
            var str = "";
            var item = result;
            while(item != null)
            {
                str = str + item.val + ",";
                item = item.next;
            }
            Assert.AreEqual(expected, str.Substring(0, str.Length - 1));
        }

        public ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            var l1Next = l1;
            var l2Next = l2;
            var currentNode = new ListNode();
            var firstNode = currentNode;
            while (l1Next != null || l2Next != null)
            {
                var takeL1 = l2Next == null || (l1Next != null && l1Next.val < l2Next.val);
                currentNode.next = takeL1 ? new ListNode(l1Next.val) : new ListNode(l2Next.val);
                currentNode = currentNode.next;
                if(takeL1)
                    l1Next = l1Next.next;
                else
                    l2Next = l2Next.next;
            }

            return firstNode.next;
        }
    }
}