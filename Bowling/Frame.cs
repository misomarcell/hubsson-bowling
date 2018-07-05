using System.Linq;

namespace Bowling
{
    public class Frame
    {
        public Player Player { get; }
        private Roll[] rolls = new Roll[2];
        private int remainingBonuses;
        private int bonusPins;

        public int? Score
        {
            get
            {
                if (!IsFinished || HasRemainingBonus)
                    return null;

                return TotalKnockedPins + bonusPins;
            }
        }

        public bool HasRemainingBonus => remainingBonuses > 0;

        public bool IsFinished => IsStrike || rolls.All(x => x != null);

        public bool IsStrike => rolls[0]?.PinsKnocked == 10;

        public bool IsSpare => !IsStrike && TotalKnockedPins == 10;

        private int TotalKnockedPins => rolls.Sum(x => x?.PinsKnocked ?? 0);

        public Frame(Player player)
        {
            Player = player;
        }

        public void Roll(int pins)
        {
            if (rolls[0] == null)
            {
                rolls[0] = new Roll(pins);
            }
            else if (rolls[1] == null)
            {
                rolls[1] = new Roll(pins);
            }
            else
            {
                throw new FrameIsAlreadyNukedWithYourBalls();
            }
            remainingBonuses = IsStrike ? 2 : (IsSpare ? 1 : 0);
        }

        public void AddBonus(int bonus)
        {
            --remainingBonuses;
            bonusPins += bonus;
        }
    }
}
