using System.Drawing;

namespace Rampart.Interfaces
{
    public interface IGameObject
    {
        void Draw(Graphics gfx, Rectangle drawableArea);
        void Move();
        bool Visible { get; set; }
        int X { get; set; }
        int Y { get; set; }
        void SetPosition(Point position);
        float VelocityX { get; set; }
        float VelocityY { get; set; }
        float AccelX { get; set; }
        float AccelY { get; set; }
        int Height { get; set; }
        int Width { get; set; }
        int Left { get; }
        int Top { get; }
        int Right { get; }
        int Bottom { get; }
        Point Center { get; }
        PointF RealPosition { get; }
        Pen Pen { get; set; }
    }
}
