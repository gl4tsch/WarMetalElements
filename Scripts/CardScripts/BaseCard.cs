using Godot;
using System.Collections.Generic;

namespace WME
{
    public abstract class BaseCard
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string PortraitPath { get; }
        public abstract Dictionary<Element, int> Cost { get; }
        public abstract int Attack { get; }
        public abstract int Health { get; }

        public virtual void OnPlay()
        {

        }

        public virtual void OnDeath()
        {

        }
    }
}