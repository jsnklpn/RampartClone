using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Rampart.BaseClasses;

namespace Rampart.Actors
{
    public class XBox : GameObjectBase
    {
        protected override void OnPaint(Graphics gfx, Rectangle drawableArea)
        {
            gfx.DrawRectangle(Pen, X, Y, Width, Height);
            gfx.DrawLine(Pen, Left, Top, Right, Bottom);
            gfx.DrawLine(Pen, Left, Bottom, Right, Top);
        }
    }
}
