using System;
using System.Collections.Generic;
using System.Text;

namespace Bowling
{
    public class Game
    {
        private readonly ICollection<Player> players = new List<Player>();

        public IEnumerable<Player> Players => players;

        public void AddPlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));


            players.Add(player);    
        }
    }
}
