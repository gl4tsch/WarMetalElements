using Godot;
using System;

namespace WME
{
    public partial class BattleScene : Node
    {
        Fighter me = new();
        Fighter enemy = new();
        BattleRoundResolver battleRoundResolver = new();

        public override void _Ready()
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

            battleRoundResolver.ResolveClashes(me, enemy);

            GD.Print();
            GD.Print(me.Hero);
            GD.Print(me.BattleLine);
            GD.Print(enemy.BattleLine);
            GD.Print(enemy.Hero);
        }
    }
}
