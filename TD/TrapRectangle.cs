using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TD
{
    class TrapRectangle
    {
        readonly List<Rectangle> _trapRectangleList;

        public TrapRectangle(List<Vector2> path, int wide)
        {
            _trapRectangleList = new List<Rectangle>();
            for (int i = 1; i < path.Count; i++)
            {
                if (path[i].X == path[i - 1].X)
                    if (path[i].Y > path[i - 1].Y)
                        _trapRectangleList.Add(new Rectangle(
                            (int)path[i - 1].X - wide, (int)path[i - 1].Y - wide,
                            wide * 2, (int)path[i].Y + wide - (int)path[i - 1].Y + wide));
                    else
                        _trapRectangleList.Add(new Rectangle(
                            (int)path[i].X - wide, (int)path[i].Y - wide,
                            wide * 2, (int)path[i - 1].Y + wide - (int)path[i].Y + wide));
                if (path[i].Y == path[i - 1].Y)
                    if (path[i].X > path[i - 1].X)
                        _trapRectangleList.Add(new Rectangle(
                            (int)path[i - 1].X - wide, (int)path[i - 1].Y - wide,
                            (int)path[i].X + wide - (int)path[i - 1].X + wide, wide * 2));
                    else
                        _trapRectangleList.Add(new Rectangle(
                            (int)path[i].X - wide, (int)path[i].Y - wide,
                            (int)path[i - 1].X + wide - (int)path[i].X + wide, wide * 2));
            }
        }

        public bool Intersects(Rectangle rectangle)
        {
            bool temp = false;
            for (int i = 0; i < _trapRectangleList.Count; i++)
                if (_trapRectangleList[i].Intersects(rectangle))
                {
                    temp = true;
                    break;
                }
            return temp;
        }

    }
}
