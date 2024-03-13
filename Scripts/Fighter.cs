
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
    }
}