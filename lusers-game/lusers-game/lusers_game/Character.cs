using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lusers_game
{
    public class Character : IGameObject
    {
        protected Texture2D _sprite;
        protected int _animFrames;
        public string Name
        {
            get;
            protected set;
        }
        private string _spritePath;
        public Vector2 Position;
        protected int animStep
        {
            get
            {
                return _animStep;
            }
            set
            {
                _animStep = value % 4;
            }
        }
        private int _animStep;
        protected const int _animDirections = 4;
        protected WalkingDirection animDirection
        {
            get
            {
                return _animDirection;
            }
            set
            {
                if(value != _animDirection)
                {
                    animStep = 0;
                    _animDirection = value;
                }
            }
        }
        private WalkingDirection _animDirection;
        protected bool _hasMoved;
        protected int _animSpacer = 0;
        public Vector2 actualPosition;

        public Character(string spritePath, int animFrames, string characterName, Vector2 startPosition)
        {
            Name = characterName;
            _animFrames = animFrames;
            _spritePath = spritePath;
            Position = startPosition;
            animStep = 0;
            _animDirection = WalkingDirection.Right;
        }

        public void Awake(ContentManager cm)
        {
            
        }

        public void Draw(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, Vector2 drawOrigin)
        {

            int width = _sprite.Width / _animFrames;
            int height = _sprite.Height / _animDirections;
            int row = mapDirection(_animDirection);
            int column = animStep;
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)(drawOrigin.X + actualPosition.X), (int)(drawOrigin.Y + actualPosition.Y), 32, 48);
            sb.Draw(_sprite, destinationRectangle, sourceRectangle, Color.White);
        }

        public void Load(GraphicsDevice gd, ContentManager cm)
        {
            _sprite = cm.Load<Texture2D>(_spritePath);
        }

        public void Sleep(ContentManager cm)
        {
            
        }

        public void Unload(GraphicsDevice gd, ContentManager cm)
        {
            
        }

        public void Update(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt)
        {
            if(_hasMoved)
            {
                _animSpacer++;
                if(_animSpacer > 3)
                {
                    _animSpacer = 0;
                    animStep++;
                }
            }
            else
            {
                animStep = 0;
                _animSpacer = 0;
            }
            _hasMoved = false;
            actualPosition = Vector2.Lerp(actualPosition, Position, 0.1f);
        }
        public virtual int mapDirection(WalkingDirection wd)
        {
            if(wd == WalkingDirection.Up)
            {
                return 3;
            }
            if (wd == WalkingDirection.Down)
            {
                return 0;
            }
            if (wd == WalkingDirection.Left)
            {
                return 2;
            }
            if (wd == WalkingDirection.Right)
            {
                return 1;
            }
            return 0;
        }
    }
}
