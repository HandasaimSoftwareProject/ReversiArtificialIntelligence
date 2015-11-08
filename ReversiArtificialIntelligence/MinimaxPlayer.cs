using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiArtificialIntelligence
{
    class MinimaxPlayer : IReversiPlayer
    {
        public Point NextMove(Disc[,] board, Disc playerColor)
        {
            Point bestMove = new Point(0, 0);
            int bestScore = int.MinValue;
            foreach (Point p1 in ReversiGame.ValidMoves(board, playerColor))
            {
                ReversiGame.PlayTurn(board, p1, playerColor);
                foreach (Point p2 in ReversiGame.ValidMoves(board, playerColor))
                {
                    int score = ReversiGame.Score(board, playerColor);
                    if (score > bestScore)
                    {
                        bestMove = p1;
                        bestScore = score;
                    }
                }
            }
            return bestMove;
        }
    }
}
