using System.Collections.Generic;
using Godot;

namespace WME.Nodes
{
    public partial class BattleLineNode : Node2D
    {
        [Export] PackedScene cardPrefab;

        BattleLine battleLine;
        Vector2 forward;
        List<CardNode> cardInstances = new();

        float cardSpacing = 220;
        float cardMoveDuration = 0.5f;

        public void Init(BattleLine battleLine, Vector2 forward)
        {
            this.battleLine = battleLine;
            this.forward = forward;

            battleLine.CardSummoned += OnCardSummoned;
            battleLine.CardMoved += OnCardMoved;
        }

        public override void _ExitTree()
        {
            if (battleLine != null)
            {
                battleLine.CardSummoned -= OnCardSummoned;
                battleLine.CardMoved -= OnCardMoved;
            }
        }

        public Vector2 GetSlotPosition(int slot)
        {
            return new Vector2(slot * cardSpacing, 0);
        }

        public List<Tween> GatherAnimations()
        {
            List<Tween> cardAnimations = new();
            foreach (var card in cardInstances)
            {
                if (card == null) continue;
                Queue<Tween> cardAnims = card.GetAnimations();
                while (cardAnims.Count > 0)
                {
                    cardAnimations.Add(cardAnims.Dequeue());
                }
            }
            return cardAnimations;
        }

        public void OnCardSummoned(CardSummonedEventArgs args)
        {
            CardNode cardInstance = cardPrefab.Instantiate<CardNode>();
            AddChild(cardInstance);
            cardInstance.Position = GetSlotPosition(args.Position);
            cardInstance.Init(args.Card, forward);
            if (cardInstances.Count > args.Position)
            {
                cardInstances[args.Position] = cardInstance;
            }
            else
            {
                cardInstances.Add(cardInstance);
            }
        }

        public void OnCardMoved(CardMovedEventArgs args)
        {
            cardInstances[args.FromPosition].QueueMoveAnimation(GetSlotPosition(args.ToPosition));
            cardInstances[args.ToPosition] = cardInstances[args.FromPosition];
            cardInstances[args.FromPosition] = null;
        }

        public void TrimNullCards()
        {
            cardInstances.RemoveAll(c => c == null);
        }
    }
}
