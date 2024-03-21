using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WME.Nodes
{
    public partial class BattleScene : Node
    {
        [Export] BattleLineNode ownBattleLine, enemyBattleLine;
        [Export] HeroNode ownHero, enemyHero;

        Fighter me = new();
        Fighter enemy = new();

        bool manualAdvanceStep = false;

        public override async void _Ready()
        {
            ownBattleLine.Init(me.BattleLine, Vector2.Up);
            enemyBattleLine.Init(enemy.BattleLine, Vector2.Down);
            ownHero.Init(me.Hero);
            enemyHero.Init(enemy.Hero);

            me.BattleLine.Summon(new Imp());
            me.BattleLine.Summon(new Salamander());
            me.BattleLine.Summon(new Imp());
            enemy.BattleLine.Summon(new Phoenix());
            enemy.BattleLine.Summon(new Imp());
            enemy.BattleLine.Summon(new Imp());

            var summonAnimations = ownBattleLine.GatherAnimations();
            summonAnimations.AddRange(enemyBattleLine.GatherAnimations());
            await TweensToTask(summonAnimations);

            GD.Print(enemy.Hero);
            GD.Print(enemy.BattleLine);
            GD.Print(me.BattleLine);
            GD.Print(me.Hero);

            await BattleRound();

            GD.Print();
            GD.Print(enemy.Hero);
            GD.Print(enemy.BattleLine);
            GD.Print(me.BattleLine);
            GD.Print(me.Hero);
        }

        async Task BattleRound()
        {
            var maxLineLength = Math.Max(me.BattleLine.Count, enemy.BattleLine.Count);

            for (int i = 0; i < maxLineLength; i++)
            {
                BaseCard ownCard = me.BattleLine.GetCardAt(i);
                BaseCard enemyCard = enemy.BattleLine.GetCardAt(i);

                await Task.Delay(200);

                ownCard?.Attack(i, me, enemy);
                enemyCard?.Attack(i, enemy, me);

                var attackAnimations = ownBattleLine.GatherAnimations();
                attackAnimations.AddRange(enemyBattleLine.GatherAnimations());
                attackAnimations.AddRange(ownHero.GetAnimations());
                attackAnimations.AddRange(enemyHero.GetAnimations());

                await TweensToTask(attackAnimations);
                await Task.Delay(200);

                me.BattleLine.TriggerDeaths();
                enemy.BattleLine.TriggerDeaths();

                var deathAnimations = ownBattleLine.GatherAnimations();
                deathAnimations.AddRange(enemyBattleLine.GatherAnimations());
                
                await TweensToTask(deathAnimations);
            }

            me.BattleLine.CloseRanks();
            enemy.BattleLine.CloseRanks();

            var moveAnimations = ownBattleLine.GatherAnimations();
            moveAnimations.AddRange(enemyBattleLine.GatherAnimations());

            await TweensToTask(moveAnimations);

            ownBattleLine.TrimNullCards();
            enemyBattleLine.TrimNullCards();
        }

        Task TweensToTask(List<Tween> tweens)
        {
            return Task.WhenAll(tweens.Select(async a => await ToSignal(a, Tween.SignalName.Finished)));
        }
    }
}
