using System.Collections.Generic;

namespace WME
{
    public class Salamander : BaseCard
    {
        public override string Name => "Salamander";

        public override string Description => "Spread attack";

        public override string PortraitPath => "icon.svg";

        public override Dictionary<Element, int> Cost => new() {{Element.Fire, 2}};

        public override int AttackValue => 1;

        public override int HealthValue => 3;

        public override void Attack(int position, Fighter owner, Fighter enemy)
        {
            SpreadAttack(position, owner, enemy);
        }
    }
}