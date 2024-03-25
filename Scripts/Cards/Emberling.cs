using System.Collections.Generic;

namespace WME
{
    public class Emberling : BaseCard
    {
        public override string Name => "Emberling";

        public override string Description => "";

        public override string PortraitPath => "Emberling.png";

        public override Dictionary<Element, int> Cost => new(){{Element.Fire, 2}};

        public override int BaseAttack => 1;

        public override int BaseAttackCount => 2;

        public override int BaseHealth => 2;
    }
}