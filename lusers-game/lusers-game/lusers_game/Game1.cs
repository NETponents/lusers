using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace lusers_game
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont fontFPSCounter;
        SpriteFont fontRoomName;
        SpriteFont fontPopUpContent;
        SpriteFont fontPopUpSubText;

        Texture2D texPopUpWindow;

        Room currentRoom;

        PopUp currentPopUp;
        KeyboardState oldKSState;

        Hud gameHud;

        Vector2 drawOrigin
        {
            get
            {
                return screenCenter + new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            }
            set
            {
                screenCenter = value - new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            }
        }

        Vector2 screenCenter;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferHeight = 1050;
            graphics.PreferredBackBufferWidth = 1680;
            graphics.ApplyChanges();
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            currentRoom = new Room();
            drawOrigin = new Vector2(-100, -100);
            oldKSState = Keyboard.GetState();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            fontFPSCounter = Content.Load<SpriteFont>("fonts/fpscounter");
            fontRoomName = Content.Load<SpriteFont>("fonts/roomname");
            // TODO: use this.Content to load your game content here
            currentRoom.Load(GraphicsDevice, Content);
            fontPopUpContent = Content.Load<SpriteFont>("fonts/popupcontent");
            fontPopUpSubText = Content.Load<SpriteFont>("fonts/popupsubtext");
            texPopUpWindow = Content.Load<Texture2D>("img/hud/redbox");
            currentPopUp = null;
            MessageService.popUpEnqueue("CEO: You must be the new hire.");
            MessageService.popUpEnqueue("CEO: After the incident at the old office, we had to relocate.");
            MessageService.popUpEnqueue("CEO: From now on, no nerf wars at work!");
            MessageService.popUpEnqueue("CEO: Anyways, you have been hired on as the director of IT.");
            MessageService.popUpEnqueue("CEO: I'm also going to need you to help us bulid our office back up.");
            MessageService.popUpEnqueue("CEO: Now get me a desk so I can get back to work!");
            //MessageService.popUpQueue.Enqueue(new PopUp("1234567890123456789012345678901234"));
            gameHud = new Hud();
            gameHud.Load(GraphicsDevice, Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            currentRoom.Update(GraphicsDevice, ref spriteBatch, Content, ref gameTime);
            gameHud.Update(GraphicsDevice, ref spriteBatch, Content, ref gameTime);
            followCameraBehavior(currentRoom.getPlayerCoordinates());
            if(Keyboard.GetState().IsKeyDown(Keys.Enter) && oldKSState.IsKeyUp(Keys.Enter))
            {
                if(MessageService.popUpQueue.Count > 0)
                {
                    currentPopUp = MessageService.popUpQueue.Dequeue();
                }
                else
                {
                    currentPopUp = null;
                }
            }
            else
            {
                if (currentPopUp == null)
                {
                    if (MessageService.popUpQueue.Count > 0)
                    {
                        currentPopUp = MessageService.popUpQueue.Dequeue();
                    }
                }
            }
            oldKSState = Keyboard.GetState();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            currentRoom.Draw(GraphicsDevice, ref spriteBatch, Content, ref gameTime, drawOrigin);
            gameHud.Draw(GraphicsDevice, ref spriteBatch, Content, ref gameTime, drawOrigin);
            if(currentPopUp != null)
            {
                spriteBatch.Draw(texPopUpWindow, new Rectangle(10, 10, 600, 80), Color.White);
                spriteBatch.DrawString(fontPopUpContent, currentPopUp.getMessage(), new Vector2(20, 20), Color.Black);
                spriteBatch.DrawString(fontPopUpSubText, "Press Enter...", new Vector2(20, 55), Color.DimGray);
            }

            float frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
            spriteBatch.DrawString(fontFPSCounter, frameRate + " FPS", new Vector2(2, 2), Color.Yellow);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        private void followCameraBehavior(Vector2 point)
        {
            screenCenter.X = MathHelper.Lerp(screenCenter.X, point.X, 0.075f);
            screenCenter.Y = MathHelper.Lerp(screenCenter.Y, point.Y, 0.075f);
            
        }
    }
}
