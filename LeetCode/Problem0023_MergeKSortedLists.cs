using NUnit.Framework;
using System;
using System.Linq;

namespace LeetCode
{
    //Difficulty: Hard
    //Time: 20 min
    public class Problem0023_MergeKSortedLists
    {
        [Test]
        [TestCase("1,4,5-1,3,4-2,6", "1,1,2,3,4,4,5,6")]
        public void Test(string input, string expected)
        {
            var lists = input.ToStringArray('-').Select(str => str.ToListNodeLinkedList()).ToArray();
            var expectedList = CreateList(expected);

            var sut = new Problem0023_MergeKSortedLists();
            var result = sut.MergeKLists(lists);

            Helpers.AssertLinkedListsAreEqual(expected.ToListNodeLinkedList(), result);
        }

        private ListNode CreateList(string listItems)
        {
            var items = listItems.ToIntArray().Select(item => new ListNode(item)).ToArray();
            for (var i = 0; i < items.Length - 1; i++)
                items[i].next = items[i + 1];
            return items.Length == 0 ? null : items[0];
        }

        public ListNode MergeKLists(ListNode[] lists)
        {
            var currentNodes = new ListNode[lists.Length];
            for (var i = 0; i < lists.Length; i++)
                currentNodes[i] = lists[i];

            ListNode firstNode = null;
            ListNode newList = null;
            while (HaveValues(currentNodes))
            {
                ListNode min = null;
                var currentI = 0;
                for (var i = 0; i < currentNodes.Length; i++)
                {
                    var node = currentNodes[i];
                    if (node == null)
                        continue;
                    if (min == null)
                    {
                        min = node;
                        currentI = i;
                    }
                    else if (node.val < min.val)
                    {
                        min = node;
                        currentI = i;
                    }
                }
                currentNodes[currentI] = currentNodes[currentI].next;

                if (firstNode == null)
                    firstNode = min;
                if (newList == null)
                    newList = min;
                else
                {
                    newList.next = min;
                    newList = min;
                }
            }

            return firstNode;
        }

        private bool HaveValues(ListNode[] lists)
            => lists.Any(list => list != null);
    }
}