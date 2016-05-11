using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lusers_game
{
    public class MouseTool : IGameObject
    {
        private Texture2D texBlocked;
        private Texture2D texClear;
        public MouseToolState toolState;
        private MouseState oldMouseState;

        public MouseTool()
        {

        }

        public void Awake(ContentManager cm)
        {
            
        }

        public void Draw(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, Vector2 drawOrigin)
        {
            if(toolState == MouseToolState.Builder)
            {
                MouseState ms = Mouse.GetState();
                Vector2 rectRoot = new Vector2(ms.X, ms.Y);
                // I don't know why this works but it does.
                rectRoot += drawOrigin * new Vector2(-1, -1);
                rectRoot.X = (int)(rectRoot.X / 100.0f) * 100;
                rectRoot.Y = (int)(rectRoot.Y / 100.0f) * 100;
                rectRoot += drawOrigin;
                sb.Draw(texClear, new Rectangle((int)rectRoot.X, (int)rectRoot.Y, 100, 100), Color.White);
            }
        }

        public void Load(GraphicsDevice gd, ContentManager cm)
        {
            texBlocked = cm.Load<Texture2D>("img/tools/placerblocked");
            texClear = cm.Load<Texture2D>("img/tools/placerclear");
            toolState = MouseToolState.Selector;
            oldMouseState = Mouse.GetState();
        }

        public void Sleep(ContentManager cm)
        {
            
        }

        public void Unload(GraphicsDevice gd, ContentManager cm)
        {
            
        }

        public void Update(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, Vector2 drawOrigin)
        {
            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed)
            {
                Vector2 rectRoot = new Vector2(ms.X, ms.Y);
                rectRoot += drawOrigin * new Vector2(-1, -1);
                rectRoot.X = (int)(rectRoot.X / 100.0f) * 100;
                rectRoot.Y = (int)(rectRoot.Y / 100.0f) * 100;
                //rectRoot += drawOrigin;
                Desk d = new Desk(rectRoot);
                d.Load(gd, cm);
                WorldObjectHolder.objects.Add(d);
            }
            oldMouseState = ms;
        }
    }
}
