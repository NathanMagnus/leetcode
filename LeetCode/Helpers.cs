using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
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

        public static bool AreEquivalent<T>(IEnumerable<T> e1, IEnumerable<T> e2)
        {
            Assert.AreEqual(e1.Count(), e2.Count());
            if (typeof(T).GetInterfaces().Contains(typeof(IEnumerable)) && typeof(T) != typeof(string))
            {
                for (var i = 0; i < e1.Count(); i++)
                {
                    var success = false;
                    foreach(var e2Element in e2)
                        success |= AreEquivalent((dynamic)e1.ElementAt(i), (dynamic)e2Element);
                    if(!success)
                        return false;
                }
                return true;
            }
            else
                return e1.SequenceEqual(e2);
        }

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