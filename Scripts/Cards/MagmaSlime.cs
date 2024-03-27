using System.Collections.Generic;

namespace WME
{
    public class MagmaSlime : BaseCard
    {
        public override string Name => "Magma Slime";

        public override string Description => "";

        public override string PortraitPath => "MagmaSlime.png";

        public override Dictionary<Element, int> Cost => new(){{Element.Fire, 2}};

        public override int BaseAttack => 1;

        public override int BaseHealth => 4;

        public override void ReceiveAttack(BaseCard attacker, int attackValue)
        {
            base.ReceiveAttack(attacker, attackValue);
            attacker.ReceiveDamage(3);
        }
    }
}