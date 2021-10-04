using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeetCode
{
    public class Problem37_SudokuSolver
    {
        [Test]
        [TestCase(
            "5,3,.,.,7,.,.,.,.-6,.,.,1,9,5,.,.,.-.,9,8,.,.,.,.,6,.-8,.,.,.,6,.,.,.,3-4,.,.,8,.,3,.,.,1-7,.,.,.,2,.,.,.,6-.,6,.,.,.,.,2,8,.-.,.,.,4,1,9,.,.,5-.,.,.,.,8,.,.,7,9",
            "5,3,4,6,7,8,9,1,2-6,7,2,1,9,5,3,4,8-1,9,8,3,4,2,5,6,7-8,5,9,7,6,1,4,2,3-4,2,6,8,5,3,7,9,1-7,1,3,9,2,4,8,5,6-9,6,1,5,3,7,2,8,4-2,8,7,4,1,9,6,3,5-3,4,5,2,8,6,1,7,9")]
        public void Test(string s, string expected)
        {
            var sut = new Problem37_SudokuSolver();

            var board = SplitString(s).ToArray();
            var expectedBoard = SplitString(expected).ToArray();

            sut.SolveSudoku(board);

            var boardString = MakeBoardString(board);

            for (var i = 0; i < board.Length; i++)
            {
                for (var j = 0; j < board[i].Length; j++)
                {
                    Assert.AreEqual(expectedBoard[i][j], board[i][j], $"{expectedBoard[i][j]} expected, but got {board[i][j]} at {{{i},{j}}}");
                }
            }
        }

        private string MakeBoardString(char[][] board)
        {
            var builder = new StringBuilder();
            foreach(var row in board)
            {
                builder.AppendLine(String.Join(",", row));
            }
            return builder.ToString();
        }

        private static IEnumerable<char[]> SplitString(string s)
        {
            var rows = s.Split("-");
            foreach (var row in rows)
                yield return row.Split(",").Select(x => String.IsNullOrEmpty(x) || x == "." ? ' ' : x[0]).ToArray();
        }

        private static bool CharInRow(char c, char[][] board, int row)
            => board[row].Any(x => x == c);

        private static bool CharInCol(char c, char[][] board, int col)
        {
            var count = 0;
            for (var row = 0; row < 9; row++)
            {
                if (board[row][col] == c)
                    return true;
            }
            return false;
        }

        private static bool CharInSquare(char c, char[][] board, int row, int col)
        {
            var maxCol = GetMaxIndex(col);
            var minCol = GetMinIndex(col);
            var maxRow = GetMaxIndex(row);
            var minRow = GetMinIndex(row);

            for (var i = minRow; i <= maxRow; i++)
            {
                for (var j = minCol; j <= maxCol; j++)
                {
                    if (board[i][j] == c)
                        return true;
                }
            }
            return false;
        }

        private static int GetMaxIndex(int row)
            => row < 3 ? 2 : (row < 6 ? 5 : 8);

        private static int GetMinIndex(int row)
            => row < 3 ? 0 : (row < 6 ? 3 : 6);

        char[] chars = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public void SolveSudoku(char[][] board)
        {
            Solve(ref board, 0, 0, 0);

            for(var i = 0; i < 9; i++)
                for(var j = 0; j < 0; j++)
                    board[i][j] = board[i][j];
            //for (var row = 0; row < 9; row++)
            //{
            //    for (var col = 0; col < 9; col++)
            //    {
            //        if(board[row][col] != ' ')
            //            continue;

            //    }
            //}
        }

        private bool Solve(ref char[][] board, int row, int col, int numIndex)
        {
            if (numIndex >= 9)
                return false;

            if (row == 8 && col == 8 && numIndex == 9)
                return false;
            if(row > 8)
                return true;

            var nextCol = col < 8 ? col + 1 : 0;
            var nextRow = col < 8 ? row : row + 1;
            var c = chars[numIndex];

            if (board[row][col] != ' ')
            {
                //Skip known cells
                return Solve(ref board, nextRow, nextCol, 0);
            }
            else if (CharInRow(c, board, row) ||
                CharInCol(c, board, col) ||
                CharInSquare(c, board, row, col))
            {
                //if this is a violation, try the next number
                return Solve(ref board, row, col, numIndex + 1);
            }
            
            // no violation and not known, assign to this cell and solve for the next cell
            board[row][col] = chars[numIndex];
            if (Solve(ref board, nextRow, nextCol, 0))
                return true;

            //this number wasn't valid, try the next from this point
            board[row][col] = ' ';
            return Solve(ref board, row, col, numIndex + 1);
        }
    }
}