using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReversiArtificialIntelligence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReversiArtificialIntelligence.Tests
{
    /// <summary>
    /// Tests the player objects <see cref="IReversiPlayer"/>
    /// </summary>
    [TestClass()]
    public class PlayerTests
    {
        /// <summary>
        /// Tests that all player can handle the first turn
        /// </summary>
        [TestMethod()]
        public void AllPlayerTest()
        {
            foreach (Type t in Assembly.GetCallingAssembly().GetTypes())
            {
                if (t.GetInterface("IReversiPlayer") != null)
                {
                    IReversiPlayer player = Activator.CreateInstance(t) as IReversiPlayer;
                    ReversiGame rg = new ReversiGame();
                    rg.PlaySingleTurn(player);
                }
            }
        }
    }
}