using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Reflection;

namespace ReversiArtificialIntelligence
{
    class Program
    {
        

        /// <summary>
        /// Main function: Runs a full multiplayer simulation and dumps the data
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ReversiMultiplayerSimulation sim = CreateMultplayerSimulation();
            sim.RunSimulation();
        }

        /// <summary>
        /// Creates simulation of all players implementing IReversiPlayer
        /// </summary>
        /// <returns>A new ReversiMultiplayerSimulation instance</returns>
        private static ReversiMultiplayerSimulation CreateMultplayerSimulation()
        {
            ReversiMultiplayerSimulation sim = new ReversiMultiplayerSimulation();

            foreach (Type t in Assembly.GetCallingAssembly().GetTypes())
            {
                if (t.GetInterface("IReversiPlayer") != null)
                {
                    IReversiPlayer player = Activator.CreateInstance(t) as IReversiPlayer;
                    sim.AddPlayer(player);
                }
            }
            return sim;
        }

        /// <summary>
        /// Runs an action under a given timeout.
        /// Throws an exception if the action timed out.
        /// </summary>
        /// <param name="action">Action to preform</param>
        /// <param name="timeout">Timeout in miliseconds</param>
        public static void RunActionWithTimeout(Action action, int timeout)
        {
            TaskFactory tf = new TaskFactory();
            Task task = tf.StartNew(action);
            try
            {
                if (!task.Wait(timeout))
                    throw new TimeoutException("Task timed out");
            }
            catch (AggregateException e)
            {
                throw e.InnerException;
            }
            if (task.IsFaulted)
            {
                throw task.Exception;
            }
            return;

        }
    }
}
