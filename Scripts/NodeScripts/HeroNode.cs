using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

namespace WME.Nodes
{
    public partial class HeroNode : Node2D
    {
        [Export] TextureRect portrait;
        [Export] Label hpLabel;
        static string portraitFolderPath = "res://Art/CardPortraits/";

        BaseHero hero;
        Queue<Tween> animations = new();

        public void Init(BaseHero hero)
        {
            this.hero = hero;

            portrait.Texture = GD.Load<Texture2D>(portraitFolderPath + hero.PortraitPath);
            hpLabel.Text = hero.CurrentHealth.ToString();

            hero.CurrentHealthChanged += OnHealthChanged;
        }

        public override void _ExitTree()
        {
            if (hero != null)
            {
                hero.CurrentHealthChanged -= OnHealthChanged;
            }
        }

        public Queue<Tween> GetAnimations()
        {
            Queue<Tween> returnQueue = new();
            while (animations.Count > 0)
            {
                returnQueue.Enqueue(animations.Dequeue());
            }
            return returnQueue;
        }

        void OnHealthChanged((int oldHp, int newHp) hpChange)
        {
            if (hpChange.oldHp > hpChange.newHp)
            {
                animations.Enqueue(ReduceHealthAnimation(hpChange.newHp));
            }
            else
            {
                animations.Enqueue(IncreaseHealthAnimation(hpChange.newHp));
            }
        }

        Tween ReduceHealthAnimation(int toHp)
        {
            Tween tween = GetTree().CreateTween();
            tween.TweenInterval(0.4f);
            tween.TweenCallback(Callable.From(() => hpLabel.AddThemeColorOverride("font_color", new Color(1, 0, 0))));
            tween.TweenInterval(0.2f);
            tween.TweenCallback(Callable.From(() => hpLabel.Text = toHp.ToString()));
            tween.TweenInterval(0.2f);
            tween.TweenCallback(Callable.From(() => hpLabel.RemoveThemeColorOverride("font_color")));
            return tween;
        }

        Tween IncreaseHealthAnimation(int toHp)
        {
            Tween tween = GetTree().CreateTween();
            tween.TweenCallback(Callable.From(() => hpLabel.AddThemeColorOverride("font_color", new Color(0, 1, 0))));
            tween.TweenInterval(0.2f);
            tween.TweenCallback(Callable.From(() => hpLabel.Text = toHp.ToString()));
            tween.TweenInterval(0.2f);
            tween.TweenCallback(Callable.From(() => hpLabel.RemoveThemeColorOverride("font_color")));
            return tween;
        }
    }
}