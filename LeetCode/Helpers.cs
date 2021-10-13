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
                nodes[i] = nodes[i + 1];
            return nodes.Length == 0 ? null : nodes[0];
        }

        public static ListNode ToListNodeLinkedList(this string str, char delimiter = ',')
            => str.ToIntArray(delimiter).ToListNodeLinkedList();
    }
}