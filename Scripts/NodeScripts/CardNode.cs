using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

namespace WME.Nodes
{
    public partial class CardNode : Node2D
    {
        [Export] TextureRect portrait;
        [Export] Label costLabel, atkLabel, hpLabel;
        static string portraitFolderPath = "res://Art/CardPortraits/";

        BaseCard card;
        Vector2 forward;
        Queue<Tween> animations = new();

        public void Init(BaseCard card, Vector2 forward)
        {
            this.card = card;
            this.forward = forward;
            portrait.Texture = GD.Load<Texture2D>(portraitFolderPath + card.PortraitPath);
            costLabel.Text = card.Cost.ToDebugString();
            atkLabel.Text = card.BaseAttack.ToString();
            hpLabel.Text = card.CurrentHealth.ToString();

            card.Attacked += QueueAttackAnimation;
            card.CurrentHealthChanged += OnHealthChanged;
            card.Died += QueueDeathAnimation;

            Scale = Vector2.Zero;
            animations.Enqueue(SummonAnimation());
        }

        public override void _ExitTree()
        {
            if (card != null)
            {
                card.Attacked -= QueueAttackAnimation;
                card.CurrentHealthChanged -= OnHealthChanged;
                card.Died -= QueueDeathAnimation;
            }
        }

        public Queue<Tween> GetAnimations()
        {
            Queue<Tween> returnQueue = new();
            while(animations.Count > 0)
            {
                returnQueue.Enqueue(animations.Dequeue());
            }
            return returnQueue;
        }

        Tween SummonAnimation()
        {
            Tween tween = GetTree().CreateTween();
            tween.TweenProperty(this, "scale", Vector2.One, 1f);
            return tween;
        }

        void QueueAttackAnimation()
        {
            animations.Enqueue(AttackAnimation(forward));
        }

        Tween AttackAnimation(Vector2 forward)
        {
            float moveAmount = 40f;
            Tween tween = GetTree().CreateTween();
            tween.TweenProperty(this, "position", Position - forward * (moveAmount / 2f), 0.2f);
            tween.TweenProperty(this, "position", Position + forward * moveAmount, 0.2f).SetTrans(Tween.TransitionType.Back);
            tween.TweenProperty(this, "position", Position, 0.2f);
            return tween;
        }

        public void QueueMoveAnimation(Vector2 targetPos)
        {
            animations.Enqueue(MoveAnimation(targetPos));
        }

        Tween MoveAnimation(Vector2 targetPos)
        {
            Tween tween = GetTree().CreateTween();
            tween.TweenProperty(this, "position", targetPos, 0.2f);
            return tween;
        }

        void OnHealthChanged((int oldHp, int newHp) hpChange)
        {
            hpLabel.Text = hpChange.newHp.ToString();
        }

        void QueueDeathAnimation()
        {
            animations.Enqueue(DeathAnimation());
        }

        Tween DeathAnimation()
        {
            Tween tween = GetTree().CreateTween();
            tween.TweenProperty(this, "scale", Vector2.Zero, 0.5f);
            tween.SetTrans(Tween.TransitionType.Bounce);
            tween.TweenCallback(Callable.From(() => QueueFree()));
            return tween;
        }

        public Tween ReduceHealthAnimation(int toHp)
        {
            Tween tween = GetTree().CreateTween();
            tween.TweenCallback(Callable.From(() => hpLabel.AddThemeColorOverride("font_color", new Color(1,0,0))));
            tween.TweenInterval(0.2f);
            tween.TweenCallback(Callable.From(() => hpLabel.Text = toHp.ToString()));
            tween.TweenInterval(0.2f);
            tween.TweenCallback(Callable.From(() => hpLabel.RemoveThemeColorOverride("font_color")));
            return tween;
        }

        public Tween IncreaseHealthAnimation(int toHp)
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
