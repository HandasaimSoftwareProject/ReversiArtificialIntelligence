using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiArtificialIntelligence
{
    class Beiser : IReversiPlayer
    {
        public Point NextMove(Disc[,] board, Disc playerColor)
        {
            Point bestMove = new Point(0, 0);
            int bestScore = int.MinValue;

            foreach (Point p1 in ReversiGame.ValidMoves(board, playerColor))
            {
                Disc[,] newBoard = ReversiGame.PlayTurn(board, p1, playerColor);
                foreach (Point p2 in ReversiGame.ValidMoves(newBoard, playerColor.Reversed()))
                {
                    int score = ReversiGame.Score(newBoard, playerColor);
                    if (score > bestScore)
                    {
                        bestMove = p1;
                        bestScore = score;
                        break;
                    }
                }
            }

            if (ReversiGame.IsValidMove(board, bestMove, playerColor))
            {
                return bestMove;
            }

            return ReversiGame.ValidMoves(board, playerColor).First();
        }
    }
}
