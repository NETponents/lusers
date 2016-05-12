using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lusers_game
{
    public class ScreenManager
    {
        /// <summary>
        /// Font for FPS counter.
        /// </summary>
        protected SpriteFont fontFPSCounter;
        private Dictionary<string, Screen> screenList;
        private string currentScreen;

        public ScreenManager()
        {
            screenList = new Dictionary<string, Screen>();
        }

        public ScreenManager(string initialScreenName, Screen initialScreen)
        {
            screenList = new Dictionary<string, Screen>();
            screenList.Add(initialScreenName, initialScreen);
            currentScreen = initialScreenName;
        }

        public void Awake(ContentManager cm)
        {
            
        }

        public void Draw(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt)
        {
            if (!screenList[currentScreen].isScreenLoaded())
            {
                screenList[currentScreen].Load(gd, cm);
            }
            screenList[currentScreen].Draw(gd, ref sb, cm, ref gt);
            // Calculate framerate.
            float frameRate = 1 / (float)gt.ElapsedGameTime.TotalSeconds;
            sb.DrawString(fontFPSCounter, frameRate + " FPS", new Vector2(2, 2), Color.Yellow);
        }

        public void Load(GraphicsDevice gd, ContentManager cm)
        {
            foreach(KeyValuePair<string, Screen> kv in screenList)
            {
                kv.Value.Load(gd, cm);
            }
            fontFPSCounter = cm.Load<SpriteFont>("fonts/fpscounter");
        }

        public void Sleep(ContentManager cm)
        {
            
        }

        public void Unload(GraphicsDevice gd, ContentManager cm)
        {
            
        }

        public void Update(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt)
        {
            if (!screenList[currentScreen].isScreenLoaded())
            {
                screenList[currentScreen].Load(gd, cm);
            }
            screenList[currentScreen].Update(gd, ref sb, cm, ref gt);
        }

        public void switchScreenContext(string screenName)
        {
            if(screenList.ContainsKey(screenName))
            {
                currentScreen = screenName;
            }
        }
        public string getCurrentScreen()
        {
            return currentScreen;
        }
    }
    public class ScreenNotFoundException : Exception
    {

    }
}
