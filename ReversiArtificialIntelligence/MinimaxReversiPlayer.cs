using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiArtificialIntelligence
{
    /// <summary>
    /// This player employes a minimax technique
    /// </summary>
    public class MinimaxReversiPlayer : IReversiPlayer
    {
        /// <summary>
        /// The minimax maximum depth
        /// </summary>
        private const int MINIMAX_DEPTH = 4;
        public Point NextMove(Disc[,] board, Disc playerColor)
        {
            return Minimax(board, playerColor, MINIMAX_DEPTH).Item2;
        }

        /// <summary>
        /// The minimax algorithm
        /// </summary>
        /// <param name="board">Target board</param>
        /// <param name="playerColor">Current player</param>
        /// <param name="maxDepth">Maximim recursion depth</param>
        /// <returns>The score and point of the best minimax play</returns>
        private Tuple<int,Point> Minimax(Disc[,] board, Disc playerColor, int maxDepth)
        {
            if (maxDepth == 0)
                return new Tuple<int, Point>(ReversiGame.Score(board, playerColor), null);
            Point bestMove = null;
            int bestScore = int.MinValue;
            foreach (Point p in ReversiGame.ValidMoves(board, playerColor))
            {
                int score = -Minimax(
                    ReversiGame.PlayTurn(board, p, playerColor), 
                    playerColor.Reversed(),
                    maxDepth - 1).Item1;
                if (score > bestScore)
                {
                    bestMove = p;
                    bestScore = score;
                }
            }
            if (bestMove == null)
                bestScore = ReversiGame.Score(board, playerColor);
            return new Tuple<int, Point>(bestScore, bestMove);
        }
    }
}
