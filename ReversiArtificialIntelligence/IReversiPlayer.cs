using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiArtificialIntelligence
{
    /// <summary>
    /// Represents a reversi player
    /// </summary>
    public interface IReversiPlayer
    {
        /// <summary>
        /// Determines the next move on a given board, for the given player
        /// </summary>
        /// <remarks>
        /// The function must return a non-null point, representing a valid move <see cref="ReversiGame.IsValidMove(Disc[,], Point, Disc)"/>.
        /// Also, this function must finish in less <see cref="ReversiGame.MOVE_TIMEOUT"/> miliseconds.
        /// This function may and should use the many public helpers in <see cref="Extensions"/> and <see cref="ReversiGame"/>.
        /// </remarks>
        /// <param name="board">The game board</param>
        /// <param name="playerColor">The current player's color</param>
        /// <returns>The point in which the next disc should be played</returns>
        Point NextMove(Disc[,] board, Disc playerColor);
    }
}
