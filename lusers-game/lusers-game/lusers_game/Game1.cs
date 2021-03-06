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
        /// <summary>
        /// Graphics device handle.
        /// </summary>
        GraphicsDeviceManager graphics;
        /// <summary>
        /// Handles 2D drawing to graphics device.
        /// </summary>
        SpriteBatch spriteBatch;

        ///////////////////////////////////////////////////
        ScreenManager sm;
        ///////////////////////////////////////////////////

        /// <summary>
        /// Initializes a new Game1 object.
        /// </summary>
        public Game1()
        {
            // Initialize graphics device.
            graphics = new GraphicsDeviceManager(this);
            // Point to content directory.
            Content.RootDirectory = "Content";
            // Initialize display parameters.
            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1280;
            //graphics.PreferredBackBufferHeight = 1050;
            //graphics.PreferredBackBufferWidth = 1680;
            // Apply new graphics object settings.
            graphics.ApplyChanges();
            // Make the mouse visible.
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
            ///////////////////////////////////////////////////
            sm = new ScreenManager("menu_main", new MainMenuScreen(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            sm.addScreen("room_office1", new Office1RoomScreen(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            ///////////////////////////////////////////////////

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

            ///////////////////////////////////////////////////
            sm.Load(GraphicsDevice, Content);
            ///////////////////////////////////////////////////
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
            // Check for exit conditions.
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            sm.Update(GraphicsDevice, ref spriteBatch, Content, ref gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Clear screen.
            GraphicsDevice.Clear(Color.Black);
            // Prepare 2D drawing interface.
            spriteBatch.Begin();
            ///////////////////////////////////////////////////
            sm.Draw(GraphicsDevice, ref spriteBatch, Content, ref gameTime);
            ///////////////////////////////////////////////////
            // Close 2D drawing interface.
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
