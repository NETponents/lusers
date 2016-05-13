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
        protected Desk claimedDesk;

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

        public override void Update(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, Vector2 drawOrigin)
        {
            // Verify that we own claimedDesk.
            bool ownsADesk = false;
            foreach (IGameObject j in WorldObjectHolder.objects)
            {
                if (j.GetType() == typeof(Desk))
                {
                    if ((j as Desk).claimedBy == Name)
                    {
                        ownsADesk = true;
                        break;
                    }
                }
            }
            if (!ownsADesk)
            {
                // Desk was deleted or manually claimed by another NPC.
                foreach (IGameObject j in WorldObjectHolder.objects)
                {
                    if (j.GetType() == typeof(Desk))
                    {
                        if ((j as Desk).claimedBy == null)
                        {
                            (j as Desk).claimedBy = Name;
                            claimedDesk = (j as Desk);
                            break;
                        }
                    }
                }
            }
            // Check to see if we have moved.
            Vector2 newPos = getNextWaypoint();
            if (newPos != Position)
            {
                _hasMoved = true;
            }
            Position = Vector2.Lerp(newPos, Position, 0.96f);
            // Call Character.Update(...) for updates.
            base.Update(gd, ref sb, cm, ref gt, drawOrigin);
        }
        public void setWayPoint(Vector2 newPosition)
        {
            targetWaypoint = newPosition;
        }
    }
}
