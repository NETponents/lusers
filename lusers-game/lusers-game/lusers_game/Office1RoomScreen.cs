using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lusers_game
{
    public class Office1RoomScreen : RoomScreen
    {
        public Office1RoomScreen(int ScreenWidth, int ScreenHeight)
            : base(ScreenWidth, ScreenHeight)
        {
            // Initialize the room object.
            currentRoom = new Room();
        }

        public override void Awake(ContentManager cm)
        {
            
        }

        public override void Draw(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt)
        {
            base.Draw(gd, ref sb, cm, ref gt);
        }

        public override void Load(GraphicsDevice gd, ContentManager cm)
        {
            // Enqueue initial dialog.
            MessageService.popUpEnqueue("CEO: You must be the new hire.");
            MessageService.popUpEnqueue("CEO: After the incident at the old office, we had to relocate.");
            MessageService.popUpEnqueue("CEO: From now on, no nerf wars at work!");
            MessageService.popUpEnqueue("CEO: Anyways, you have been hired on as the director of IT.");
            MessageService.popUpEnqueue("CEO: I'm also going to need you to help us build our office.");
            MessageService.popUpEnqueue("CEO: Now get me a desk so I can get back to work!");
            // Initialize start-game NPCs.
            CharacterList.npcs.Add(new Characters.CEO(new Vector2(400, 200)));
            // Enqueue first task.
            TaskList.tasks.Add(new Tasks.T1());
            base.Load(gd, cm);
        }

        public override void Sleep(ContentManager cm)
        {

        }

        public override void Unload(GraphicsDevice gd, ContentManager cm)
        {

        }

        public override void Update(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, ScreenManager sm)
        {
            base.Update(gd, ref sb, cm, ref gt, sm);
        }
    }
}
