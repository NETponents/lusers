using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lusers_game
{
    public class MainMenuScreen : Screen
    {
        List<Button> controlList;
        ScreenManager _sm;

        public MainMenuScreen(int ScreenWidth, int ScreenHeight)
            : base(ScreenWidth, ScreenHeight)
        {
            controlList = new List<Button>();
        }

        public override void Load(GraphicsDevice gd, ContentManager cm)
        {
            controlList.Add(new Button("Play", new Vector2(50, 50), PlayAction));
            foreach (Button b in controlList)
            {
                b.Load(gd, cm);
            }
            base.Load(gd, cm);
        }

        public override void Update(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, ScreenManager sm)
        {
            if(_sm == null)
            {
                _sm = sm;
            }
            foreach (Button b in controlList)
            {
                b.Update(gd, ref sb, cm, ref gt, new Vector2(0, 0));
            }
            base.Update(gd, ref sb, cm, ref gt, sm);
        }

        public override void Draw(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt)
        {
            foreach (Button b in controlList)
            {
                b.Draw(gd, ref sb, cm, ref gt, new Vector2(0, 0));
            }
            base.Draw(gd, ref sb, cm, ref gt);
        }
        private void PlayAction()
        {
            _sm.switchScreenContext("room_office1");
        }
    }
}
