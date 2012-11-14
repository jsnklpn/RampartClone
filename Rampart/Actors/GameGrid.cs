using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Rampart.BaseClasses;

namespace Rampart.Actors
{
    public class GameGrid : GameObjectBase
    {
        public int CellNumHorizontal { get; private set; }
        public int CellNumVertical { get; private set; }
        public float CellHeight { get; private set; }
        public float CellWidth { get; private set; }
        public PointF CellSize { get { return new PointF(CellWidth, CellHeight); } }

        public GameGrid(int numCellsHorizontal, int numCellsVertical, Rectangle rect)
            : base(rect.Height, rect.Width, rect.X, rect.Y)
        {
            CellNumHorizontal = numCellsHorizontal;
            CellNumVertical = numCellsVertical;
            CalculateCellSize();
        }

        private void CalculateCellSize()
        {
            CellWidth = (float)Width / CellNumHorizontal;
            CellHeight = (float)Height / CellNumVertical;
        }

        public Point GetCellLocation(Point cell)
        {
            return GetCellLocation(cell.X, cell.Y);
        }

        public Point GetCellLocation(int horizontalCell, int verticalCell)
        {
            return new Point((int)(horizontalCell * CellWidth), (int)(verticalCell * CellHeight));
        }

        protected override void OnPaint(Graphics gfx, Rectangle drawableArea)
        {
            gfx.DrawRectangle(Pen, X, Y, Width, Height);

            for (int i = 0; i < CellNumHorizontal; i++)
            {
                int xloc = (int)((i + 1) * CellWidth);
                gfx.DrawLine(Pen, xloc, Top, xloc, Bottom);
            }
            for (int i = 0; i < CellNumVertical; i++)
            {
                int yloc = (int)((i + 1) * CellHeight);
                gfx.DrawLine(Pen, Left, yloc, Right, yloc);
            }
        }
    }
}
