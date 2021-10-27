using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode
{
    // Difficulty: Hard
    // Time: Started 7:55
    public class Problem149_MaxPointsOnALine
    {
        [Test]
        [TestCase("[1,1],[2,2],[3,3]", 3)]
        [TestCase("[1,1],[3,2],[5,3],[4,1],[2,3],[1,4]", 4)]
        [TestCase("[0,0]", 1)]
        [TestCase("[1,0],[0,0]", 2)]
        [TestCase("[1,0],[2,2],[-1,-1]", 2)]
        [TestCase("[9,-25],[-4,1],[-7,7]", 3)]
        [TestCase("[7,3],[19,19],[-16,3],[13,17],[-18,1],[-18,-17],[13,-3],[3,7],[-11,12],[7,19],[19,-12],[20,-18],[-16,-15],[-10,-15],[-16,-18],[-14,-1],[18,10],[-13,8],[7,-5],[-4,-9],[-11,2],[-9,-9],[-5,-16],[10,14],[-3,4],[1,-20],[2,16],[0,14],[-14,5],[15,-11],[3,11],[11,-10],[-1,-7],[16,7],[1,-11],[-8,-3],[1,-6],[19,7],[3,6],[-1,-2],[7,-3],[-6,-8],[7,1],[-15,12],[-17,9],[19,-9],[1,0],[9,-10],[6,20],[-12,-4],[-16,-17],[14,3],[0,-1],[-18,9],[-15,15],[-3,-15],[-5,20],[15,-14],[9,-17],[10,-14],[-7,-11],[14,9],[1,-1],[15,12],[-5,-1],[-17,-5],[15,-2],[-12,11],[19,-18],[8,7],[-5,-3],[-17,-1],[-18,13],[15,-3],[4,18],[-14,-15],[15,8],[-18,-12],[-15,19],[-9,16],[-9,14],[-12,-14],[-2,-20],[-3,-13],[10,-7],[-2,-10],[9,10],[-1,7],[-17,-6],[-15,20],[5,-17],[6,-6],[-11,-8]", 6)]
        public void Test(string s, int expected)
        {
            var points = s
                .Split("],[")
                .Select(item => item.Replace(']', ' ').Replace('[', ' ').Trim())
                .Select(item => item.Split(",").Select(x => int.Parse(x)).ToArray())
                .ToArray();

            var sut = new Problem149_MaxPointsOnALine();
            var result = sut.MaxPoints(points);
            Assert.AreEqual(expected, result);
        }

        public int MaxPoints(int[][] points)
        {
            var pointsArray = points
                .Select(point => new Point(point[0], point[1]))
                //.OrderBy(point => point.X).ThenBy(point => point.Y)
                .ToArray();

            var max = 1;
            for (var i = 0; i < pointsArray.Length; i++)
            {
                var slopes = new List<decimal>();
                var point1 = pointsArray[i];
                for (var j = i + 1; j < pointsArray.Length; j++)
                {
                    var point2 = pointsArray[j];
                    var run = (decimal)point2.X - point1.X;
                    var rise = (decimal)point2.Y - point1.Y;
                    //slopes.Add(new Slope(rise, run));
                    slopes.Add(run == 0 ? 0 : rise/run);
                }
                
                var slopeMax = slopes.Any()
                    ? slopes
                        .GroupBy(slope => slope)
                        .Select(g => g.Count() + 1)
                        .Max()
                    : 1;

                max = Math.Max(max, slopeMax);
            }

            return max;

            //var counts = new List<int>();
            //foreach (var slope in slopes)
            //{
            //    for (var i = 0; i < pointsArray.Length; i++)
            //    {
            //        var count = 1;
            //        var point1 = pointsArray[i];
            //        for (var j = i + 1; j < pointsArray.Length; j++)
            //        {
            //            var point2 = pointsArray[j];
            //            if (IsOnSameLine(point1, point2, slope))
            //                count++;
            //        }
            //        counts.Add(count);
            //    }
            //}

            //return counts.Any()
            //    ? counts.Max()
            //    : 1;
        }

        //private static bool IsOnSameLine(Point point1, Point point2, Slope slope)
        //{
        //    var x = point1.X;
        //    var y = point1.Y;

        //    if (slope.Rise == 0 && slope.Run == 0)
        //        return x == point2.X && y == point2.Y;

        //    while (
        //        ((x > point2.X && slope.Run < 0) || (x < point2.X && slope.Run > 0) || (x == point2.X && slope.Run == 0))
        //        && ((y > point2.Y && slope.Rise < 0) || (y < point2.Y && slope.Rise > 0) || (y == point2.Y && slope.Rise == 0))
        //    )
        //    {
        //        x += slope.Run;
        //        y += slope.Rise;
        //    }

        //    var result = x == point2.X && y == point2.Y;
        //    return result;
        //}

        private class Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        //private class Slope
        //{
        //    public int Rise { get; set; }
        //    public int Run { get; set; }
        //    public decimal SlopeDecimal { get; }

        //    public Slope(int rise, int run)
        //    {
        //        var lcd = GetGCD(rise, run);

        //        Rise = rise / lcd;
        //        Run = run / lcd;

        //        SlopeDecimal = Rise / Run;
        //    }

        //    private int GetGCD(int num1, int num2)
        //    {
        //        num1 = Math.Abs(num1);
        //        num2 = Math.Abs(num2);
        //        while (num1 != num2)
        //        {
        //            if (num1 > num2)
        //                num1 = num1 - num2;

        //            if (num2 > num1)
        //                num2 = num2 - num1;
        //        }

        //        return num1;
        //    }

        //    public override int GetHashCode()
        //        => HashCode.Combine(Rise, Run);

        //    public override bool Equals(object obj)
        //        => obj is Slope slope && slope.Rise == Rise && slope.Run == Run;
        //}
    }
}