using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Rampart.HelperClasses
{
    public static class ExtensionMethods
    {
        public static float Magnitude(this PointF A)
        {
            return (float)Math.Sqrt(A.X * A.X + A.Y * A.Y);
        }

        public static PointF Normalize(this PointF A, float length = 1.0f)
        {
            float distance = A.Magnitude();
            return new PointF((A.X / distance) * length, (A.Y / distance) * length);
        }

        public static PointF Subtract(this PointF A, PointF vectorToSubtract)
        {
            return new PointF(A.X - vectorToSubtract.X, A.Y - vectorToSubtract.Y);            
        }

        public static PointF Subtract(this PointF A, Point vectorToSubtract)
        {
            return new PointF(A.X - vectorToSubtract.X, A.Y - vectorToSubtract.Y);
        }

        public static PointF Subtract(this Point A, PointF vectorToSubtract)
        {
            return new PointF(A.X - vectorToSubtract.X, A.Y - vectorToSubtract.Y);
        }

        public static Point Subtract(this Point A, Point vectorToSubtract)
        {
            return new Point(A.X - vectorToSubtract.X, A.Y - vectorToSubtract.Y);
        }

        public static PointF ToPointF(this Point A)
        {
            return new PointF(A.X, A.Y);
        }

        public static Point ToPoint(this PointF A)
        {
            return new Point((int)A.X, (int)A.Y);
        }
    }
}
