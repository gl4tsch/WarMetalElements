using Godot;
using System;

namespace WME.Nodes
{
    public partial class CardNode : Node2D
    {
        [Export] Sprite2D portrait;
        [Export] Label costLabel, atkLabel, hpLabel;
        static string portraitFolderPath = "res://Art/CardPortraits/";

        public void Init(BaseCard card)
        {
            portrait.Texture = GD.Load<Texture2D>(portraitFolderPath + card.PortraitPath);
            costLabel.Text = card.Cost.ToDebugString();
            atkLabel.Text = card.AttackValue.ToString();
            hpLabel.Text = card.CurrentHealth.ToString();
        }
    }
}
