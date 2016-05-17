using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lusers_game
{
    public class Desk : Furnature
    {
        public Desk(Vector2 origin) : base(origin, "img/objects/desk", "Desk", new Vector2(1, 1))
        {
            
        }
    }
}
