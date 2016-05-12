using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lusers_game
{
    public abstract class Screen
    {
        protected bool _isLoaded;
        protected int _screenWidth;
        protected int _screenHeight;

        public Screen(int ScreenWidth, int ScreenHeight)
        {
            _isLoaded = false;
            _screenWidth = ScreenWidth;
            _screenHeight = ScreenHeight;
        }

        public virtual void Awake(ContentManager cm)
        {
            
        }

        public virtual void Draw(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt)
        {
            
        }

        public virtual void Load(GraphicsDevice gd, ContentManager cm)
        {
            _isLoaded = true;
        }

        public virtual void Sleep(ContentManager cm)
        {
            
        }

        public virtual void Unload(GraphicsDevice gd, ContentManager cm)
        {
            
        }

        public virtual void Update(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, ScreenManager sm)
        {
            
        }

        public virtual bool isScreenLoaded()
        {
            return _isLoaded;
        }
    }
}
