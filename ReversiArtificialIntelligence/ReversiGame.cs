using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.IO;

namespace ReversiArtificialIntelligence
{
    public class ReversiGame
    {
        /// <summary>
        /// The game board
        /// </summary>
        private Disc[,] board;
        /// <summary>
        /// The color of the current player
        /// </summary>
        private Disc currentPlayer;
        /// <summary>
        /// The time allocated to a single move, in miliseconds
        /// </summary>
        public static readonly int MOVE_TIMEOUT = 1000;

        /// <summary>
        /// Creates a new instance of the ReversiGame Class
        /// </summary>
        /// <param name="size">The sive of the board</param>
        public ReversiGame(int size = 8)
        {
            Contract.Requires(size > 0);
            Contract.Requires(size % 2 == 0);

            board = new Disc[size, size];
            board[size / 2 - 1, size / 2 - 1] = Disc.Black;
            board[size / 2, size / 2 - 1] = Disc.White;
            board[size / 2 - 1, size / 2] = Disc.White;
            board[size / 2, size / 2] = Disc.Black;
            currentPlayer = Disc.White;
        }

        /// <summary>
        /// Plays a single game and dumps the results
        /// </summary>
        /// <param name="white">The white (first) player</param>
        /// <param name="black">The black (second) player</param>
        /// <param name="dump">A text writer to act as a dump</param>
        /// <returns>The winner (Disc.Empty if draw)</returns>
        public static Disc PlayGame(IReversiPlayer white, IReversiPlayer black,
            TextWriter dump = null)
        {
            if(dump == null)
                dump = Console.Out;
            dump.WriteLine("{0},{1}#", white.GetName(), black.GetName());
            ReversiGame game = new ReversiGame();
            dump.WriteLine(game.board.AsString() + "$");
            game.currentPlayer = Disc.White;
            int passes = 0;
            while (passes < 2)
            {
                if (ValidMoves(game.board, game.currentPlayer).Count == 0)
                {
                    passes++;
                    game.currentPlayer = game.currentPlayer.Reversed();
                }
                else
                    try
                    {
                        Program.RunActionWithTimeout(
                          () =>
                            game.PlaySingleTurn(
                            game.currentPlayer == Disc.White ? white : black), 
                          MOVE_TIMEOUT);
                        passes = 0;
                    }
                    catch (Exception e)
                    {
                        dump.WriteLine(e);
                        dump.WriteLine(e.StackTrace);
                        return game.currentPlayer.Reversed();
                    }
                dump.WriteLine(game.board.AsString() + "$");
            }
            int score = Score(game.board, Disc.White);
            if (score > 0)
                return Disc.White;
            else if (score < 0)
                return Disc.Black;
            else
                return Disc.Empty;
        }

        /// <summary>
        /// Calculates and returns the current score of the board for a given player
        /// The score is calculated as the number of allied discs minus the number of opponnent discs.
        /// </summary>
        /// <param name="board">The game board</param>
        /// <param name="color">The player color</param>
        /// <returns>The board-player score</returns>
        public static int Score(Disc[,] board, Disc color)
        {
            Contract.Requires(!color.IsEmpty());
            int score = CountDiscs(board, Disc.White) - CountDiscs(board, Disc.Black);
            if (color == Disc.Black)
                score *= -1;
            return score;
        }

        /// <summary>
        /// Counts the number of discs of a given color on the board.
        /// (works for Disc.Empty)
        /// </summary>
        /// <param name="board">The game board</param>
        /// <param name="color">The player color</param>
        /// <returns>The disc count</returns>
        public static int CountDiscs(Disc[,] board, Disc color)
        {
            int count = 0;
            foreach (Point p in board.PointsIterator())
                if (board.At(p) == color)
                    count++;
            return count;
        }

        /// <summary>
        /// Returns the set of valid moves for a given board and a given player
        /// </summary>
        /// <param name="board">The game board</param>
        /// <param name="currentPlayer">The current player</param>
        /// <returns>A set of valid play points</returns>
        public static ISet<Point> ValidMoves(Disc[,] board, Disc currentPlayer)
        {
            HashSet<Point> validMoves = new HashSet<Point>();
            foreach (Point p in board.PointsIterator())
                if (IsValidMove(board, p, currentPlayer))
                    validMoves.Add(p);
            return validMoves;
        }

