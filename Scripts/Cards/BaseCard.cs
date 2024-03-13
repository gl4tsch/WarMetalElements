using System.Collections.Generic;

namespace WME
{
    public abstract class BaseCard : IAttackable
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string PortraitPath { get; }
        public abstract Dictionary<Element, int> Cost { get; }
        public abstract int AttackValue { get; }
        public abstract int HealthValue { get; }
        public int CurrentHealth { get; protected set; }
        public bool IsDead => CurrentHealth <= 0;

        public BaseCard()
        {
            CurrentHealth = HealthValue;
        }

        public virtual void OnPlay()
        {

        }

        public virtual void OnDeath()
        {

        }

        public virtual void Attack(int position, Fighter owner, Fighter enemy)
        {
            enemy.GetAttackableAt(position).ReceiveAttack(AttackValue);
        }

        protected void SpreadAttack(int position, Fighter owner, Fighter enemy)
        {
            enemy.GetAttackableAt(position - 1)?.ReceiveAttack(AttackValue);
            enemy.GetAttackableAt(position)?.ReceiveAttack(AttackValue);
            enemy.GetAttackableAt(position + 1)?.ReceiveAttack(AttackValue);
        }

        public virtual void ReceiveAttack(int attackValue)
        {
            CurrentHealth -= attackValue;
        }

        public override string ToString()
        {
            return ToDebugString();
        }

        public string ToDebugString()
        {
            return $"{Name} {AttackValue} {CurrentHealth}";
        }
    }
}