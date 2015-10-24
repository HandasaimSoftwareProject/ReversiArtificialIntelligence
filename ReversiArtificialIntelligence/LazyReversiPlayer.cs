using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiArtificialIntelligence
{
    /// <summary>
    /// This player returns the first move in the valid moves list
    /// </summary>
    public class LazyReversiPlayer : IReversiPlayer
    {
        public Point NextMove(Disc[,] board, Disc playerColor)
        {
            return ReversiGame.ValidMoves(board, playerColor).First();
        }
    }
}
