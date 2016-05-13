using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace lusers_game
{
    namespace Characters
    {
        public class Employee : NonPlayerCharacter
        {
            public Employee(Vector2 startPosition, string name, string imagePath)
                    : base(imagePath, 4, name, startPosition, WalkingDirection.Left)
            {

            }

            public override void Update(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, Vector2 drawOrigin, RoomScreen currentRoom)
            {
                base.Update(gd, ref sb, cm, ref gt, drawOrigin, currentRoom);
            }

            protected override Vector2 getNextWaypoint()
            {
                if (claimedDesk != null)
                {
                    targetWaypoint = claimedDesk.getOrigin();
                }
                Rectangle r = new Rectangle((int)Position.X - 40, (int)Position.Y - 40, 80, 80);
                if (Geometry.Vector2DIntersectsRectangle(r, targetWaypoint))
                {
                    return Position;
                }
                else
                {
                    return targetWaypoint;
                }
            }
        }
    }
}
