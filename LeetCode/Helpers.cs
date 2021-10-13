using NUnit.Framework;
using System.Linq;

namespace LeetCode
{
    public static class Helpers
    {
        public static int[] ToIntArray(this string str, char delimiter = ',')
            => str.Split(delimiter).ToIntArray();

        public static int[] ToIntArray(this string[] strs)
            => strs.Select(n => int.Parse(n)).ToArray();

        public static string[] ToStringArray(this string str, char delimiter = ',')
            => str.Split(delimiter).ToArray();

        public static ListNode ToListNodeLinkedList(this int[] ints)
        {
            var nodes = ints.Select(i => new ListNode(i)).ToArray();
            for (var i = 0; i < nodes.Length - 1; i++)
                nodes[i].next = nodes[i + 1];
            return nodes.Length == 0 ? null : nodes[0];
        }

        public static ListNode ToListNodeLinkedList(this string str, char delimiter = ',')
            => str.ToIntArray(delimiter).ToListNodeLinkedList();

        public static void AssertLinkedListsAreEqual(params ListNode[] lists)
        {
            var currents = new ListNode[lists.Length];

            for (var i = 0; i < lists.Length; i++)
                currents[i] = lists[i];

            while (currents.All(x => x != null))
            {
                for (var i = 0; i < lists.Length; i++)
                {
                    if (i < lists.Length - 1)
                        Assert.AreEqual(currents[i].val, currents[i + 1].val);

                    currents[i] = currents[i].next;
                }
            }

            Assert.True(currents.All(x => x == null));
        }
    }
}