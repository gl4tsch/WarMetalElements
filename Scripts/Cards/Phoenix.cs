using System.Collections.Generic;

namespace WME
{
    public class Phoenix : BaseCard
    {
        public override string Name => "Phoenix";

        public override string Description => $"On death transform into Ash";

        public override string PortraitPath => "Phoenix.png";

        public override Dictionary<Element, int> Cost => new() {{Element.Fire, 3}};

        public override int BaseAttack => 3;

        public override int BaseHealth => 2;

        public override void OnDeath(int slot, Fighter owner, Fighter enemy)
        {
            base.OnDeath(slot, owner, enemy);

            // summon ash
            owner.BattleLine.Transform(slot, new Ash());
        }
    }
}