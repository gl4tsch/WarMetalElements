using System;

namespace WME
{
    public class CardSummonedEventArgs : EventArgs
    {
        public BaseCard Card { get; set; }
        public int Position { get; set; }

        public CardSummonedEventArgs(BaseCard card, int position)
        {
            Card = card;
            Position = position;
        }
    }
}