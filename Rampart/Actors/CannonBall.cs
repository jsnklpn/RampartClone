using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Rampart.BaseClasses;
using Rampart.HelperClasses;

namespace Rampart.Actors
{
    public class CannonBall : KillableGameObjectBase
    {
        private int _cannonOwner;
        private Brush _brush;
        private Point _targetLocation;

        public CannonBall(int cannonOwner, Point size, Point currentLocation, Point targetLocation, float velocity)
            : base(size.Y, size.X, currentLocation.X, currentLocation.Y)
        {
            _cannonOwner = cannonOwner;
            _brush = new SolidBrush(Color.DarkGray);
            _targetLocation = targetLocation;
            CalculateVelocityVectors(velocity);
        }

        public CannonBall(int cannonOwner, PointF size, PointF currentLocation, PointF targetLocation, float velocity)
            : this(cannonOwner, size.ToPoint(), currentLocation.ToPoint(), targetLocation.ToPoint(), velocity)
        {
        }

        private void CalculateVelocityVectors(float velocity)
        {
            var vector = new PointF(_targetLocation.X - _floatX, _targetLocation.Y - _floatY);
            vector = vector.Normalize(velocity);
            VelocityX = vector.X;
            VelocityY = vector.Y;
        }

        public override void Move()
        {
            float distanceToTargetBefore = _targetLocation.Subtract(RealPosition).Magnitude();

            base.Move();

            // After we move, detect if we reached our target
            float distanceToTargetAfter = _targetLocation.Subtract(RealPosition).Magnitude();
            if (distanceToTargetAfter > distanceToTargetBefore && HitPoints > 0)
            {
                HitPoints = 0;
                VelocityX = 0;
                VelocityY = 0;
                SetPosition(_targetLocation);
            }
        }

        protected override void OnPaint(Graphics gfx, Rectangle drawableArea)
        {
            gfx.FillEllipse(_brush, X, Y, Width, Height);
            gfx.DrawEllipse(Pen, X, Y, Width, Height);
        }
    }
}
