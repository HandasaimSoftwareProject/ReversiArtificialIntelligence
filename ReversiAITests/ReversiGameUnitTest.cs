using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReversiArtificialIntelligence;

namespace ReversiAITests
{
    /// <summary>
    /// Tests various methods in the ReversiGame class
    /// </summary>
    [TestClass]
    public class ReversiGameUnitTest
    {
        [TestMethod]
        public void PointsToReverseTest()
        {
            Disc[,] board = "----\n-OX-\n-XO-\n----".BoardFromString();
            var pointSet = ReversiGame.PointsToReverse(board, new Point(0, 2), Disc.Black);
            Assert.AreEqual(1, pointSet.Count);
            Assert.IsTrue(pointSet.Contains(new Point(1, 2)));
        }
        [TestMethod]
        public void PointsToReverseTest2()
        {
            Disc[,] board = "------\n--XO--\n---O--\n--XX--\n------\n------".BoardFromString();
            var pointSet = ReversiGame.PointsToReverse(board, new Point(1, 4), Disc.White);
            Assert.AreEqual(2, pointSet.Count);
            Assert.IsTrue(pointSet.Contains(new Point(1, 3)));
            Assert.IsTrue(pointSet.Contains(new Point(2, 3)));
        }
        [TestMethod]
        public void IsValidMoveFalseTest()
        {
            Disc[,] board = "----\n-OX-\n-XO-\n----".BoardFromString();
            Assert.IsFalse(ReversiGame.IsValidMove(board, new Point(0, 0), Disc.White));
            Assert.IsFalse(ReversiGame.IsValidMove(board, new Point(0, 1), Disc.Black));
            Assert.IsFalse(ReversiGame.IsValidMove(board, new Point(1, 1), Disc.White));
            Assert.IsFalse(ReversiGame.IsValidMove(board, new Point(1, 1), Disc.Black));
        }
        [TestMethod]
        public void IsValidMoveTrueTest()
        {
            Disc[,] board = "----\n-OX-\n-XO-\n----".BoardFromString();
            Assert.IsTrue(ReversiGame.IsValidMove(board, new Point(0, 1), Disc.White));
            Assert.IsTrue(ReversiGame.IsValidMove(board, new Point(1, 3), Disc.Black));
            Assert.IsTrue(ReversiGame.IsValidMove(board, new Point(3, 1), Disc.Black));
        }

        [TestMethod]
        public void ScoreTest()
        {
            Disc[,] board = "------\n--XO--\n---O--\n--XX--\n------\n------".BoardFromString();
            Assert.AreEqual(1, ReversiGame.Score(board, Disc.White));
            Assert.AreEqual(-1, ReversiGame.Score(board, Disc.Black));
        }
    }
}
