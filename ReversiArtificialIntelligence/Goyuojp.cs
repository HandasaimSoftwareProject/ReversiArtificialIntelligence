using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiArtificialIntelligence
{
    /// <summary>
    /// This player calculates the move that guarentees the most allied discs on the next turn
    /// </summary>
    public class Goyuojp : IReversiPlayer
    {
        public Point NextMove(Disc[,] board, Disc playerColor)
        {
            Point bestMove = new Point(0, 0);
            int bestScore = int.MinValue;
            bool flag = false, flag2=false;
            foreach (Point p in ReversiGame.ValidMoves(board, playerColor))
            {
                if ((p.X == 0 && p.Y == 0) || (p.X == 0 && p.Y == 7) || (p.Y == 0 && p.X == 7) || (p.X == 7 && p.Y == 7)) return p;
                else
                {
                    if ((p.Y == 0) || (p.Y == 7) || (p.X == 0) || (p.X == 7))
                    {
                        return p;
                    }
                   
                }
                int score = ReversiGame.Score(ReversiGame.PlayTurn(board, p, playerColor), playerColor);
                if ((score > bestScore)&&((p.X!=1)&&(p.Y!=1)&&(p.Y!=6)&&(p.X!=6)))

                {
                    flag=true;
                    bestMove = p;
                    bestScore = score;
                }
                else
                {
                }
            }
            if(flag==false)
            {
                foreach (Point p in ReversiGame.ValidMoves(board, playerColor))
                {
                    if ((p.X != 1 && p.Y != 1)&&(p.X != 1 && p.Y != 6)&&(p.X != 6 && p.Y != 1)&&(p.X != 6 && p.Y != 6))
                    {
                        flag2 = true;
                        return p;
                    }
                   
                }
                if (flag2 == false)
                {
                    bestMove = ReversiGame.ValidMoves(board, playerColor).First();
                }
            }
            return bestMove;
        }
    }
}
