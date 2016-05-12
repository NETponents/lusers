using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lusers_game
{
    public class RoomScreen : Screen
    {
        /// <summary>
        /// Pointer to active pop up message object.
        /// </summary>
        protected PopUp currentPopUp;
        /// <summary>
        /// Old keyboard state from last update cycle.
        /// Used for bounce protection.
        /// </summary>
        protected KeyboardState oldKSState;
        /// <summary>
        /// Current tool in use.
        /// </summary>
        protected MouseTool mt;
        /// <summary>
        /// Current HUD object to display. [unused]
        /// </summary>
        protected Hud gameHud;
        /// <summary>
        /// Relative origin of objects to draw from.
        /// Used for virtual viewport.
        /// </summary>
        protected Vector2 drawOrigin
        {
            get
            {
                return screenCenter + new Vector2(_screenWidth / 2, _screenHeight / 2);
            }
            set
            {
                screenCenter = value - new Vector2(_screenWidth / 2, _screenHeight / 2);
            }
        }
        /// <summary>
        /// Location of center screen relative to the world origin.
        /// </summary>
        protected Vector2 screenCenter;
        /// <summary>
        /// Font for pop up messages.
        /// </summary>
        protected SpriteFont fontPopUpContent;
        /// <summary>
        /// Font for sub-text on pop up messages.
        /// </summary>
        protected SpriteFont fontPopUpSubText;
        /// <summary>
        /// Background texture object for pop up windows.
        /// </summary>
        protected Texture2D texPopUpWindow;
        /// <summary>
        /// Pointer to room object that player is currently playing in.
        /// </summary>
        protected Room currentRoom;

        public RoomScreen(int ScreenWidth, int ScreenHeight)
            : base(ScreenWidth, ScreenHeight)
        {

        }

        public override void Awake(ContentManager cm)
        {
            
        }

        public override void Draw(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt)
        {
            // Draw the current room and all subsequent children components.
            currentRoom.Draw(gd, ref sb, cm, ref gt, drawOrigin);
            // Draw NPCs.
            foreach (NonPlayerCharacter i in CharacterList.npcs)
            {
                i.Draw(gd, ref sb, cm, ref gt, drawOrigin);
            }
            // Draw world objects like furniture.
            WorldObjectHolder.Draw(gd, ref sb, cm, ref gt, drawOrigin);
            // Draw the MouseTool object.
            mt.Draw(gd, ref sb, cm, ref gt, drawOrigin);
            // Draw the HUD.
            gameHud.Draw(gd, ref sb, cm, ref gt, drawOrigin);
            // Draw the pop up box if there is a message that needs to be displayed.
            if (currentPopUp != null)
            {
                sb.Draw(texPopUpWindow, new Rectangle(10, 10, 1200, 80), Color.White);
                sb.DrawString(fontPopUpContent, currentPopUp.getMessage(), new Vector2(20, 20), Color.Black);
                sb.DrawString(fontPopUpSubText, "Press Enter...", new Vector2(20, 55), Color.DimGray);
            }
            base.Draw(gd, ref sb, cm, ref gt);
        }

        public override void Load(GraphicsDevice gd, ContentManager cm)
        {
            // Set viewport to slightly past the origin.
            drawOrigin = new Vector2(-100, -100);
            // Grab the current state of the keyboard so the Update() function can run.
            oldKSState = Keyboard.GetState();
            // Load fonts
            fontPopUpContent = cm.Load<SpriteFont>("fonts/popupcontent");
            fontPopUpSubText = cm.Load<SpriteFont>("fonts/popupsubtext");
            // Load textures.
            texPopUpWindow = cm.Load<Texture2D>("img/hud/redbox");
            // Load the room object.
            currentRoom.Load(gd, cm);
            // Load all NPC characters.
            foreach (Character i in CharacterList.npcs)
            {
                i.Load(gd, cm);
            }
            // Create new HUD object.
            gameHud = new Hud();
            gameHud.Load(gd, cm);
            // Create new mouse tool.
            mt = new MouseTool();
            mt.Load(gd, cm);
            base.Load(gd, cm);
        }

        public override void Sleep(ContentManager cm)
        {
            
        }

        public override void Unload(GraphicsDevice gd, ContentManager cm)
        {
            
        }

        public override void Update(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt)
        {
            // Update room object and all subsequent children components.
            currentRoom.Update(gd, ref sb, cm, ref gt, drawOrigin);
            // Update the HUD.
            gameHud.Update(gd, ref sb, cm, ref gt, drawOrigin);
            // Aim the camera at the main character.
            followCameraBehavior(currentRoom.getPlayerCoordinates());
            // Check to see if we need to cycle pop up messages.
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && oldKSState.IsKeyUp(Keys.Enter))
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
            mt.Update(gd, ref sb, cm, ref gt, drawOrigin);
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
                i.Update(gd, ref sb, cm, ref gt, drawOrigin);
            }
            base.Update(gd, ref sb, cm, ref gt);
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
