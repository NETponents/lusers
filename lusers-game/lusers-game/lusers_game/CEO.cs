using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lusers_game
{
    namespace Characters
    {
        public class CEO : NonPlayerCharacter
        {
            public CEO(Vector2 startPosition)
                : base("img/characters/guy-black", 4, "CEO", startPosition, WalkingDirection.Left)
            {
                
            }

            protected override Vector2 getNextWaypoint()
            {
                Rectangle r = new Rectangle((int)Position.X - 40, (int)Position.Y - 40, 80, 80);
                if(Geometry.Vector2DIntersectsRectangle(r, targetWaypoint))
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
