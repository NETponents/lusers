using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lusers_game
{
    public static class Geometry
    {
        public static bool Vector2DIntersectsRectangle(Rectangle r, Vector2 v)
        {
            if(v.X >= r.X && v.X <= r.X + r.Width && v.Y >= r.Y && v.Y <= r.Y + r.Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
