using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Rampart.Interfaces;
using Rampart.BaseClasses;
using Rampart.HelperClasses;

namespace Rampart.Actors
{
    public class PlayerCannon : KillableGameObjectBase
    {
        private static int UIDCounter = 0;
        private int _cannonUID;

        public int PlayerOwner { get; set; }
        public Pen CannonPen { get; set; }
        public int CannonLength { get; set; }
        public bool HasFired { get; set; }
        public float CannonSpeed { get; set; }
        public int ID { get { return _cannonUID; } }
        public IGameObject ObjectToPointAt { get; set; }

        public PlayerCannon(int owner)
        {
            _cannonUID = UIDCounter++;
            PlayerOwner = owner;
            HasFired = false;
            CannonSpeed = 7.0f;
            ObjectToPointAt = null;
            HitPoints = 100;
            CannonPen = new Pen(this.Pen.Color, this.Pen.Width * 5);
            CannonAutoSizeLength();
        }

        public void CannonAutoSizeLength()
        {
            CannonLength = (Width + Height) / 2;
        }

        public PointF PointAt(IGameObject obj)
        {
            if (obj != null)
            {
                var p2 = obj.RealPosition;
                var p1 = this.RealPosition;
                PointF p3 = new PointF(p2.X - p1.X, p2.Y - p1.Y);
                return p3.Normalize();
            }
            else
            {
                return new PointF(0.707f, 0.707f); // 45 degree angle
            }
        }

        protected override void OnPaint(Graphics gfx, Rectangle drawableArea)
        {
            var normal = PointAt(ObjectToPointAt);

            var center = Center;
            //var barrelWidth = CannonPen.Width;
            gfx.DrawRectangle(Pen, X, Y, Width, Height);
            gfx.DrawEllipse(Pen, X, Y, Width, Height);
            //gfx.FillEllipse(new SolidBrush(CannonPen.Color), center.X - 1, center.Y - 1, 2, 2);
            gfx.DrawLine(CannonPen, center.X, center.Y, center.X + (CannonLength * normal.X), center.Y + (CannonLength * normal.Y));
        }
    }
}
