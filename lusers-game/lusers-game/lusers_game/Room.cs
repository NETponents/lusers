using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lusers_game
{
    public class Room : IGameObject
    {
        protected string _roomName;
        protected Map _roomMap;
        protected MainCharacter _playerCharacter;

        public Room()
        {

        }

        public void Awake(ContentManager cm)
        {
            // Not needed right now.
        }

        public void Draw(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, Vector2 drawOrigin)
        {
            _roomMap.Draw(gd, ref sb, cm, ref gt, drawOrigin);
            _playerCharacter.Draw(gd, ref sb, cm, ref gt, drawOrigin);
        }

        public void Load(GraphicsDevice gd, ContentManager cm)
        {
            _roomMap = new Map("img/tex/floor_1", 20, 20, 100, 100);
            _roomMap.Load(gd, cm);
            _playerCharacter = new MainCharacter("img/characters/guy", 4, "Player", new Vector2(200, 200));
            _playerCharacter.Load(gd, cm);

        }

        public void Sleep(ContentManager cm)
        {
            //throw new NotImplementedException();
        }

        public void Unload(GraphicsDevice gd, ContentManager cm)
        {
            //throw new NotImplementedException();
        }

        public void Update(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, Vector2 drawOrigin)
        {
            //throw new NotImplementedException();
            _playerCharacter.Update(gd, ref sb, cm, ref gt, drawOrigin);
        }

        public string getRoomName()
        {
            if(_roomName == null)
            {
                return "Unnamed";
            }
            else
            {
                return _roomName;
            }
        }

        public bool isInsideMapBounds(Vector2 point)
        {
            if (point.X >= 0 && point.Y >= 0 && point.X <= _roomMap.mapWidth && point.Y <= _roomMap.mapHeight)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Vector2 getPlayerCoordinates()
        {
            return _playerCharacter.Position * new Vector2(-1, -1);
        }
    }
}
