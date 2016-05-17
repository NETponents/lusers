using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lusers_game
{
    public class NavigationService
    {
        private int _blockWidth;
        private int _blockHeight;
        private int _mapWidth;
        private int _mapHeight;
        private Queue<Vector2> _navigationList;
        private Vector2 _currentWayPoint;

        public void Awake(ContentManager cm)
        {
            
        }

        public void Draw(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, Vector2 drawOrigin, RoomScreen rs)
        {
            
        }

        public void Load(GraphicsDevice gd, ContentManager cm, Map mp)
        {
            _blockWidth = 100;
            _blockHeight = 100;
            _mapHeight = mp.mapHeight;
            _mapWidth = mp.mapWidth;
        }

        public void Sleep(ContentManager cm)
        {
            
        }

        public void Unload(GraphicsDevice gd, ContentManager cm)
        {
            
        }

        public void Update(RoomScreen rs, bool[,] collisionMap, Vector2 currentPosition)
        {
            // TODO: Only check points on currently selected path for new collisions.
        }

        private bool wayPointIsInBounds(Vector2 wp)
        {
            return Geometry.Vector2DIntersectsRectangle(new Rectangle(0, 0, _mapWidth * _blockWidth, _mapHeight * _blockHeight), wp);
        }
        private bool matrixWayPointIsInBounds(Vector2 wp)
        {
            return Geometry.Vector2DIntersectsRectangle(new Rectangle(0, 0, _mapWidth, _mapHeight), wp);
        }
        public void setWayPoint(Vector2 wp, RoomScreen rs, ref bool[][] collisionMap, Vector2 currentPosition)
        {
            if(matrixWayPointIsInBounds(wp))
            {
                _currentWayPoint = wp;
                refreshNodeMap(rs, ref collisionMap, currentPosition);
            }
            else
            {
                throw new NotInsideBoundsException();
            }
        }
        private bool isClear(Vector2 tileCoordinates, RoomScreen rs)
        {
            tileCoordinates.X *= _blockWidth;
            tileCoordinates.Y *= _blockHeight;
            tileCoordinates.X += 5;
            tileCoordinates.Y += 5;
            foreach(Furnature g in rs.gameObjects)
            {
                if(Geometry.Vector2DIntersectsRectangle(g.getBoundingBox(), tileCoordinates))
                {
                    return false;
                }
            }
            return true;
        }
    }
    [Serializable]
    public class NotInsideBoundsException : Exception
    {
        public NotInsideBoundsException() { }
        public NotInsideBoundsException(string message) : base(message) { }
        public NotInsideBoundsException(string message, Exception inner) : base(message, inner) { }
        protected NotInsideBoundsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}
