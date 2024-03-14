using System;

namespace WME
{
    public class CardDiedEventArgs : EventArgs
    {
        public BaseCard Card { get; set; }
        public int Slot { get; set; }

        public CardDiedEventArgs(BaseCard card, int slot)
        {
            Card = card;
            Slot = slot;
        }
    }
}