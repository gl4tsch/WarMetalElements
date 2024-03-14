using System;

namespace WME
{
    public class CardMovedEventArgs : EventArgs
    {
        public int FromPosition { get; set; }
        public int ToPosition { get; set; }

        public CardMovedEventArgs(int from, int to)
        {
            FromPosition = from;
            ToPosition = to;
        }
    }
}