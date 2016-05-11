using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lusers_game
{
    public abstract class NonPlayerCharacter : Character
    {
        protected Vector2 targetWaypoint;

        public NonPlayerCharacter(string spritePath, int animFrames, string characterName, Vector2 startPosition)
            : base(spritePath, animFrames, characterName, startPosition)
        {
            targetWaypoint = startPosition;
        }
        public NonPlayerCharacter(string spritePath, int animFrames, string characterName, Vector2 startPosition, WalkingDirection startDirection)
            : base(spritePath, animFrames, characterName, startPosition, startDirection)
        {
            targetWaypoint = startPosition;
        }

        protected abstract Vector2 getNextWaypoint();

        public new void Update(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, Vector2 drawOrigin)
        {
            Vector2 newPos = getNextWaypoint();
            if(newPos != Position)
            {
                _hasMoved = true;
            }
            Position = newPos;
            base.Update(gd, ref sb, cm, ref gt, drawOrigin);
        }
        public void setWayPoint(Vector2 newPosition)
        {
            targetWaypoint = newPosition;
        }
    }
}
