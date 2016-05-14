using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lusers_game
{
    public class MouseTool /*: IGameObject*/
    {
        private Texture2D texBlocked;
        private Texture2D texClear;
        public MouseToolState toolState;
        private MouseState oldMouseState;
        public MouseToolFunction toolFunction;

        public MouseTool()
        {

        }

        public void Awake(ContentManager cm)
        {

        }

        public void Draw(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, Vector2 drawOrigin)
        {
            if (toolState == MouseToolState.Builder)
            {
                MouseState ms = Mouse.GetState();
                Vector2 rectRoot = new Vector2(ms.X, ms.Y);
                // For debugging purposes
                //sb.Draw(texBlocked, new Rectangle((int)(rectRoot.X + drawOrigin.X - 5), (int)(rectRoot.Y + drawOrigin.Y - 5), 10, 10), Color.White);
                // I don't know why this works but it does.
                rectRoot += drawOrigin * new Vector2(-1, -1);
                rectRoot.X = (int)(rectRoot.X / 100.0f) * 100;
                rectRoot.Y = (int)(rectRoot.Y / 100.0f) * 100;
                rectRoot += drawOrigin;
                sb.Draw(texClear, new Rectangle((int)rectRoot.X, (int)rectRoot.Y, 100, 100), Color.White);
            }
        }

        public void Load(GraphicsDevice gd, ContentManager cm)
        {
            texBlocked = cm.Load<Texture2D>("img/tools/placerblocked");
            texClear = cm.Load<Texture2D>("img/tools/placerclear");
            toolState = MouseToolState.Selector;
            oldMouseState = new MouseState(0, 0, 0, ButtonState.Pressed, ButtonState.Pressed, ButtonState.Pressed, ButtonState.Pressed, ButtonState.Pressed);
        }

        public void Sleep(ContentManager cm)
        {

        }

        public void Unload(GraphicsDevice gd, ContentManager cm)
        {

        }

        public void Update(GraphicsDevice gd, ref SpriteBatch sb, ContentManager cm, ref GameTime gt, Vector2 drawOrigin, RoomScreen rs)
        {
            MouseState ms = Mouse.GetState();
            if (toolState == MouseToolState.Builder)
            {
                KeyboardState ks = Keyboard.GetState();
                if (ks.IsKeyDown(Keys.NumPad1))
                {
                    toolFunction = MouseToolFunction.Wall;
                }
                else if (ks.IsKeyDown(Keys.NumPad2))
                {
                    toolFunction = MouseToolFunction.Desk;
                }
                if (ms.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
                {
                    Vector2 rectRoot = new Vector2(ms.X, ms.Y);
                    bool clear = true;
                    foreach (Furnature f in rs.gameObjects)
                    {
                        if (Geometry.Vector2DIntersectsRectangle(f.getBoundingBox(), new Vector2(ms.X, ms.Y) + drawOrigin))
                        {
                            clear = false;
                            break;
                        }
                    }
                    if (clear)
                    {
                        rectRoot += drawOrigin * new Vector2(-1, -1);
                        rectRoot.X = (int)(rectRoot.X / 100.0f) * 100;
                        rectRoot.Y = (int)(rectRoot.Y / 100.0f) * 100;
                        //rectRoot += drawOrigin;
                        Furnature d;
                        if (toolFunction == MouseToolFunction.Desk)
                        {
                            d = new Desk(rectRoot);
                        }
                        else if (toolFunction == MouseToolFunction.Wall)
                        {
                            d = new Wall(rectRoot);
                        }
                        else
                        {
                            d = null;
                        }
                        if (d != null)
                        {
                            d.Load(gd, cm);
                            rs.gameObjects.Add(d);
                        }
                    }
                }
                else if (ms.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released)
                {
                    Vector2 rectRoot = new Vector2(ms.X, ms.Y);
                    rectRoot += drawOrigin * new Vector2(-1, -1);
                    //rectRoot.X = (int)(rectRoot.X / 100.0f) * 100;
                    //rectRoot.Y = (int)(rectRoot.Y / 100.0f) * 100;
                    List<IGameObject> itemsToRemove = new List<IGameObject>();
                    foreach (IGameObject g in rs.gameObjects)
                    {
                        if (Geometry.Vector2DIntersectsRectangle((g as Furnature).getBoundingBox(), rectRoot))
                        {
                            itemsToRemove.Add(g);
                        }
                    }
                    foreach (IGameObject i in itemsToRemove)
                    {
                        rs.gameObjects.Remove(i);
                    }
                }
            }
            oldMouseState = ms;
        }
    }
    public enum MouseToolFunction
    {
        Wall,
        Desk
    }
}
