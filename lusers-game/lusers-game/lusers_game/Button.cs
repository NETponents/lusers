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
    public class Button : IGameObject
    {
        protected string _buttonText;
        protected Vector2 _drawLocation;
        protected Action _triggerFunction;
        protected Texture2D _backgroundImage;
        protected Vector2 _stringLength
        {
            get
            {
                return _textFont.MeasureString(_buttonText);
            }
        }
        private string _backgroundPath;
        private SpriteFont _textFont;
        private Rectangle drawRect;

        public Button(string text, Vector2 location, Action f)
        {
            _buttonText = text;
            _drawLocation = location;
            _triggerFunction = f;
        }

        public Button(string text, Vector2 location, Action f, string backgroundPath)
        {
            _buttonText = text;
            _drawLocation = location;
            _triggerFunction = f;
            _backgroundPath = backgroundPath;
        }

        public void Awake(ContentManager cm)
        {
            
        }

        public void Draw(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, Vector2 drawOrigin)
        {
            if(_backgroundImage != null)
            {
                sb.Draw(_backgroundImage, drawRect, Color.White);
            }
            sb.DrawString(_textFont, _buttonText, _drawLocation + new Vector2(10, 10), Color.Yellow);
        }

        public void Load(GraphicsDevice gd, ContentManager cm)
        {
            if (_backgroundPath != null)
            {
                _backgroundImage = cm.Load<Texture2D>(_backgroundPath);
            }
            _textFont = cm.Load<SpriteFont>("fonts/buttontext");
            Vector2 lowerRightCorner = _stringLength + new Vector2(20, 20);
            drawRect = new Rectangle((int)_drawLocation.X, (int)_drawLocation.Y, (int)lowerRightCorner.X, (int)lowerRightCorner.Y);
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
            if(ms.LeftButton == ButtonState.Pressed)
            {
                if (Geometry.Vector2DIntersectsRectangle(drawRect, new Vector2(ms.X, ms.Y)))
                {
                    triggerAction();
                }
            }
        }

        protected void triggerAction()
        {
            _triggerFunction();
        }
    }
}
