﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lusers_game
{
    public class Character : ICollidable
    {
        public float walkSpeed;
        public string Name
        {
            get;
            protected set;
        }
        public Vector2 Position;
        public Vector2 actualPosition;
        public float characterHealth;
        public float targetHealth;
        protected Texture2D _sprite;
        protected int _animFrames;
        protected bool _hasMoved;
        protected int _animSpacer = 0;
        protected const int _animDirections = 4;
        protected WalkingDirection animDirection
        {
            get
            {
                return _animDirection;
            }
            set
            {
                if (value != _animDirection)
                {
                    animStep = 0;
                    _animDirection = value;
                }
            }
        }
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
        private string _spritePath;
        private int _animStep;
        private WalkingDirection _animDirection;
        private SpriteFont fontHealthFloat;
        
        public Character(string spritePath, int animFrames, string characterName, Vector2 startPosition)
        {
            Name = characterName;
            _animFrames = animFrames;
            _spritePath = spritePath;
            Position = startPosition;
            animStep = 0;
            _animDirection = WalkingDirection.Right;
            characterHealth = 100.0f;
            targetHealth = 100.0f;
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
            targetHealth = 100.0f;
            walkSpeed = 0;
        }

        public virtual void Awake(ContentManager cm)
        {
            
        }

        public virtual void Draw(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, Vector2 drawOrigin)
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
                sb.DrawString(fontHealthFloat, (int)characterHealth + "%", actualPosition - new Vector2(0, 40) + drawOrigin, Color.Yellow);
            }
            sb.DrawString(fontHealthFloat, Name, actualPosition - new Vector2(0, 20) + drawOrigin, Color.Yellow);
        }

        public virtual void Load(GraphicsDevice gd, ContentManager cm)
        {
            _sprite = cm.Load<Texture2D>(_spritePath);
            fontHealthFloat = cm.Load<SpriteFont>("fonts/healthfloat");
        }

        public virtual void Sleep(ContentManager cm)
        {
            
        }

        public virtual void Unload(GraphicsDevice gd, ContentManager cm)
        {
            
        }

        public virtual void Update(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, Vector2 drawOrigin, RoomScreen currentRoom)
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
            foreach (IGameObject i in currentRoom.gameObjects)
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
            foreach (Character c in currentRoom.characters)
            {
                if(c.Equals(this))
                {
                    continue;
                }
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
                if (npcHit)
                {
                    if (walkSpeed > c.walkSpeed)
                    {
                        c.targetHealth -= walkSpeed - c.walkSpeed;
                    }
                    else if (walkSpeed < c.walkSpeed)
                    {
                        targetHealth -= c.walkSpeed - walkSpeed;
                    }
                    else
                    {
                        float hitVal = Math.Abs(walkSpeed - c.walkSpeed);
                        c.targetHealth -= hitVal;
                        targetHealth -= hitVal;
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
            targetHealth = Math.Max(0, targetHealth);
            characterHealth = MathHelper.Lerp(characterHealth, targetHealth, 0.1f);
            if(characterHealth == 0)
            {
                // Player is dead
            }
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
