using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

namespace WME
{
    public class BattleRoundResolver
    {
        public void ResolveClashes(Fighter me, Fighter enemy)
        {
            var maxLineLength = Mathf.Max(me.BattleLine.Count, enemy.BattleLine.Count);

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