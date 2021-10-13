using NUnit.Framework;
using System;
using System.Linq;

namespace LeetCode
{
    // Difficulty: Easy
    public class Problem0001_TwoSum
    {
        [Test]
        [TestCase("2,7,11,15", 9, "0,1")]
        [TestCase("3,2,4", 6, "1,2")]
        [TestCase("3,3", 6, "0,1")]
        public void Test(string numsString, int target, string expectedString)
        {
            var nums = numsString.ToIntArray();
            var sut = new Problem0001_TwoSum();
            var result = sut.TwoSum(nums, target);

            var expected = expectedString.ToIntArray();
            Assert.AreEqual(expected, result);
        }

        public int[] TwoSum(int[] nums, int target)
        {
            for (var i = 0; i < nums.Length - 1; i++)
            {
                for (var j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] + nums[j] == target)
                        return new[] { i, j };
                }
            }
            throw new Exception("Shouldn't get here");
        }
    }
}