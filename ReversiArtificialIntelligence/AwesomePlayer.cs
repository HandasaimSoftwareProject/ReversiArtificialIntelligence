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
                if ((p.X == 7 && p.Y == 7) || (p.X == 7 && p.Y == 0) || (p.X == 0 && p.Y == 7) || (p.X == 0 && p.Y == 0))
                {
                    return p;
                }
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
