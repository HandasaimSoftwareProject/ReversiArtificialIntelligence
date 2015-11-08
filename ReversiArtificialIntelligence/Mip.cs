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
    public class Mip : IReversiPlayer
    {
        public Point NextMove(Disc[,] board, Disc playerColor)
        {
            Point bestMove= new Point(0,0);
            int bestScore = int.MinValue;
            Point DRCorner = new Point(7, 7);
            bool isDRfull =false;
            Point URCorner = new Point(7, 0);
            bool isURfull =false;
            Point DLCorner = new Point(0, 7);
            bool isDLfull =false;
            Point ULCorner = new Point(0, 0);
            bool isULfull =false;
            bool shouldDoDumm = true;

            if (ReversiGame.IsValidMove(board,DRCorner,playerColor))
            {
                bestMove = DRCorner;
                shouldDoDumm = false;
                isDRfull = true;
            }
            else if (ReversiGame.IsValidMove(board, URCorner, playerColor))
            {
                bestMove = URCorner;
                shouldDoDumm = false;
                isURfull = true;
            }
            else if (ReversiGame.IsValidMove(board, DLCorner, playerColor))
            {
                bestMove = DLCorner;
                shouldDoDumm = false;
                isDLfull = true;
            }
            else if (ReversiGame.IsValidMove(board, ULCorner, playerColor))
            {
                bestMove = ULCorner;
                shouldDoDumm = false;
                isULfull = true;
            }
                if(shouldDoDumm)
                {
                    foreach (Point p in ReversiGame.ValidMoves(board, playerColor))
                    {
                        int score = ReversiGame.Score(ReversiGame.PlayTurn(board, p, playerColor), playerColor);
                        if (score > bestScore)
                        {
                            bestMove = p;
                            bestScore = score;
                        }
                    }
                    ReversiGame.PlayTurn(board, bestMove, playerColor);
                    foreach (Point p in ReversiGame.ValidMoves(board, playerColor))
                    {
                        int score = ReversiGame.Score(ReversiGame.PlayTurn(board, p, playerColor), playerColor);
                        if (score > bestScore)
                        {
                            bestMove = p;
                            bestScore = score;
                        }
                    }
                    ReversiGame.PlayTurn(board, bestMove, playerColor);
                    foreach (Point p in ReversiGame.ValidMoves(board, playerColor))
                    {
                        int score = ReversiGame.Score(ReversiGame.PlayTurn(board, p, playerColor), playerColor);
                        if (score > bestScore)
                        {
                            bestMove = p;
                            bestScore = score;
                        }

                    }
                    ReversiGame.PlayTurn(board, bestMove, playerColor);
                    foreach (Point p in ReversiGame.ValidMoves(board, playerColor))
                    {
                        int score = ReversiGame.Score(ReversiGame.PlayTurn(board, p, playerColor), playerColor);
                        if (score > bestScore)
                        {
                            bestMove = p;
                            bestScore = score;
                        }

                    }
                    ReversiGame.PlayTurn(board, bestMove, playerColor);
                    foreach (Point p in ReversiGame.ValidMoves(board, playerColor))
                    {
                        int score = ReversiGame.Score(ReversiGame.PlayTurn(board, p, playerColor), playerColor);
                        if (score > bestScore)
                        {
                            bestMove = p;
                            bestScore = score;
                        }

                    }
                    ReversiGame.PlayTurn(board, bestMove, playerColor);
                    foreach (Point p in ReversiGame.ValidMoves(board, playerColor))
                    {
                        int score = ReversiGame.Score(ReversiGame.PlayTurn(board, p, playerColor), playerColor);
                        if (score > bestScore)
                        {
                            bestMove = p;
                            bestScore = score;
                        }

                    }
                }
            }
            return bestMove;
        }
    }
}
