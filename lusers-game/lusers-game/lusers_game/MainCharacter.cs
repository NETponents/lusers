using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lusers_game
{
    public class MainCharacter : Character
    {
        public MainCharacter(string spritePath, int animFrames, string characterName, Vector2 startPosition) : base(spritePath, animFrames, characterName, startPosition)
        {

        }
        public void Update(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Position.Y -= 5;
                //if(!currentRoom.isInsideMapBounds(Position))
                //{
                //    Position.Y -= 5;
                //}
                _hasMoved = true;
                animDirection = WalkingDirection.Up;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Position.X -= 5;
                //if (!currentRoom.isInsideMapBounds(Position))
                //{
                //    Position.X -= 5;
                //}
                _hasMoved = true;
                animDirection = WalkingDirection.Left;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Position.Y += 5;
                //if (!currentRoom.isInsideMapBounds(Position))
                //{
                //    Position.Y += 5;
                //}
                _hasMoved = true;
                animDirection = WalkingDirection.Down;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Position.X += 5;
                //if (!currentRoom.isInsideMapBounds(Position))
                //{
                //    Position.X += 5;
                //}
                _hasMoved = true;
                animDirection = WalkingDirection.Right;
            }
            base.Update(gd, ref sb, cm, ref gt);
        }
    }
}
