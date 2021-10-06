using NUnit.Framework;
using System;
using System.Linq;

namespace LeetCodes
{
    public class Problem45_JumpGameII
    {
        [Test]
        [TestCase("2,3,1,1,4", 2)]
        [TestCase("2,3,0,1,4", 2)]
        public void Test(string s, int expected)
        {
            var inputArray = s
                .Split(",")
                .Select(x => int.Parse(x))
                .ToArray();

            var sut = new Problem45_JumpGameII();
            var result = sut.Jump(inputArray);
            Assert.AreEqual(expected, result);
        }

        public int Jump(int[] nums)
        {
            var arr = new int[nums.Length];
            for (var i = nums.Length - 1; i >= 0; i--)
            {
                if (i == nums.Length - 1)
                    arr[i] = 0;
                else
                    arr[i] = GetMin(i, nums, arr);
            }
            return arr[0];
        }

        private int GetMin(int i, int[] nums, int[] arr)
        {
            var min = int.MaxValue;
            for (var count = 1; count <= nums[i]; count++)
            {
                if (i + count >= nums.Length)
                    break;
                min = Math.Min(min, arr[i + count]);
            }
            return min == int.MaxValue ? int.MaxValue : min + 1;
        }
    }
}