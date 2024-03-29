using System;
using System.Collections.Generic;

namespace WME
{
    public class Imp : BaseCard
    {
        public override string Name => "Imp";

        public override string Description => "an Imp";

        public override string PortraitPath => "Imp.png";

        public override Dictionary<Element, int> Cost => new() {{Element.Fire, 1}};

        public override int BaseAttack => 1;

        public override int BaseHealth => 1;

    }
}