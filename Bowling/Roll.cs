using System;

namespace Bowling
{
    internal class Roll
    {        
        public int PinsKnocked
        {
            get; private set;
        }

        public Roll(int pins)
        {
            PinsKnocked = pins;
        }


    }
}