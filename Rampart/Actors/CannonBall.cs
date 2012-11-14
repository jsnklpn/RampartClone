using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Rampart.BaseClasses;

namespace Rampart.Actors
{
    public class CannonBall : KillableGameObjectBase
    {
        private Brush _brush;

        public CannonBall()
        {
            HitPoints = 1;
            _brush = new SolidBrush(Color.DarkGray);
        }

        protected override void OnPaint(Graphics gfx, Rectangle drawableArea)
        {
            gfx.FillEllipse(_brush, X, Y, Width, Height);
            gfx.DrawEllipse(Pen, X, Y, Width, Height);
        }
    }
}
