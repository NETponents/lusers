using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lusers
{
    namespace Players
    {
        public class Character
        {
            protected string _name;
            protected Vector2 _location;
            protected Texture2D _sprite;

            public Character(Vector2 location, Texture2D sprite)
            {
                _location = location;
                _sprite = sprite;
                _name = "Unnamed";
            }
            public Character(Vector2 location, Texture2D sprite, string name)
            {
                _location = location;
                _sprite = sprite;
                _name = name;
            }
        }
    }
}
