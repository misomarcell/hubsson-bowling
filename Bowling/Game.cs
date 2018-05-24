using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bowling
{
    public class GameBuilder
    {
        private readonly ICollection<Player> players = new List<Player>();

        public IEnumerable<Player> Players => players;

        public void AddPlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            if (players.Contains(player))
                throw new PlayerAlreadyExistException();

            players.Add(player);
        }

        public IRunningGame Start()
        {
            if (!HasPlayers())
                throw new InvalidOperationException("Game cannot be started without players. Please add at least one before starting the game.");

            return new RunningGame(players.ToList());
        }

        private bool HasPlayers() => players.Any();
    }
}
