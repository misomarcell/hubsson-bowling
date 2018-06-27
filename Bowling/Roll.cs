using System;

namespace Bowling
{
    internal class Roll
    {
        private int remainingBonuses = 0;
        private int? _pins;
                
        public Player Player { get; }
        public int? Pins {
            get => _pins;
            set
            {
                _pins = value;

                if (IsStrike)
                    remainingBonuses = 2;
            }
        }
        public bool IsStrike => Pins.HasValue && Pins.Value >= 10;

        public bool IsPendingStrike => IsStrike && remainingBonuses > 0;

        public Roll(Player player)
        {
            Player = player;
        }

        internal void AddBonus(int pins)
        {
            _pins += pins;
            --remainingBonuses;
        }
    }
}