using System;
using System.Collections.Generic;

namespace WME
{
    public class BattleLine
    {
        List<BaseCard> units = new();
        public int Count => units.Count;

        public event Action<CardSummonedEventArgs> CardSummoned;
        public event Action<CardDiedEventArgs> CardDied;
        public event Action<CardMovedEventArgs> CardMoved;

        public BaseCard this[int idx]
        {
            get => idx < 0 || idx >= units.Count ? null : units[idx];
            set => units[idx] = value;
        }

        public BaseCard GetCardAt(int idx)
        {
            return idx >= units.Count ? null : units[idx];
        }

        public void Summon(BaseCard card)
        {
            units.Add(card);
            CardSummoned?.Invoke(new(card, units.Count - 1));
        }

        public void Transform(int idx, BaseCard to)
        {
            units[idx] = to;
        }

        public void Remove(int idx)
        {
            units[idx] = null;
        }

        public void TriggerDeaths()
        {
            for (int i = 0; i < units.Count; i++)
            {
                if (units[i] == null) continue;
                if (units[i].IsDead)
                {
                    units[i].OnDeath();
                    CardDied?.Invoke(new(units[i], i));
                    units[i] = null;
                }
            }
        }

        public void CloseRanks()
        {
            for (int i = 0; i < units.Count; i++)
            {
                if (units[i] == null)
                {
                    // find next non empty slot
                    for (int j = i + 1; j < units.Count; j++)
                    {
                        if (units[j] != null)
                        {
                            MoveCard(j, i);
                            break;
                        }
                    }
                    
                    // no more units in the list
                    if (units[i] == null)
                    {
                        units.RemoveRange(i, units.Count - i);
                        break;
                    }
                }
            }
        }

        void MoveCard(int from, int to)
        {
            // move unit to the left
            units[to] = units[from];
            units[from] = null;
            CardMoved?.Invoke(new(from, to));
        }

        public override string ToString()
        {
            return string.Join(" | ", units);
        }
    }
}