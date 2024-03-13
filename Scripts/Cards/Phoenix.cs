using System.Collections.Generic;

namespace WME
{
    public class Phoenix : BaseCard
    {
        public override string Name => "Phoenix";

        public override string Description => $"On death transform into Ash";

        public override string PortraitPath => "icon.svg";

        public override Dictionary<Element, int> Cost => new() {{Element.Fire, 3}};

        public override int AttackValue => 3;

        public override int HealthValue => 2;

        public override void OnDeath()
        {
            // summon ash
        }
    }
}