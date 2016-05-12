using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lusers_game
{
    public class Character : IGameObject, ICollidable
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
        public float characterHealth;
        private SpriteFont fontHealthFloat;
        public float walkSpeed;

        public Character(string spritePath, int animFrames, string characterName, Vector2 startPosition)
        {
            Name = characterName;
            _animFrames = animFrames;
            _spritePath = spritePath;
            Position = startPosition;
            animStep = 0;
            _animDirection = WalkingDirection.Right;
            characterHealth = 100.0f;
            walkSpeed = 0;
        }

        public Character(string spritePath, int animFrames, string characterName, Vector2 startPosition, WalkingDirection startDirection)
        {
            Name = characterName;
            _animFrames = animFrames;
            _spritePath = spritePath;
            Position = startPosition;
            animStep = 0;
            _animDirection = startDirection;
            characterHealth = 100.0f;
            walkSpeed = 0;
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
            if (characterHealth != 100.0f || GetType() == typeof(MainCharacter))
            {
                sb.DrawString(fontHealthFloat, characterHealth + "%", actualPosition - new Vector2(0, 20) + drawOrigin, Color.Yellow);
            }
        }

        public void Load(GraphicsDevice gd, ContentManager cm)
        {
            _sprite = cm.Load<Texture2D>(_spritePath);
            fontHealthFloat = cm.Load<SpriteFont>("fonts/healthfloat");
        }

        public void Sleep(ContentManager cm)
        {
            
        }

        public void Unload(GraphicsDevice gd, ContentManager cm)
        {
            
        }

        public void Update(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, Vector2 drawOrigin)
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
            Vector2 newPosition = Vector2.Lerp(actualPosition, Position, 0.1f);
            Vector2 newXMove = new Vector2(newPosition.X, actualPosition.Y);
            Vector2 newYMove = new Vector2(actualPosition.X, newPosition.Y);
            bool Xhit = false;
            bool Yhit = false;
            foreach (IGameObject i in WorldObjectHolder.objects)
            {
                Rectangle r = (i as ICollidable).getBoundingBox();
                if(r.Intersects(new Rectangle((int)newXMove.X, (int)newXMove.Y, 32, 48)))
                {
                    Xhit = true;
                }
                if (r.Intersects(new Rectangle((int)newYMove.X, (int)newYMove.Y, 32, 48)))
                {
                    Yhit = true;
                }
            }
            foreach (Character c in CharacterList.npcs)
            {
                bool npcHit = false;
                Rectangle r = (c as ICollidable).getBoundingBox();
                if (r.Intersects(new Rectangle((int)newXMove.X, (int)newXMove.Y, 32, 48)))
                {
                    Xhit = true;
                    npcHit = true;
                }
                if (r.Intersects(new Rectangle((int)newYMove.X, (int)newYMove.Y, 32, 48)))
                {
                    Yhit = true;
                    npcHit = true;
                }
                if(npcHit)
                {
                    if(walkSpeed > c.walkSpeed)
                    {
                        c.characterHealth -= walkSpeed - c.walkSpeed;
                    }
                    else if(walkSpeed < c.walkSpeed)
                    {
                        characterHealth -= c.walkSpeed - walkSpeed;
                    }
                    else
                    {
                        float hitVal = Math.Abs(walkSpeed - c.walkSpeed);
                        c.characterHealth -= hitVal;
                        characterHealth -= hitVal;
                    }
                }
            }
            if (Xhit)
            {
                newPosition.X = actualPosition.X;
            }
            if (Yhit)
            {
                newPosition.Y = actualPosition.Y;
            }
            if(Xhit || Yhit)
            {
                Position = newPosition;
            }
            walkSpeed = Vector2.Distance(actualPosition, newPosition);
            actualPosition = newPosition;
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

        public Rectangle getBoundingBox()
        {
            return new Rectangle((int)actualPosition.X, (int)actualPosition.Y, 32, 48);
        }
    }
}
