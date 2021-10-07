using NUnit.Framework;
using System;
using System.Linq;

namespace LeetCode
{
    public class Problem292_NimGame
    {
        [Test]
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(3, true)]
        [TestCase(4, false)]
        [TestCase(5, true)]
        [TestCase(6, true)]
        [TestCase(7, true)]
        [TestCase(8, false)]
        [TestCase(1348820612, false)]
        public void Test(int n, bool expected)
        {
            var sut = new Problem292_NimGame();
            var result = sut.CanWinNim(n);
            Assert.AreEqual(expected, result);
        }

        public bool CanWinNim(int n)
            => n % 4 != 0;

        //public bool CanWinNim(int n)
        //{
        //    var previous = new bool[3];
        //    var canWin = false;
        //    for (var i = 1; i < n + 1; i++)
        //    {
        //        if (i <= 3)
        //        {
        //            canWin = true;
        //            previous[i - 1] = true;
        //            continue;
        //        }
        //        if(i == 4)
        //        {
        //            canWin = false;
        //            ShiftPrevious(previous, canWin);
        //            continue;
        //        }

        //        canWin = previous.Any(p => !p);
        //        ShiftPrevious(previous, canWin);
        //    }

        //    return canWin;
        //}

        //private static void ShiftPrevious(bool[] previous, bool newValue)
        //{
        //    previous[0] = previous[1];
        //    previous[1] = previous[2];
        //    previous[2] = newValue;
        //}
    }
}