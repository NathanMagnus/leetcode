using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode
{
    // 2 tries
    public class Problem06_ZigZagConversion
    {
        [Test]
        [TestCase("PAYPALISHIRING", 3, "PAHNAPLSIIGYIR")]
        [TestCase("AB", 1, "AB")]
        public void Test(string s, int rows, string expected)
        {
            var sut = new Problem06_ZigZagConversion();
            var result = sut.Convert(s, rows);
            Assert.AreEqual(expected, result);
        }

        public string Convert(string s, int numRows)
        {
            var array = CreateArray(s, numRows);
            var row = 0;
            var col = 0;
            var up = false;
            foreach (var character in s)
            {
                array[row][col] = character;
                var newRow = GetNextRow(numRows, row, up);
                var newCol = GetNextCol(numRows, row, col);

                row = newRow;
                col = newCol;
                if (row == 0 || row == numRows - 1)
                    up = !up;
            }

            var result = new String(FlattenArray(array).ToArray());
            return result;
        }

        private IEnumerable<char> FlattenArray(char[][] array)
        {
            for (var i = 0; i < array.Length; i++)
            {
                for (var j = 0; j < array[i].Length; j++)
                {
                    if (array[i][j] != ' ')
                        yield return array[i][j];
                }
            }
        }

        private int GetNextCol(int numRows, int row, int col)
        {
            if (row == numRows - 1)
                return col + 1;
            if (col % (numRows - 1) != 0)
                return col + 1;
            return col;
        }

        private int GetNextRow(int numRows, int row, bool up)
        {
            if (numRows == 1)
                return row;
            return row == numRows - 1 ? row - 1 : (row + (up ? -1 : 1));
        }

        private char[][] CreateArray(string s, int numRows)
        {
            var array = new char[numRows][];
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = new char[s.Length];
                for (var j = 0; j < s.Length; j++)
                    array[i][j] = ' ';
            }
            return array;
        }
    }
}