using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiArtificialIntelligence
{
    /// <summary>
    /// Containes many extension methods to other types.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Returns the reversed color of the disc
        /// </summary>
        /// <param name="disc">A colored disc</param>
        /// <returns>Black if disc is White, White if disc is black, and an exception otherwise</returns>
        public static Disc Reversed(this Disc disc)
        {
            switch (disc)
            {
                case Disc.Black:
                    return Disc.White;
                case Disc.White:
                    return Disc.Black;
                default:
                    throw new Exception("Can't reverse empty disc");
            }
        }

        /// <summary>
        /// Checks if the disc is empty
        /// </summary>
        /// <param name="disc">A Disc</param>
        /// <returns>True if the disc is empty</returns>
        public static bool IsEmpty(this Disc disc)
        {
            return disc == Disc.Empty;
        }

        /// <summary>
        /// Returns the disc in given cell on the board
        /// </summary>
        /// <param name="board">The target board</param>
        /// <param name="p">The point on the board</param>
        /// <returns>The disc in the given cell</returns>
        public static Disc At(this Disc[,] board, Point p)
        {
            return board[p.X, p.Y];
        }

        /// <summary>
        /// Sets the disc in given cell on the board
        /// </summary>
        /// <param name="board">The target board</param>
        /// <param name="p">The point on the board</param>
        /// <param name="d">The disc to place on the board</param>
        public static void SetAt(this Disc[,] board, Point p, Disc d)
        {
            board[p.X, p.Y] = d;
        }

        /// <summary>
        /// Reverse the disc at a given point on the board
        /// </summary>
        /// <param name="board">The target board</param>
        /// <param name="p">The point on the board</param>
        public static void ReverseAt(this Disc[,] board, Point p)
        {
            board[p.X, p.Y] = board[p.X, p.Y].Reversed();
        }

        /// <summary>
        /// Tests if a point is within the borders of the board
        /// </summary>
        /// <param name="board">The target board</param>
        /// <param name="p">The point on the board</param>
        /// <returns>True if the point is on the board, false otherwise</returns>
        public static bool OnBoard(this Disc[,] board, Point p)
        {
            return p.X >= 0 && p.Y >= 0 &&
                p.X < board.GetLength(0) && p.Y < board.GetLength(1);
        }

        /// <summary>
        /// Generates a string representation of the board
        /// Black - 'O'
        /// White - 'X'
        /// </summary>
        /// <param name="board">The target board</param>
        /// <returns>a string representation of the board</returns>
        public static string AsString(this Disc[,] board)
        {
            char[] c = new char[] { 'O', '-', 'X' };
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                    sb.Append(c[(int)board[i, j] + 1]);
                sb.Append('\n');
            }
            return sb.ToString();
        }

        /// <summary>
        /// Generates a board from a string representation
        /// </summary>
        /// <param name="s">a string representation of the board</param>
        /// <returns>A new board</returns>
        public static Disc[,] BoardFromString(this String s)
        {
            string[] lines = s.Split('\n');
            Disc[,] board = new Disc[lines.Length, lines.Length];
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(1); j++)
                    switch (lines[i][j])
                    {
                        case 'O':
                            board[i, j] = Disc.Black;
                            break;
                        case '-':
                            board[i, j] = Disc.Empty;
                            break;
                        case 'X':
                            board[i, j] = Disc.White;
                            break;
                    }
            return board;
        }

        /// <summary>
        /// Iterates all points of the board
        /// </summary>
        /// <param name="board">The target board</param>
        /// <returns>An iterator of points (use foreach)</returns>
        public static IEnumerable<Point> PointsIterator(this Disc[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(1); j++)
                    yield return new Point(i, j);
        }

        /// <summary>
        /// Returns the name of an IReversiPlayer
        /// </summary>
        /// <param name="player">The target player</param>
        /// <returns>The name of the player</returns>
        public static string GetName(this IReversiPlayer player)
        {
            return player.GetType().Name;
        }
    }

}
