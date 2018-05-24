namespace Bowling
{
    public interface IRunningGame
    {
        Player ActivePlayer { get; }

        void Roll(int pins);
    }
}
