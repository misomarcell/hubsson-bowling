using System.Collections.Generic;

namespace Bowling
{
    public interface IRunningGame
    {
        Player ActivePlayer { get; }
        IList<Player> Players { get; }

        void Roll(int pins);
        int GetScoreOf(Player player);
        int GetScoreOf(string name);

        string Winner { get; }
    }
}
