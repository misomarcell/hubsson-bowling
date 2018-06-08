using System;

namespace Bowling
{
    public class Player
    {
        public string Name { get; }
        public Player(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public bool Equals(Player other)
        {
            return Name.Equals(other?.Name);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return Equals((Player)obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
