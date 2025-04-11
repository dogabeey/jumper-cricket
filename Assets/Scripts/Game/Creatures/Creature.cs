namespace Lionsfall
{
    public abstract class Creature : GridElement
    {
        public abstract bool IsPlayer { get; set; }
        public abstract int InitialHealth { get; set; }
        public abstract int TurnSpeed { get; set; }

        internal int health;
    }
}