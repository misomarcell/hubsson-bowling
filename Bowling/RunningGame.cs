using System;
using System.Collections.Generic;
using System.Linq;

namespace Bowling
{
    internal class RunningGame : IRunningGame
    {
        private readonly ICollection<Player> players = new List<Player>();

        public RunningGame(ICollection<Player> players)
        {
            this.players = players;
        }

        public void Roll(int pins)
        {
            var currentPlayer = GetCurrentPlayer();
            currentPlayer.AddScore(pins);
        }

        private Player GetCurrentPlayer()
        {
            return players.ElementAt(0); // TODO: Implement this
        }
    }
}