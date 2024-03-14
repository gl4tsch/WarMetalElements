using System.Collections.Generic;

namespace WME
{
    public class Ash : BaseCard
    {
        public override string Name => "Ash";

        public override string Description => "On round end transform into Phoenix";

        public override string PortraitPath => "icon.svg";

        public override Dictionary<Element, int> Cost => new() {{Element.Fire, 1}};

        public override int AttackValue => 0;

        public override int HealthValue => 3;

        public override void OnPlay()
        {
            // subscribe to round end event somewhere?
        }
    }
}