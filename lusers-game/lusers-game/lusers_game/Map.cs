using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lusers_game
{
    public class Map : IGameObject
    {
        private string mapFileName;
        private Texture2D mapTexture;
        private int texRows;
        private int texColumns;
        private int texWidth;
        private int texHeight;
        public int mapWidth
        {
            get
            {
                return texColumns * texWidth;
            }
        }
        public int mapHeight
        {
            get
            {
                return texRows * texHeight;
            }
        }

        public Map(string mapFName, int rows, int columns, int width, int height)
        {
            mapFileName = mapFName;
            texRows = rows;
            texColumns = columns;
            texWidth = width;
            texHeight = height;
        }

        public void Load(GraphicsDevice gd, ContentManager cm)
        {
            mapTexture = cm.Load<Texture2D>(mapFileName);
        }

        public void Awake(ContentManager cm)
        {
            // Nohting to do here.
        }

        public void Draw(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, Vector2 drawOrigin)
        {
            for(int x = 0; x < texColumns; x++)
            {
                for(int y = 0; y < texRows; y++)
                {
                    sb.Draw(mapTexture, new Rectangle((x * texWidth) + (int)drawOrigin.X, (y * texHeight) + (int)drawOrigin.Y, texWidth, texHeight), Color.White);
                }
            }
        }

        public void Update(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt)
        {
            // No updating needed unless inherited object specifically requests animation updates.
        }

        public void Sleep(ContentManager cm)
        {
            // Not needed since unload is controlled by Room parent object.
        }

        public void Unload(GraphicsDevice gd, ContentManager cm)
        {
            // GC will take care of this for us.
        }
    }
}
