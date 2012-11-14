using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Rampart.Interfaces;

namespace Rampart.BaseClasses
{
    public abstract class GameObjectBase : IGameObject
    {
        public int X { get { return (int)_floatX; } set { _floatX = value; } }
        public int Y { get { return (int)_floatY; } set { _floatY = value; } }
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }
        public float AccelX { get; set; }
        public float AccelY { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public bool Visible { get; set; }

        public int Left { get { return this.X; } }
        public int Top { get { return this.Y; } }
        public int Right { get { return this.X + this.Width; } }
        public int Bottom { get { return this.Y + this.Height; } }
        public Point Center { get { return new Point((Left + Right) / 2, (Top + Bottom) / 2); } }
        public PointF RealPosition { get { return new PointF(_floatX, _floatY); } }

        public Pen Pen { get; set; }

        protected float _floatX;
        protected float _floatY;

        public GameObjectBase() : this(50, 50) { }
        public GameObjectBase(int height, int width) : this(height, width, 0, 0) { }
        public GameObjectBase(int height, int width, int x, int y)
        {
            VelocityX = 0;
            VelocityY = 0;
            AccelX = 0;
            AccelY = 0;
            Height = height;
            Width = width;
            Visible = true;
            _floatX = x;
            _floatY = y;
            Pen = new Pen(Color.White, 1.0f);
        }

        public void SetPosition(Point position)
        {
            _floatX = position.X;
            _floatY = position.Y;
        }

        public virtual void Move()
        {
            _floatX += VelocityX;
            _floatY += VelocityY;

            // Calculate new velocities
            VelocityX += AccelX;
            VelocityY += AccelY;
        }

        public void Draw(Graphics gfx, Rectangle drawableArea)
        {
            if (Visible) OnPaint(gfx, drawableArea);
        }

        protected abstract void OnPaint(Graphics gfx, Rectangle drawableArea);
    }
}
