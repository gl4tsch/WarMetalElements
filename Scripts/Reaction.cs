
using System.Collections.Generic;

namespace WME
{
    public class Reaction
    {
        public enum ReactionTrigger
        {
            Play = 0,
            Death = 1,
            Attack = 2,
            ReceiveAttack = 3,
            ReceiveDamage = 4
        }

        public virtual void Execute(List<BaseCard> affectedCards)
        {

        }
    }
}