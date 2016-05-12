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
        /// <summary>
        /// Font for FPS counter.
        /// </summary>
        SpriteFont fontFPSCounter;
        /// <summary>
        /// Font for room name. [unused]
        /// </summary>
        SpriteFont fontRoomName;
        /// <summary>
        /// Font for pop up messages.
        /// </summary>
        SpriteFont fontPopUpContent;
        /// <summary>
        /// Font for sub-text on pop up messages.
        /// </summary>
        SpriteFont fontPopUpSubText;
        /// <summary>
        /// Background texture object for pop up windows.
        /// </summary>
        Texture2D texPopUpWindow;
        /// <summary>
        /// Pointer to room object that player is currently playing in.
        /// </summary>
        Room currentRoom;
        /// <summary>
        /// Pointer to active pop up message object.
        /// </summary>
        PopUp currentPopUp;
        /// <summary>
        /// Old keyboard state from last update cycle.
        /// Used for bounce protection.
        /// </summary>
        KeyboardState oldKSState;
        /// <summary>
        /// Current tool in use.
        /// </summary>
        MouseTool mt;
        /// <summary>
        /// Current HUD object to display. [unused]
        /// </summary>
        Hud gameHud;
        /// <summary>
        /// Relative origin of objects to draw from.
        /// Used for virtual viewport.
        /// </summary>
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
        /// <summary>
        /// Location of center screen relative to the world origin.
        /// </summary>
        Vector2 screenCenter;

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
            graphics.IsFullScreen = true;
            //graphics.PreferredBackBufferHeight = 800;
            //graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 1050;
            graphics.PreferredBackBufferWidth = 1680;
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
            // Initialize the room object.
            currentRoom = new Room();
            // Set viewport to slightly past the origin.
            drawOrigin = new Vector2(-100, -100);
            // Grab the current state of the keyboard so the Update() function can run.
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
            // Load fonts
            fontFPSCounter = Content.Load<SpriteFont>("fonts/fpscounter");
            fontRoomName = Content.Load<SpriteFont>("fonts/roomname");
            fontPopUpContent = Content.Load<SpriteFont>("fonts/popupcontent");
            fontPopUpSubText = Content.Load<SpriteFont>("fonts/popupsubtext");
            // Load the room object.
            currentRoom.Load(GraphicsDevice, Content);
            // Load message box textures.
            texPopUpWindow = Content.Load<Texture2D>("img/hud/redbox");
            currentPopUp = null;
            // Enqueue initial dialog.
            MessageService.popUpEnqueue("CEO: You must be the new hire.");
            MessageService.popUpEnqueue("CEO: After the incident at the old office, we had to relocate.");
            MessageService.popUpEnqueue("CEO: From now on, no nerf wars at work!");
            MessageService.popUpEnqueue("CEO: Anyways, you have been hired on as the director of IT.");
            MessageService.popUpEnqueue("CEO: I'm also going to need you to help us build our office.");
            MessageService.popUpEnqueue("CEO: Now get me a desk so I can get back to work!");
            // Initialize start-game NPCs.
            CharacterList.npcs.Add(new Characters.CEO(new Vector2(400, 200)));
            // Load all NPC characters.
            foreach(Character i in CharacterList.npcs)
            {
                i.Load(GraphicsDevice, Content);
            }
            // Enqueue first task.
            TaskList.tasks.Add(new Tasks.T1());
            // Create new HUD object.
            gameHud = new Hud();
            gameHud.Load(GraphicsDevice, Content);
            // Create new mouse tool.
            mt = new MouseTool();
            mt.Load(GraphicsDevice, Content);
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
            // Update room object and all subsequent children components.
            currentRoom.Update(GraphicsDevice, ref spriteBatch, Content, ref gameTime, drawOrigin);
            // Update the HUD.
            gameHud.Update(GraphicsDevice, ref spriteBatch, Content, ref gameTime, drawOrigin);
            // Aim the camera at the main character.
            followCameraBehavior(currentRoom.getPlayerCoordinates());
            // Check to see if we need to cycle pop up messages.
            if(Keyboard.GetState().IsKeyDown(Keys.Enter) && oldKSState.IsKeyUp(Keys.Enter))
            {
                // Check to make sure the queue isn't empty first.
                if (MessageService.popUpQueue.Count > 0)
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
                // If we currently aren't displaying anything.
                if (currentPopUp == null)
                {
                    if (MessageService.popUpQueue.Count > 0)
                    {
                        currentPopUp = MessageService.popUpQueue.Dequeue();
                    }
                }
            }
            // Check to see if we need to switch MouseToolState modes.
            if (Keyboard.GetState().IsKeyDown(Keys.C) && oldKSState.IsKeyUp(Keys.C))
            {
                // Toggle state.
                if (mt.toolState == MouseToolState.Selector)
                {
                    mt.toolState = MouseToolState.Builder;
                }
                else
                {
                    mt.toolState = MouseToolState.Selector;
                }
            }
            // Update the MouseTool in the (possibly new) MouseToolSate.
            mt.Update(GraphicsDevice, ref spriteBatch, Content, ref gameTime, drawOrigin);
            // Check current tasks for completion.
            foreach (Task t in TaskList.tasks)
            {
                // Only check if not already complete.
                if (!t.isComplete)
                {
                    t.checkForCompletion();
                }
            }
            // Swap old keyboard state with new one.
            oldKSState = Keyboard.GetState();
            // Update all NPCs.
            foreach (NonPlayerCharacter i in CharacterList.npcs)
            {
                i.Update(GraphicsDevice, ref spriteBatch, Content, ref gameTime, drawOrigin);
            }
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
            // Draw the current room and all subsequent children components.
            currentRoom.Draw(GraphicsDevice, ref spriteBatch, Content, ref gameTime, drawOrigin);
            // Draw NPCs.
            foreach (NonPlayerCharacter i in CharacterList.npcs)
            {
                i.Draw(GraphicsDevice, ref spriteBatch, Content, ref gameTime, drawOrigin);
            }
            // Draw world objects like furniture.
            WorldObjectHolder.Draw(GraphicsDevice, ref spriteBatch, Content, ref gameTime, drawOrigin);
            // Draw the MouseTool object.
            mt.Draw(GraphicsDevice, ref spriteBatch, Content, ref gameTime, drawOrigin);
            // Draw the HUD.
            gameHud.Draw(GraphicsDevice, ref spriteBatch, Content, ref gameTime, drawOrigin);
            // Draw the pop up box if there is a message that needs to be displayed.
            if(currentPopUp != null)
            {
                spriteBatch.Draw(texPopUpWindow, new Rectangle(10, 10, 1200, 80), Color.White);
                spriteBatch.DrawString(fontPopUpContent, currentPopUp.getMessage(), new Vector2(20, 20), Color.Black);
                spriteBatch.DrawString(fontPopUpSubText, "Press Enter...", new Vector2(20, 55), Color.DimGray);
            }
            // Calculate framerate.
            float frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
            spriteBatch.DrawString(fontFPSCounter, frameRate + " FPS", new Vector2(2, 2), Color.Yellow);
            // Close 2D drawing interface.
            spriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Performs linear interpolation for camera motion smoothing to focus on a specified point.
        /// </summary>
        /// <param name="point">Point to focus on relative to world origin.</param>
        private void followCameraBehavior(Vector2 point)
        {
            screenCenter.X = MathHelper.Lerp(screenCenter.X, point.X, 0.075f);
            screenCenter.Y = MathHelper.Lerp(screenCenter.Y, point.Y, 0.075f);
            
        }
    }
}
