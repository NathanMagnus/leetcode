using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode
{
    // Difficulty: Hard
    public class Problem0004_MedianOfTwoSoftedArrays
    {
        [Test]
        [TestCase("1,3", "2", 2.0)]
        public void Test(string nums1String, string nums2String, decimal expected)
        {
            var nums1 = nums1String.ToIntArray();
            var nums2 = nums2String.ToIntArray();

            var sut = new Problem0004_MedianOfTwoSoftedArrays();
            var result = sut.FindMedianSortedArrays(nums1, nums2);
            Assert.AreEqual(expected, result);
        }

        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            var arr = new List<int>(nums1);
            arr.AddRange(nums2);

            var arrArray = arr.ToArray();
            //throw new Exception($"arrArray: {arrArray.Length} --- {String.Join(",", arrArray.Select(i => i.ToString()))}");
            var mid = (arrArray.Length) / 2;
            var merged = MergeSort(arrArray);
            //throw new Exception($"merged: {merged.Length} --- {String.Join(",", merged.Select(i => i.ToString()))}");
            // return median calculated
            if (merged.Length % 2 == 1)
                return merged[merged.Length / 2];

            var v1 = merged[(mid - 1)];
            var v2 = merged[mid];
            //throw new Exception($"{v1} + {v2} / 2");
            return (v1 + v2) / (double)2;
        }

        private static int[] MergeSort(int[] arr)
        {
            if (arr.Length <= 1)
                return arr;

            var mid = (arr.Length) / 2;
            var arr2 = MergeSort(arr[..mid]);

            var arr3 = MergeSort(arr[mid..]);
            var arr4 = Merge(arr2, arr3).ToArray();
            return arr4;
        }

        private static IEnumerable<int> Merge(int[] arr2, int[] arr3)
        {
            var i = 0;
            var j = 0;
            while (i < arr2.Length || j < arr3.Length)
            {
                if (i < arr2.Length && (j >= arr3.Length || arr2[i] <= arr3[j]))
                {
                    yield return arr2[i];
                    i++;
                }
                else
                {
                    yield return arr3[j];
                    j++;
                }
            }
        }
    }
}