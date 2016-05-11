using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lusers_game
{
    public static class WorldObjectHolder
    {
        public static List<IGameObject> objects = new List<IGameObject>();

        public static void Draw(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, Vector2 drawOrigin)
        {
            foreach (IGameObject i in objects)
            {
                i.Draw(gd, ref sb, cm, ref gt, drawOrigin);
            }
        }
    }
}
