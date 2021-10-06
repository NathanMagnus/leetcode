using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodes
{
    public class Problem442_FindAllDuplicatesInAnArray
    {
        [Test]
        [TestCase("4,3,2,7,8,2,3,1", "2,3")]
        public void Test(string s, string expected)
        {
            var inputArray = s
                .Split(",")
                .Select(x => int.Parse(x))
                .ToArray();
            
            var sut = new Problem442_FindAllDuplicatesInAnArray();
            var result = sut.FindDuplicates(inputArray).ToArray();

            var expectedArray = expected
                .Split(",")
                .Select(x => int.Parse(x));

            Assert.AreEqual(expectedArray, result);
        }

        public IList<int> FindDuplicates(int[] nums)
        {
            var max = nums.Max();
            var counts = new int[max + 1];
            
            foreach(var num in nums)
                counts[num]++;

            var list = new List<int>();
            for(var i = 0; i < counts.Length; i++)
            {
                if(counts[i] > 1)
                    list.Add(i);
            }
            return list;
        }
    }
}