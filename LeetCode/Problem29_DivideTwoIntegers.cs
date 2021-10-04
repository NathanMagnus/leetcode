using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LeetCodes
{
    public class Problem29_DivideTwoIntegers
    {
        [Test]
        [TestCase(7, -3, -2)]
        [TestCase(2147483647, 3, 715827882)]
        [TestCase(10, 3, 3)]
        [TestCase(-1, -1, 1)]
        [TestCase(-1, 1, -1)]
        [TestCase(int.MinValue, -1, int.MaxValue)]
        public void Test(int i1, int i2, int expected)
        {
            var sut = new Problem29_DivideTwoIntegers();
            var result = sut.Divide(i1, i2);
            Assert.AreEqual(expected, result);
        }

        public int Divide(int dividend, int divisor)
        {
            try
            {
                var count = 0;
                var value = dividend;
                var div = divisor;

                var lastC = div.ToString()[div.ToString().Length - 1];
                while (lastC == '2' || lastC == '4' || lastC == '6' || lastC == '8')
                {
                    div >>= 1;
                    value >>= 1;
                    lastC = div.ToString()[div.ToString().Length - 1];
                    count += div > 0 ? 2 : -2;
                }

                if (div == 1)
                    return value;
                if (div == -1 && dividend > int.MinValue)
                    return -value;
                if (div == -1 && dividend == int.MinValue)
                    return int.MaxValue;


                while (
                    ((dividend < 0 && div > 0) && value <= -div)
                    || ((dividend > 0 && div < 0) && value >= Math.Abs(div))
                    || ((dividend > 0 && div > 0) && value >= div)
                    || ((dividend < 0 && div < 0) && value <= div)
                )
                {
                    if (dividend < 0 && div > 0)
                    {
                        value += div;
                        count--;
                    }
                    else if (dividend > 0 && div < 0)
                    {
                        value += div;
                        count--;
                    }
                    else
                    {
                        value -= div;
                        count = checked(count + 1);
                    }
                }
                return count;
            }
            catch (OverflowException ex)
            {
                return int.MaxValue;
            }

        }
    }
}