using System.Collections.Generic;

namespace WME
{
    public class Ash : BaseCard
    {
        public override string Name => "Ash";

        public override string Description => "On round end transform into Phoenix";

        public override string PortraitPath => "icon.svg";

        public override Dictionary<Element, int> Cost => new() {{Element.Fire, 1}};

        public override int BaseAttack => 0;

        public override int BaseHealth => 3;

        public override void OnRoundEnd(int slot, Fighter owner, Fighter enemy)
        {
            OnDeath(slot, owner, enemy);
            owner.BattleLine.SummonAt(new Phoenix(), slot);
        }
    }
}