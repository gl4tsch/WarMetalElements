using System;

namespace WME
{
    public abstract class BaseHero : IAttackable
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string PortraitPath { get; }
        public abstract int HealthValue { get; }
        public int CurrentHealth;

        public Action<(int oldHp, int newHp)> CurrentHealthChanged;

        public BaseHero()
        {
            CurrentHealth = HealthValue;
        }

        public virtual void ReceiveAttack(BaseCard attacker, int attackValue)
        {
            int prevHp = CurrentHealth;
            CurrentHealth -= attackValue;
            CurrentHealthChanged?.Invoke((prevHp, CurrentHealth));
        }

        public override string ToString()
        {
            return $"{Name} {CurrentHealth}";
        }
    }
}