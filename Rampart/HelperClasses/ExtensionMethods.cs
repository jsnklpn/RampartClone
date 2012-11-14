using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Rampart.HelperClasses
{
    public static class ExtensionMethods
    {
        public static PointF Normalize(this PointF A)
        {
            float distance = (float)Math.Sqrt(A.X * A.X + A.Y * A.Y);
            return new PointF(A.X / distance, A.Y / distance);
        }
    }
}
