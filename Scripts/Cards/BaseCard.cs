using System;
using System.Collections.Generic;

namespace WME
{
    public abstract class BaseCard : IAttackable
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string PortraitPath { get; }
        public abstract Dictionary<Element, int> Cost { get; }
        public abstract int BaseAttack { get; }
        public virtual int BaseAttackCount => 1;
        public abstract int BaseHealth { get; }
        public int CurrentHealth { get; protected set; }
        public bool IsDead => CurrentHealth <= 0;

        // Animation Trigger Events
        public Action Attacked;
        public Action<(int oldHp, int newHp)> CurrentHealthChanged;
        public Action Died;

        public BaseCard()
        {
            CurrentHealth = BaseHealth;
        }

        public virtual void OnPlay()
        {

        }

        public virtual void OnDeath(int slot, Fighter owner, Fighter enemy)
        {
            Died?.Invoke();
        }

        public virtual void OnRoundEnd(int slot, Fighter owner, Fighter enemy)
        {

        }

        public virtual void Attack(int slot, Fighter owner, Fighter enemy)
        {
            enemy.GetAttackableAt(slot).ReceiveAttack(this, BaseAttack);
            Attacked?.Invoke();
        }

        protected void SpreadAttack(int slot, Fighter owner, Fighter enemy)
        {
            enemy.GetAttackableAt(slot - 1)?.ReceiveAttack(this, BaseAttack);
            enemy.GetAttackableAt(slot)?.ReceiveAttack(this, BaseAttack);
            enemy.GetAttackableAt(slot + 1)?.ReceiveAttack(this, BaseAttack);
            Attacked?.Invoke();
        }

        public virtual void ReceiveAttack(BaseCard attacker, int attackValue)
        {
            ReceiveDamage(attackValue);
        }

        public virtual void ReceiveDamage(int damage)
        {
            int prevHp = CurrentHealth;
            CurrentHealth -= damage;
            CurrentHealthChanged?.Invoke((prevHp, CurrentHealth));
        }

        public override string ToString()
        {
            return ToDebugString();
        }

        public string ToDebugString()
        {
            return $"{Name} {BaseAttack} {CurrentHealth}";
        }
    }
}