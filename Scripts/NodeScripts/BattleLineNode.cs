using System.Collections.Generic;
using Godot;

namespace WME.Nodes
{
    public partial class BattleLineNode : Node2D
    {
        [Export] PackedScene cardPrefab;

        BattleLine battleLine;
        List<CardNode> cardInstances = new();

        float cardSpacing = 300;
        float cardMoveDuration = 0.5f;

        public void Init(BattleLine battleLine)
        {
            this.battleLine = battleLine;

            battleLine.CardSummoned += OnCardSummoned;
            battleLine.CardDied += OnCardDied;
            battleLine.CardMoved += OnCardMoved;
        }

        public override void _ExitTree()
        {
            if (battleLine != null)
            {
                battleLine.CardSummoned -= OnCardSummoned;
                battleLine.CardDied -= OnCardDied;
                battleLine.CardMoved -= OnCardMoved;
            }
        }

        public Vector2 GetSlotPosition(int slot)
        {
            return new Vector2(slot * cardSpacing, 0);
        }

        public void OnCardSummoned(CardSummonedEventArgs args)
        {
            CardNode cardInstance = cardPrefab.Instantiate<CardNode>();
            AddChild(cardInstance);
            cardInstance.Position = GetSlotPosition(args.Position);
            cardInstance.Init(args.Card);
            cardInstances.Add(cardInstance);
        }

        public void OnCardDied(CardDiedEventArgs args)
        {
            var cardInstance = cardInstances[args.Slot];
            cardInstance.QueueFree();
        }

        public void OnCardMoved(CardMovedEventArgs args)
        {
            var moveTween = CreateTween();
            moveTween.TweenProperty(cardInstances[args.FromPosition], "Position", GetSlotPosition(args.ToPosition), cardMoveDuration);
            cardInstances[args.ToPosition] = cardInstances[args.FromPosition];
        }
    }
}
