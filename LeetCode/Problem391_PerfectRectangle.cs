using NUnit.Framework;
using System;
using System.Linq;

namespace LeetCode
{
    // Difficulty: Hard
    // Time: 30min 
    public class Problem391_PerfectRectangle
    {
        [Test]
        [TestCase("[[1,1,3,3],[3,1,4,2],[3,2,4,4],[1,3,2,4],[2,3,3,4]]", true)]
        [TestCase("[[1,1,2,3],[1,3,2,4],[3,1,4,2],[3,2,4,4]]", false)]
        [TestCase("[[1,1,3,3],[3,1,4,2],[1,3,2,4],[3,2,4,4]]", false)]//
        [TestCase("[[1,1,3,3],[3,1,4,2],[1,3,2,4],[2,2,4,4]]", false)]
        [TestCase("[[0,0,4,1],[0,0,4,1]]", false)]
        [TestCase("[[0,0,4,1],[7,0,8,3],[5,1,6,3],[6,0,7,2],[4,0,5,1],[4,2,5,3],[2,1,4,3],[0,2,2,3],[0,1,2,2],[6,2,8,3],[5,0,6,1],[4,1,5,2]]", false)]
        [TestCase("[[7,0,8,3],[6,2,8,3]]", false)]
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

            var minX = rectangleObjects.Min(rect => rect.BottomLeft.X);
            var maxX = rectangleObjects.Max(rect => rect.TopRight.X);
            var minY = rectangleObjects.Min(rect => rect.BottomLeft.Y);
            var maxY = rectangleObjects.Max(rect => rect.TopRight.Y);

            var tiles = new bool[maxX, maxY];

            for (var i = 0; i < rectangleObjects.Length; i++)
            {
                var intersect = rectangleObjects
                    .Where(rect => rect != rectangleObjects[i])
                    .Any(rect => Intersect(rectangleObjects[i], rect));
                if (intersect)
                    return false;
            }

            for (var x = minX; x < maxX; x++)
            {
                for (var y = minY; y < maxY; y++)
                {
                    var testRect = new Rectangle(new Point(x, y), new Point(x + 1, y + 1));
                    var covered = rectangleObjects.Any(rect => Intersect(rect, testRect));
                    if (!covered)
                        return false;
                }
            }

            return true;
        }

        private bool Intersect(Rectangle rectangle1, Rectangle rectangle2)
        {
            if (rectangle1.BottomLeft.X >= rectangle2.TopRight.X || rectangle2.BottomLeft.X >= rectangle1.TopRight.X)
                return false;

            if (rectangle1.BottomLeft.Y >= rectangle2.TopRight.Y || rectangle2.BottomLeft.Y >= rectangle1.TopRight.Y)
                return false;

            //bottom left in rect then intersect
            //if (rectangle1.BottomLeft.X <= rectangle2.BottomLeft.X && rectangle1.TopRight.X > rectangle2.BottomLeft.X //within X range
            //   && rectangle1.BottomLeft.Y <= rectangle2.BottomLeft.Y && rectangle1.TopRight.Y > rectangle2.BottomLeft.Y // and within Y range
            //)
            //    return true; //intersect

            ////top left in rect, then intersect
            //if (rectangle1.BottomLeft.X < rectangle2.TopLeft.X && rectangle1.TopRight.X >= rectangle2.TopLeft.X //within X range
            //   && rectangle1.BottomLeft.Y < rectangle2.TopLeft.Y && rectangle1.TopRight.Y >= rectangle2.TopLeft.Y // and within Y range
            //)
            //    return true; //intersect

            return true;
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
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; }
            public int Y { get; }
        }
    }

    public class Problem391_PerfectRectangle_slow
    {
        [Test]
        [TestCase("[[1,1,3,3],[3,1,4,2],[3,2,4,4],[1,3,2,4],[2,3,3,4]]", true)]
        [TestCase("[[1,1,2,3],[1,3,2,4],[3,1,4,2],[3,2,4,4]]", false)]
        [TestCase("[[1,1,3,3],[3,1,4,2],[1,3,2,4],[3,2,4,4]]", false)]//
        [TestCase("[[1,1,3,3],[3,1,4,2],[1,3,2,4],[2,2,4,4]]", false)]
        [TestCase("[[0,0,4,1],[0,0,4,1]]", false)]
        [TestCase("[[0,0,4,1],[7,0,8,3],[5,1,6,3],[6,0,7,2],[4,0,5,1],[4,2,5,3],[2,1,4,3],[0,2,2,3],[0,1,2,2],[6,2,8,3],[5,0,6,1],[4,1,5,2]]", false)]
        [TestCase("[[7,0,8,3],[6,2,8,3]]", false)]
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

            var minX = rectangleObjects.Min(rect => rect.BottomLeft.X);
            var maxX = rectangleObjects.Max(rect => rect.TopRight.X);
            var minY = rectangleObjects.Min(rect => rect.BottomLeft.Y);
            var maxY = rectangleObjects.Max(rect => rect.TopRight.Y);

            var tiles = new bool[maxX, maxY];

            for (var i = 0; i < rectangleObjects.Length; i++)
            {
                var intersect = rectangleObjects
                    .Where(rect => rect != rectangleObjects[i])
                    .Any(rect => Intersect(rectangleObjects[i], rect));
                if(intersect)
                    return false;
            }

            for (var x = minX; x < maxX; x++)
            {
                for (var y = minY; y < maxY; y++)
                {
                    var testRect = new Rectangle(new Point(x, y), new Point(x + 1, y + 1));
                    var covered = rectangleObjects.Any(rect => Intersect(rect, testRect));
                    if (!covered)
                        return false;
                }
            }

            return true;
        }

        private bool Intersect(Rectangle rectangle1, Rectangle rectangle2)
        {
            if(rectangle1.BottomLeft.X >= rectangle2.TopRight.X || rectangle2.BottomLeft.X >= rectangle1.TopRight.X)
                return false;

            if(rectangle1.BottomLeft.Y >= rectangle2.TopRight.Y || rectangle2.BottomLeft.Y >= rectangle1.TopRight.Y)
                return false;

            //bottom left in rect then intersect
            //if (rectangle1.BottomLeft.X <= rectangle2.BottomLeft.X && rectangle1.TopRight.X > rectangle2.BottomLeft.X //within X range
            //   && rectangle1.BottomLeft.Y <= rectangle2.BottomLeft.Y && rectangle1.TopRight.Y > rectangle2.BottomLeft.Y // and within Y range
            //)
            //    return true; //intersect

            ////top left in rect, then intersect
            //if (rectangle1.BottomLeft.X < rectangle2.TopLeft.X && rectangle1.TopRight.X >= rectangle2.TopLeft.X //within X range
            //   && rectangle1.BottomLeft.Y < rectangle2.TopLeft.Y && rectangle1.TopRight.Y >= rectangle2.TopLeft.Y // and within Y range
            //)
            //    return true; //intersect

            return true;
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
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; }
            public int Y { get; }
        }
    }
}