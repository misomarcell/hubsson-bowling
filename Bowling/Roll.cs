namespace Bowling
{
    internal class Roll
    {

        public Player Player { get; }
        public int? Pins { get; set; }
        public Roll(Player player)
        {
            Player = player;
        }
    }
}