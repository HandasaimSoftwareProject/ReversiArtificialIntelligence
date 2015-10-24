using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiArtificialIntelligence
{
    /// <summary>
    /// Represnts a point on the board
    /// </summary>
    /// <remarks>
    /// The board is a two-dimensional 0-based-index array.
    /// (0,0) is the top left corner.
    /// </remarks>
    public class Point
    {
        /// <summary>
        /// The vertical (row) index
        /// </summary>
        public readonly int X;
        /// <summary>
        /// The horizontal (column) index
        /// </summary>
        public readonly int Y;

        /// <summary>
        /// Creates a new instance of the point class
        /// </summary>
        /// <param name="x">vertical index</param>
        /// <param name="y">horizontal index</param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Addes two points
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        /// <summary>
        /// Returns a set of all 8 directional vectors
        /// </summary>
        /// <remarks>
        /// The vectors are: (0,1),(1,0),(-1,0),(0,-1),(1,-1),(-1,1),(1,1),(-1,-1)
        /// </remarks>
        public static ISet<Point> Directions
        {
            get
            {
                ISet<Point> dirs = new HashSet<Point>();
                for (int i = -1; i <= 1; i++)
                    for (int j = -1; j <= 1; j++)
                        if (i != 0 || j != 0)
                            dirs.Add(new Point(i, j));
                return dirs;
            }
        }

        #region OverriddenMethods

        public override string ToString()
        {
            return String.Format("({0},{1})", X, Y);
        }

        public override bool Equals(object obj)
        {
            Point p = obj as Point;
            if (p == null)
                return false;
            return p.X == X && p.Y == Y;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }

        #endregion
    }
}
