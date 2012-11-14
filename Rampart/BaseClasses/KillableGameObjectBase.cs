using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rampart.Interfaces;

namespace Rampart.BaseClasses
{
    public abstract class KillableGameObjectBase : GameObjectBase, IKillable
    {
        public int HitPoints { get; set; }
        public bool IsDead { get { return HitPoints <= 0; } }

        public KillableGameObjectBase() : this(50, 50) { }
        public KillableGameObjectBase(int height, int width) : this(height, width, 0, 0) { }
        public KillableGameObjectBase(int height, int width, int x, int y)
            : base(height, width, x, y)
        {
            HitPoints = 1;
        }
    }
}
