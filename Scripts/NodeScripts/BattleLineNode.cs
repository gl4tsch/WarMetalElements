using System.Collections.Generic;
using Godot;

namespace WME.Nodes
{
    public partial class BattleLineNode : Node2D
    {
        [Export] PackedScene cardPrefab;

        float cardSpacing = 300;
        BattleLine battleLine;
        List<CardNode> cardInstances = new();

        public void Init(BattleLine battleLine)
        {
            this.battleLine = battleLine;

            battleLine.CardSummoned += OnCardSummoned;
        }

        public override void _ExitTree()
        {
            if (battleLine != null)
            {
                battleLine.CardSummoned -= OnCardSummoned;
            }
        }

        public void OnCardSummoned(CardSummonedEventArgs args)
        {
            CardNode cardInstance = cardPrefab.Instantiate<CardNode>();
            AddChild(cardInstance);
            cardInstance.Position = new Vector2(args.Position * cardSpacing, 0);
            cardInstance.Init(args.Card);
        }
    }
}
