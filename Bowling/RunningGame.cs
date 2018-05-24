using System.Collections.Generic;

namespace Bowling
{
    internal class RunningGame : IRunningGame
    {
        private readonly IList<Player> players = new List<Player>();

        public Player ActivePlayer => playerEnumerator.Current;

        private readonly IEnumerator<Player> playerEnumerator;

        public RunningGame(IList<Player> players)
        {
            this.players = players;
            playerEnumerator = GetPlayer().GetEnumerator();
            playerEnumerator.MoveNext();
        }

        public void Roll(int pins)
        {
            ActivePlayer.AddScore(pins);
            playerEnumerator.MoveNext();
        }

        public IEnumerable<Player> GetPlayer()
        {
            while (true)
            {
                foreach (var player in players)
                {
                    yield return player;
                    yield return player;
                }
            }
        }
    }
}