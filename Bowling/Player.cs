using System;

namespace Bowling
{
    public class Player
    {
        public int Score { get; private set; }

        internal void AddScore(int pins)
        {
            Score += pins;
        }
    }
}
