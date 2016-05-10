using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lusers_game
{
    interface IGameObject
    {
        void Load(GraphicsDevice gd, ContentManager cm);
        void Awake(ContentManager cm);
        void Draw(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, Vector2 drawOrigin);
        void Update(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt);
        void Sleep(ContentManager cm);
        void Unload(GraphicsDevice gd, ContentManager cm);
    }
}
