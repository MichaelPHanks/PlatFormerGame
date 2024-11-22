using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformerGameClient.Enums;
using PlatformerGameClient.InputHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerGameClient.Views
{
    public class SettingsView : GameStateView
    {

        public Keys up = Keys.W;
        public Keys left = Keys.A;
        public Keys right = Keys.D;
        public Keys down = Keys.S;
        public GameStateEnum prevState = GameStateEnum.MainMenu;
        private Texture2D backgroundImage;
        private SpriteFont m_fontMenu;
        private SpriteFont m_fontMenuSelect;
        private bool isKeySelected = false;
        private bool m_waitForKeyRelease = false;
        private Rectangle Up = new Rectangle();
        private Rectangle Left = new Rectangle();
        private Rectangle Right = new Rectangle();
        private Rectangle Down = new Rectangle();

        private bool saving = false;
        private bool loading = false;
        private Texture2D whiteBackground;
        private bool isEnterUp = false;
        KeyboardInput keyboard;


        private enum KeySelection
        {
            Up,
            Left,
            Right,
            Down,
            None,
        }
        private KeySelection m_currentSelection = KeySelection.Up;
        public override void loadContent(ContentManager contentManager)
        {
            m_fontMenu = contentManager.Load<SpriteFont>("Fonts/menu");
            m_fontMenuSelect = contentManager.Load<SpriteFont>("Fonts/menu-selected");

            keyboard = new KeyboardInput();

            keyboard.registerCommand(Keys.Up, true, new IInputDevice.CommandDelegate(UpHit));
            keyboard.registerCommand(Keys.Down, true, new IInputDevice.CommandDelegate(DownHit));

        }

        public void UpHit(GameTime gameTime)
        {
            Console.WriteLine("Did something");
            m_currentSelection -= 1;

        }
        public void DownHit(GameTime gameTime)
        {
            Console.WriteLine("Did something cooler");
            m_currentSelection += 1;

        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            keyboard.Update(gameTime);
            return GameStateEnum.Settings;
        }

        public override void render(GameTime gameTime)
        {
            m_spriteBatch.Begin();
/*            m_spriteBatch.Draw(backgroundImage, new Rectangle(0, 0, m_graphics.PreferredBackBufferWidth, m_graphics.PreferredBackBufferHeight), Color.Gray);
*/
            float scale1 = m_graphics.PreferredBackBufferWidth / 1920f;
/*
            Vector2 stringSize2 = m_fontMenu.MeasureString("Press ESC To Return") * scale1;

            *//*m_spriteBatch.Draw(whiteBackground, new Rectangle((int)(m_graphics.PreferredBackBufferWidth / 5 - stringSize2.X / 2),
            (int)(m_graphics.PreferredBackBufferHeight / 10f - stringSize2.Y), (int)stringSize2.X, (int)stringSize2.Y), Color.White);*//*

            m_spriteBatch.DrawString(
                           m_fontMenu,
                           "Press ESC To Return",
                           new Vector2(m_graphics.PreferredBackBufferWidth / 5 - stringSize2.X / 2,
            m_graphics.PreferredBackBufferHeight / 10f - stringSize2.Y),
                           Color.Black,
                           0,
                           Vector2.Zero,
                           scale1,
                           SpriteEffects.None,
                           0);*/
            // Display the current Keys and their buttons...
            float bottom = drawMenuItem(m_fontMenu, "Settings", m_graphics.PreferredBackBufferHeight / 1080f * 100f, Color.Black);
            bottom = drawMenuItem(m_currentSelection == KeySelection.Up ? m_fontMenuSelect : m_fontMenu, "Up: " + up.ToString(), bottom, m_currentSelection == KeySelection.Up && isKeySelected ? Color.Blue : Color.White);

            bottom = drawMenuItem(m_currentSelection == KeySelection.Left ? m_fontMenuSelect : m_fontMenu, "Left: " + left.ToString(), bottom, m_currentSelection == KeySelection.Left && isKeySelected ? Color.Blue : Color.White);
            bottom = drawMenuItem(m_currentSelection == KeySelection.Right ? m_fontMenuSelect : m_fontMenu, "Right: " + right.ToString(), bottom, m_currentSelection == KeySelection.Right && isKeySelected ? Color.Blue : Color.White);
            bottom = drawMenuItem(m_currentSelection == KeySelection.Down ? m_fontMenuSelect : m_fontMenu, "Down: " + down.ToString(), bottom, m_currentSelection == KeySelection.Down && isKeySelected ? Color.Blue : Color.White);

       /*     bottom = drawMenuItem(m_fontMenu, "Press Enter To Select a Key Binding To Change", bottom + stringSize2.Y, Color.LightGray);
            bottom = drawMenuItem(m_fontMenu, "Once Blue, Select The Preferred Key For That Control", bottom + stringSize2.Y, Color.LightGray);
            bottom = drawMenuItem(m_fontMenu, "Press Escape If You Change Your Mind (If Blue)", bottom + stringSize2.Y, Color.LightGray);*/


            m_spriteBatch.End();
        }

        private float drawMenuItem(SpriteFont font, string text, float y, Color color)
        {

            float scale = m_graphics.PreferredBackBufferWidth / 1980f;
            Vector2 stringSize = font.MeasureString(text) * scale;
            m_spriteBatch.DrawString(
                           font,
                           text,
                           new Vector2(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, y),
                           color,
                           0,
                           Vector2.Zero,
                           scale,
                           SpriteEffects.None,
                           0);

            if (text == "Up: " + up.ToString())
            {
                Up = new Rectangle(((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2) - 10, (int)y, (int)stringSize.X + 20, (int)stringSize.Y);

            }
            if (text == "Left: " + left.ToString())
            {
                Left = new Rectangle(((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2) - 10, (int)y, (int)stringSize.X + 20, (int)stringSize.Y);

            }
            if (text == "Right: " + right.ToString())
            {
                Right = new Rectangle(((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2) - 10, (int)y, (int)stringSize.X + 20, (int)stringSize.Y);

            }

            if (text == "Down: " + down.ToString())
            {
                Down = new Rectangle(((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2) - 10, (int)y, (int)stringSize.X + 20, (int)stringSize.Y);

            }

            return y + stringSize.Y;
        }

        public override void update(GameTime gameTime)
        {
        }
    }
}