        /// <summary>
        /// Playes a single game turn
        /// </summary>
        /// <param name="player">The player object</param>
        public void PlaySingleTurn(IReversiPlayer player)
        {
            Point point = player.NextMove((Disc[,])board.Clone(), currentPlayer);
            board = PlayTurn(board, point, currentPlayer);
            currentPlayer = currentPlayer.Reversed();
        }

        /// <summary>
        /// Plays a single turn on the given board
        /// </summary>
        /// <remarks>
        /// Does not change the given board.
        /// </remarks>
        /// <param name="board">The game board</param>
        /// <param name="newDisc">The point to place the new disc</param>
        /// <param name="currentPlayer">The color of the disc to place</param>
        /// <returns>A new board, with the given move played</returns>
        public static Disc[,] PlayTurn(Disc[,] board, Point newDisc, Disc currentPlayer)
        {
            if (!IsValidMove(board, newDisc, currentPlayer))
                throw new Exception("Invalid move");
            Disc[,] newBoard = (Disc[,])board.Clone();
            newBoard.SetAt(newDisc, currentPlayer);
            foreach (Point p in PointsToReverse(board, newDisc, currentPlayer))
                newBoard.ReverseAt(p);
            return newBoard;
        }

        /// <summary>
        /// Returns a set of points to reverse as a result of placing a new disc
        /// </summary>
        /// <remarks>
        /// A move is invalid if the number of points in the set is zero
        /// <see cref="ReversiGame.IsValidMove(Disc[,], Point, Disc)"/>
        /// </remarks>
        /// <param name="board">The game board</param>
        /// <param name="newDisc">The new disc's position</param>
        /// <param name="currentPlayer">The new disc's color</param>
        /// <returns>A set of points to reverse</returns>
        public static ISet<Point> PointsToReverse(Disc[,] board, Point newDisc, Disc currentPlayer)
        {
            if (!board.At(newDisc).IsEmpty())
                throw new Exception("Non-empty point chosen to place new disc");
            HashSet<Point> result = new HashSet<Point>();
            foreach (Point direction in Point.Directions)
                result.UnionWith(ReversedToNext(board, newDisc, direction, currentPlayer));
            return result;
        }

        /// <summary>
        /// Checks if a given move is valid
        /// </summary>
        /// <param name="board">The game board</param>
        /// <param name="point">The point to play in</param>
        /// <param name="currentPlayer">The current player's color</param>
        /// <returns>True if the move is valid, false otherwise</returns>
        public static bool IsValidMove(Disc[,] board, Point point, Disc currentPlayer)
        {
            if (point == null)
                return false;
            return board.At(point).IsEmpty() &&
                PointsToReverse(board, point, currentPlayer).Count > 0;
        }

        /// <summary>
        /// Returns the set of points to reverse in a given direction.
        /// </summary>
        /// <remarks>
        /// Returns the consecutive set of discs from the current point 
        /// to the next same colored point in the given direction.
        /// </remarks>
        /// <param name="board">The game board</param>
        /// <param name="point">The point of the new disc</param>
        /// <param name="direction">The direction to scan in</param>
        /// <param name="currentPlayer">The current player's color</param>
        /// <returns></returns>
        public static ISet<Point> ReversedToNext(Disc[,] board, Point point, Point direction, Disc currentPlayer)
        {
            HashSet<Point> points = new HashSet<Point>();
            point += direction;
            if (!board.OnBoard(point))
                return points;
            Disc currentDisc;
            for (currentDisc = board.At(point);
                currentDisc == currentPlayer.Reversed() && board.OnBoard(point + direction);
                point += direction, currentDisc = board.At(point))
                points.Add(point);
            if (currentDisc.IsEmpty() ||
                !board.OnBoard(point + direction) && currentDisc != currentPlayer)
                points.Clear();
            return points;
        }
    }
}
