using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiArtificialIntelligence
{
    /// <summary>
    /// 
    /// </summary>
    public class AwesomePlayer : IReversiPlayer
    {
        public Point NextMove(Disc[,] board, Disc playerColor)
        {
            Point bestMove = new Point(0, 0);
            int MaxD = 1;
            
            foreach (Point p in ReversiGame.ValidMoves(board, playerColor))
            {
                int score = ReversiGame.Score(ReversiGame.PlayTurn(board, p, playerColor), playerColor);
                if (board.At(p) == playerColor)
                {

                }

                if (score > MaxD)
                {
                    bestMove = p;
                    MaxD = score;
                }
            }
            return bestMove;
        }
    }
}
