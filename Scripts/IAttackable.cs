namespace WME
{
    public interface IAttackable
    {
        public void ReceiveAttack(BaseCard attacker, int attackValue);
    }
}