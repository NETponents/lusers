using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lusers_game
{
    public class Furnature : IGameObject, ICollidable
    {
        protected Vector2 drawRoot;
        protected Texture2D sprite_o1;
        public string objectName;
        protected Vector2 size;
        private string texName;
        public string claimedBy;
        //////////////////////////
        private SpriteFont testOwnership;
        //////////////////////////

        public Furnature(Vector2 origin, string spriteName, string name, Vector2 objectSize)
        {
            drawRoot = origin;
            texName = spriteName;
            objectName = name;
            size = objectSize;
            claimedBy = "";
        }

        public void Awake(ContentManager cm)
        {

        }

        public void Draw(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, Vector2 drawOrigin)
        {
            Vector2 relativeRoot = drawRoot + drawOrigin;
            Vector2 scaledSize = size * new Vector2(100, 100);
            sb.Draw(sprite_o1, new Rectangle((int)relativeRoot.X, (int)relativeRoot.Y, (int)scaledSize.X, (int)scaledSize.Y), Color.White);
            //////////////////////////
            if (!claimedBy.Equals(""))
            {
                sb.DrawString(testOwnership, claimedBy + "'s " + objectName, drawRoot - new Vector2(0, 30) + drawOrigin, Color.DarkBlue);
            }
            //////////////////////////
        }

        public void Load(GraphicsDevice gd, ContentManager cm)
        {
            sprite_o1 = cm.Load<Texture2D>(texName);
            //////////////////////////
            testOwnership = cm.Load<SpriteFont>("fonts/healthfloat");
            //////////////////////////
        }

        public void Sleep(ContentManager cm)
        {

        }

        public void Unload(GraphicsDevice gd, ContentManager cm)
        {

        }

        public void Update(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, Vector2 drawOrigin)
        {

        }
        public Rectangle getBoundingBox()
        {
            return new Rectangle((int)drawRoot.X, (int)drawRoot.Y, (int)size.X * 100, (int)size.Y * 100);
        }
        public Vector2 getOrigin()
        {
            return drawRoot;
        }
        public void setOwner(string owner)
        {
            claimedBy = owner;
        }
    }
}
