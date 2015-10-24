using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReversiArtificialIntelligence
{
    /// <summary>
    /// Represents a multiplayer simulation
    /// </summary>
    public class ReversiMultiplayerSimulation
    {
        /// <summary>
        /// The list of players
        /// </summary>
        private readonly List<IReversiPlayer> players;
        /// <summary>
        /// The Scoreboard.
        /// Each cell represents the outcome of a single match.
        /// </summary>
        private int[,] scoreboard;
        /// <summary>
        /// The total scores of the simulation
        /// </summary>
        private int[] scores;

        /// <summary>
        /// The Scoreboard.
        /// Each cell represents the outcome of a single match.
        /// </summary>
        public int[,] Scoreboard { get { return scoreboard; } }
        /// <summary>
        /// The total scores of the simulation
        /// </summary>
        public int[] Scores { get { return scores; } }

        /// <summary>
        /// Creates a new instance of the class
        /// </summary>
        public ReversiMultiplayerSimulation()
        {
            Directory.CreateDirectory("dumps");
            players = new List<IReversiPlayer>();
            scoreboard = new int[players.Count, players.Count];
            scores = new int[players.Count];
        }

        /// <summary>
        /// Runs a simulation of a single match
        /// </summary>
        /// <param name="i1">Index of the white player</param>
        /// <param name="i2">Index of the black player</param>
        private void RunSimulation(int i1, int i2)
        {
            IReversiPlayer p1 = players[i1], p2 = players[i2];
            string filename = String.Format("dumps\\{0}_{1}.txt", p1.GetName(), p2.GetName());
            StreamWriter sw = new StreamWriter(filename);
            Disc winner = ReversiGame.PlayGame(p1, p2, sw);
            Scoreboard[i1, i2] = (int)winner;
            sw.Close();
        }

        /// <summary>
        /// Calculates the final scores
        /// 2 - Victory, 1 - Draw, 0 - Defeat
        /// </summary>
        private void CalculateScores()
        {
            scores = new int[players.Count];
            for (int i = 0; i < players.Count; i++)
            {
                for (int j = 0; j < players.Count; j++)
                {
                    Scores[i] += Scoreboard[i, j] - Scoreboard[j, i] + 2;
                }
                Scores[i] -= 2;
            }
        }

        /// <summary>
        /// Adds a player to the simulation
        /// </summary>
        /// <param name="player">An IReversiPlayer object</param>
        public void AddPlayer(IReversiPlayer player)
        {
            players.Add(player);
        }

        /// <summary>
        /// Runs a full All vs. All simulation
        /// </summary>
        public void RunSimulation()
        {
            scoreboard = new int[players.Count, players.Count];
            for (int i = 0; i < players.Count; i++)
            {
                for (int j = 0; j < players.Count; j++)
                {
                    Console.WriteLine("Running game {0} out of {1}: {2} vs. {3}",
                        i * players.Count + j + 1,
                        players.Count * players.Count,
                        players[i].GetName(),
                        players[j].GetName());
                    RunSimulation(i, j);
                }
            }
            CalculateScores();
            OutputResults();
        }

        /// <summary>
        /// Outputs the scoreboard and final scores to csv files.
        /// </summary>
        private void OutputResults()
        {
            StreamWriter sw = new StreamWriter("scoreboard.csv");
            sw.WriteLine("," + String.Join(",", from p in players select p.GetName()));
            for (int i = 0; i < players.Count; i++)
            {
                sw.WriteLine(players[i].GetName() + "," + 
                    String.Join(",", from j in Enumerable.Range(0,players.Count) select scoreboard[i,j]));
            }
            sw.Close();
            sw = new StreamWriter("scores.csv");
            for (int i = 0; i < players.Count; i++)
            {
                sw.WriteLine(players[i].GetName() + "," + scores[i]);
            }
            sw.Close();
        }
    }
}