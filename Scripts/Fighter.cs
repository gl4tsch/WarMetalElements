
namespace WME
{
    public class Fighter
    {
        public BaseHero Hero = new JohnDoe();
        public BattleLine BattleLine = new();
        public ManaPool ManaPool = new();

        public IAttackable GetAttackableAt(int position)
        {
            return (IAttackable)BattleLine[position] ?? Hero;
        }

        public bool Summon(BaseCard card, bool payManaCost = true)
        {
            if (payManaCost && !ManaPool.PayCost(card))
            {
                return false;
            }

            BattleLine.Summon(card);
            card.OnPlay();
            return true;
        }
    }
}