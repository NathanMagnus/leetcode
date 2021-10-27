using NUnit.Framework;
using System;
using System.Linq;

namespace LeetCode
{
    // Difficulty: Hard
    // Time: 
    public class Problem391_PerfectRectangle
    {
        [Test]
        [TestCase("[[1,1,3,3],[3,1,4,2],[3,2,4,4],[1,3,2,4],[2,3,3,4]]", true)]
        [TestCase("[[1,1,2,3],[1,3,2,4],[3,1,4,2],[3,2,4,4]]", false)]
        [TestCase("[[1,1,3,3],[3,1,4,2],[1,3,2,4],[3,2,4,4]]", false)]
        [TestCase("[[1,1,3,3],[3,1,4,2],[1,3,2,4],[2,2,4,4]]", false)]
        //[TestCase("", true)]
        public void Test(string s, bool expected)
        {
            var split = s[2..^2]
                .Split("],[")
                .Select(points => points.Split(","))
                .Select(items => items.Select(int.Parse).ToArray())
                .ToArray();

            var sut = new Problem391_PerfectRectangle();
            var result = sut.IsRectangleCover(split);
            Assert.AreEqual(expected, result);
        }

        public bool IsRectangleCover(int[][] rectangles)
        {
            var rectangleObjects = rectangles
                .Select(points => (new Point(points[0], points[1]), new Point(points[2], points[3])))
                .Select(points => new Rectangle(points.Item1, points.Item2))
                .ToArray();

            //TODO: If any rectangles intersect, then false
            //TODO: For each rect, make sure there is one that picks off where it left off

            //var minX = rectangleObjects.Min(rect => rect.BottomLeft.X);
            //var maxX = rectangleObjects.Max(rect => rect.TopRight.X);
            //var minY = rectangleObjects.Min(rect => rect.BottomLeft.Y);
            //var maxY = rectangleObjects.Max(rect => rect.TopRight.Y);

            //for (double x = minX + 0.5; x < maxX; x++)
            //{
            //    for (double y = minX + 0.5; y < maxY; y++)
            //    {
            //        var inRectangleCount = GetRectangleContainCount(rectangleObjects, new Point(x, y));
            //        if (IsOnEdge(rectangleObjects, new Point(x, y)) && inRectangleCount > 1)
            //            inRectangleCount--;
            //        if (inRectangleCount != 1)
            //            return false;
            //    }
            //}

            return true;
        }

        private bool IsOnEdge(Rectangle[] rectangleObjects, Point point) 
            => rectangleObjects
                .Any(rect => rect.BottomLeft.Y == point.Y || rect.BottomLeft.Y == point.Y || rect.TopRight.X == point.X || rect.TopRight.Y == point.Y);

        private int GetRectangleContainCount(Rectangle[] rectangles, Point point)
            => rectangles
            .Where(rect => RectangleContains(rect, point))
            .Count();

        private bool RectangleContains(Rectangle rect, Point point)
            => rect.BottomLeft.X <= point.X && rect.TopRight.X >= point.X
                && rect.BottomLeft.Y <= point.Y && rect.TopRight.Y >= point.Y;

        private Point GetBottomLeft(Rectangle[] rectangleObjects)
        {
            var minX = rectangleObjects.Min(obj => obj.BottomLeft.X);
            return rectangleObjects.First(rect => rect.BottomLeft.X == minX).BottomLeft;
        }
        private Point GetTopRight(Rectangle[] rectangleObjects)
        {
            var maxY = rectangleObjects.Max(obj => obj.TopRight.Y);
            return rectangleObjects.First(rect => rect.TopRight.Y == maxY).TopRight;
        }

        private class Rectangle
        {
            public Rectangle(Point bottomLeft, Point topRight)
            {
                BottomLeft = bottomLeft;
                TopRight = topRight;
                BottomRight = new Point(TopRight.X, BottomLeft.Y);
                TopLeft = new Point(BottomLeft.X, TopRight.Y);
            }

            public Point BottomLeft { get; }
            public Point TopRight { get; }
            public Point TopLeft { get; }
            public Point BottomRight { get; }
        }

        private class Point
        {
            public Point(double x, double y)
            {
                X = x;
                Y = y;
            }

            public double X { get; }
            public double Y { get; }
        }
    }
}