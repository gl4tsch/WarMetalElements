using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WME.Nodes
{
    public partial class BattleScene : Node
    {
        [Export] BattleLineNode ownBattleLine, enemyBattleLine;
        [Export] HeroNode ownHero, enemyHero;

        public Battle Battle = new(new(), new());
        Fighter player => Battle.Player;
        Fighter enemy => Battle.Enemy;

        bool manualAdvanceStep = false;

        public override async void _Ready()
        {
            ownBattleLine.Init(player.BattleLine, Vector2.Up);
            enemyBattleLine.Init(enemy.BattleLine, Vector2.Down);
            ownHero.Init(player.Hero);
            enemyHero.Init(enemy.Hero);

            player.Summon(new MagmaSlime());
            player.Summon(new Salamander());
            player.Summon(new Imp());
            player.Summon(new Ash());
            enemy.Summon(new Phoenix());
            enemy.Summon(new Emberling());
            enemy.Summon(new Imp());

            var summonAnimations = ownBattleLine.GatherAnimations();
            summonAnimations.AddRange(enemyBattleLine.GatherAnimations());
            await TweensToTask(summonAnimations);

            DebugPrintBoardState();
            await BattleRound();
            DebugPrintBoardState();
            await BattleRound();
            DebugPrintBoardState();
            await BattleRound();
            DebugPrintBoardState();
        }

        async Task BattleRound()
        {
            var maxLineLength = Math.Max(player.BattleLine.Count, enemy.BattleLine.Count);

            for (int i = 0; i < maxLineLength; i++)
            {
                BaseCard playerCard = player.BattleLine.GetCardAt(i);
                BaseCard enemyCard = enemy.BattleLine.GetCardAt(i);

                await Task.Delay(200);
                await CardClash(i, playerCard, enemyCard);
            }

            RoundEnd();

            var roundEndAnimations = ownBattleLine.GatherAnimations();
            roundEndAnimations.AddRange(enemyBattleLine.GatherAnimations());
            
            await TweensToTask(roundEndAnimations);

            player.BattleLine.CloseRanks();
            enemy.BattleLine.CloseRanks();

            var moveAnimations = ownBattleLine.GatherAnimations();
            moveAnimations.AddRange(enemyBattleLine.GatherAnimations());

            await TweensToTask(moveAnimations);

            ownBattleLine.TrimNullCards();
            enemyBattleLine.TrimNullCards();
        }

        async Task CardClash(int slot, BaseCard playerCard, BaseCard enemyCard)
        {
            int playerCardAttackCount = 0;
            int enemyCardAttackCount = 0;

            while (playerCardAttackCount < playerCard?.BaseAttackCount || enemyCardAttackCount < enemyCard?.BaseAttackCount)
            {
                if (playerCard?.BaseAttackCount > playerCardAttackCount)
                {
                    playerCard?.Attack(slot, player, enemy);
                    playerCardAttackCount++;
                }

                if (enemyCard?.BaseAttackCount > enemyCardAttackCount)
                {
                    enemyCard?.Attack(slot, enemy, player);
                    enemyCardAttackCount++;
                }

                var attackAnimations = ownBattleLine.GatherAnimations();
                attackAnimations.AddRange(enemyBattleLine.GatherAnimations());
                attackAnimations.AddRange(ownHero.GetAnimations());
                attackAnimations.AddRange(enemyHero.GetAnimations());

                await TweensToTask(attackAnimations);
                await Task.Delay(200);

                player.BattleLine.TriggerDeaths(player, enemy);
                enemy.BattleLine.TriggerDeaths(enemy, player);

                var deathAnimations = ownBattleLine.GatherAnimations();
                deathAnimations.AddRange(enemyBattleLine.GatherAnimations());

                await TweensToTask(deathAnimations);
            }
        }

        void RoundEnd()
        {
            for (int i = 0; i < player.BattleLine.Count; i++)
            {
                player.BattleLine[i]?.OnRoundEnd(i, player, enemy);
            }
            for (int i = 0; i < enemy.BattleLine.Count; i++)
            {
                enemy.BattleLine[i]?.OnRoundEnd(i, enemy, player);
            }
        }

        Task TweensToTask(List<Tween> tweens)
        {
            return Task.WhenAll(tweens.Select(async a => await ToSignal(a, Tween.SignalName.Finished)));
        }

        public void DebugPrintBoardState()
        {
            GD.Print(enemy.Hero);
            GD.Print(enemy.BattleLine);
            GD.Print(player.BattleLine);
            GD.Print(player.Hero);
        }
    }
}
