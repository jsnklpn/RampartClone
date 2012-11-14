using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Rampart.BaseClasses;

namespace Rampart.Actors
{
    public class SimpleParticle : KillableGameObjectBase
    {
        public const int DEFAULT_LIFETIME = 50;

        public SimpleParticle(int lifeTime)
        {
            this.HitPoints = lifeTime;
            this.Pen = new System.Drawing.Pen(Color.White, 2.0f);
        }

        public SimpleParticle()
            : this(DEFAULT_LIFETIME)
        {
        }

        public override void Move()
        {
            base.Move();
            this.HitPoints--;
        }

        protected override void OnPaint(System.Drawing.Graphics gfx, System.Drawing.Rectangle drawableArea)
        {
            gfx.DrawLine(Pen, Center, Center);
        }
    }
}
