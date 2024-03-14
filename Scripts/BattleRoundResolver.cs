using System;

namespace WME
{
    // this will be the link between data and presentation?
    // steps through a battle round, triggering data changes and waiting for animations
    // TODO
    public class BattleRoundResolver
    {
        // one pair attacks (both)
        // dead units vanish
        // next pair
        // at the end: colse ranks (both)
        public void ResolveClashes(Fighter me, Fighter enemy)
        {
            var maxLineLength = Math.Max(me.BattleLine.Count, enemy.BattleLine.Count);

            for (int i = 0; i < maxLineLength; i++)
            {
                BaseCard ownCard = me.BattleLine.GetCardAt(i);
                BaseCard enemyCard = enemy.BattleLine.GetCardAt(i);

                ownCard?.Attack(i, me, enemy);
                enemyCard?.Attack(i, enemy, me);

                me.BattleLine.TriggerDeaths();
                enemy.BattleLine.TriggerDeaths();
            }

            me.BattleLine.CloseRanks();
            enemy.BattleLine.CloseRanks();
        }
    }
}