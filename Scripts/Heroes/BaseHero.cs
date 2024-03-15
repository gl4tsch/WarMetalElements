namespace WME
{
    public abstract class BaseHero : IAttackable
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract int HealthValue { get; }
        public int CurrentHealth;

        public BaseHero()
        {
            CurrentHealth = HealthValue;
        }

        public virtual void ReceiveAttack(BaseCard attacker, int attackValue)
        {
            CurrentHealth -= attackValue;
        }

        public override string ToString()
        {
            return $"{Name} {CurrentHealth}";
        }
    }
}