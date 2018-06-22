namespace Bowling
{
    internal class Roll
    {

        public Player Player { get; }
        public int? Pins { get; set; }
        public bool IsStrike => Pins.HasValue && Pins.Value == 10;

        public Roll(Player player)
        {
            Player = player;
        }
    }
}