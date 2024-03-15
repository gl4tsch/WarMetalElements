using Godot;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WME.Nodes
{
    public partial class BattleScene : Node
    {
        [Export] BattleLineNode ownBattleLine, enemyBattleLine;

        Fighter me = new();
        Fighter enemy = new();
        BattleRoundResolver battleRoundResolver = new();

        public override async void _Ready()
        {
            me.BattleLine.Summon(new Salamander());
            me.BattleLine.Summon(new Imp());
            me.BattleLine.Summon(new Imp());
            me.BattleLine.Summon(new Salamander());
            me.BattleLine.Summon(new Imp());
            me.BattleLine.Summon(new Imp());
            me.BattleLine.Summon(new Salamander());
            enemy.BattleLine.Summon(new Phoenix());
            enemy.BattleLine.Summon(new Phoenix());
            enemy.BattleLine.Summon(new Phoenix());
            enemy.BattleLine.Summon(new Phoenix());
            enemy.BattleLine.Summon(new Salamander());
            enemy.BattleLine.Summon(new Phoenix());
            enemy.BattleLine.Summon(new Phoenix());
            enemy.BattleLine.Summon(new Phoenix());

            GD.Print(me.Hero);
            GD.Print(me.BattleLine);
            GD.Print(enemy.BattleLine);
            GD.Print(enemy.Hero);

            await BattleRound();

            GD.Print();
            GD.Print(me.Hero);
            GD.Print(me.BattleLine);
            GD.Print(enemy.BattleLine);
            GD.Print(enemy.Hero);
        }

        async Task BattleRound()
        {
            var maxLineLength = Math.Max(me.BattleLine.Count, enemy.BattleLine.Count);

            for (int i = 0; i < maxLineLength; i++)
            {
                BaseCard ownCard = me.BattleLine.GetCardAt(i);
                BaseCard enemyCard = enemy.BattleLine.GetCardAt(i);

                ownCard?.Attack(i, me, enemy);
                enemyCard?.Attack(i, enemy, me);

                var ownAnims = ownBattleLine.GatherAnimations();
                var enemyAnims = enemyBattleLine.GatherAnimations();
                
                await Task.WhenAll(ownAnims.Select(async a => await ToSignal(a, Tween.SignalName.Finished)));

                me.BattleLine.TriggerDeaths();
                enemy.BattleLine.TriggerDeaths();
            }
        }
    }
}
