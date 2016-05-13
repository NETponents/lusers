using Microsoft.Xna.Framework;
using System;

namespace lusers_game
{
    public class Wall : Furnature
    {
        public Wall(Vector2 origin)
            : base(origin, "img/objects/wall", "Wall", new Vector2(1, 1))
        {

        }
    }
}
