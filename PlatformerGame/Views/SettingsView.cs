using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformerGameClient.Enums;
using PlatformerGameClient.InputHandling;
using PlatformerGameClient.Views.MenuComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

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

        private MenuItemList m_resolutionItems;
        private MenuItem hd;
        private MenuItem poorQuality;
        private MenuItem okayQuality;
        private Dictionary<MenuState, string> m_gameStates;
        private MenuItem backButton;
        private bool backButtonClicked = false;

        private bool canUseMouse = false;

        private List<MenuObject> m_menuObjects;

        private string currentSelection = "1920X1080";

        private enum KeySelection
        {
            Up,
            Left,
            Right,
            Down,
            None,
        }

        private enum MenuState
        {   Back,
            HighDefinition,
            PoorDefinition
        }
        private MenuState m_currentSelection = MenuState.HighDefinition;
        public override void loadContent(ContentManager contentManager)
        {
            m_fontMenu = contentManager.Load<SpriteFont>("Fonts/menu");
            m_fontMenuSelect = contentManager.Load<SpriteFont>("Fonts/menu-selected");

            keyboard = new KeyboardInput();

            keyboard.registerCommand(Keys.Up, true, new IInputDevice.CommandDelegate(UpHit));
            keyboard.registerCommand(Keys.Down, true, new IInputDevice.CommandDelegate(DownHit));
            m_gameStates = new Dictionary<MenuState, string>
            {
                {MenuState.HighDefinition, "1920X1080" },
                {MenuState.PoorDefinition, "1280X720" },
                { MenuState.Back, "<--"}
            };
            /*float scale = m_graphics.PreferredBackBufferWidth / 1920f;
            Vector2 stringSize = m_fontMenuSelect.MeasureString("1920X1080") * scale;
            float bottom = m_graphics.PreferredBackBufferWidth / 4;
            hd = new MenuItem("1920X1080", new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)bottom, (int)stringSize.X, (int)stringSize.Y), m_graphics, m_fontMenuSelect, m_spriteBatch, new MenuItem.OnClick(highDefinition), true, m_fontMenuSelect);
            bottom += stringSize.Y;
            stringSize = m_fontMenu.MeasureString("1280X720") * scale;
            poorQuality = new MenuItem("1280X720", new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)bottom, (int)stringSize.X, (int)stringSize.Y), m_graphics, m_fontMenuSelect, m_spriteBatch, new MenuItem.OnClick(highDefinition), false, m_fontMenuSelect);
 
            List <MenuItem> list = new List<MenuItem>();
            list.Add(hd);
            list.Add((poorQuality));*/
            float scale = m_graphics.PreferredBackBufferWidth / 1920f;

            Vector2 stringSize = m_fontMenu.MeasureString("<--") * scale;
            backButton = new MenuItem("<--", new Rectangle(100, 100, (int)stringSize.X, (int)stringSize.Y), m_graphics, m_fontMenu, m_spriteBatch, new MenuItem.OnClick(backClicked), false, m_fontMenuSelect);
            m_resolutionItems = new MenuItemList(new List<string> { "1920X1080", "1280X720" }, m_fontMenu, m_fontMenuSelect, "1920X1080", this.m_graphics, this.m_spriteBatch, new Vector2(0, m_graphics.PreferredBackBufferWidth / 4));
            m_menuObjects = new List<MenuObject> {backButton, m_resolutionItems };
            m_resolutionItems.registerOnClick("1920X1080", new MenuItem.OnClick(highDefinition));
            m_resolutionItems.registerOnClick("1280X720", new MenuItem.OnClick(poorDefinition));
            /* m_menulist = new MenuItemList();*/
        }

        public void highDefinition(GameTime gameTime)
        {
            m_graphics.PreferredBackBufferWidth = 1920;
            m_graphics.PreferredBackBufferHeight = 1080;

            m_graphics.IsFullScreen = false;
            m_graphics.ApplyChanges();
        }
        public void poorDefinition(GameTime gameTime)
        {
            m_graphics.PreferredBackBufferWidth = 1280;
            m_graphics.PreferredBackBufferHeight = 720;

            m_graphics.IsFullScreen = false;
            m_graphics.ApplyChanges();
        }
        public void backClicked(GameTime gameTime)
        {
            backButtonClicked = true;
            Console.WriteLine("Something amazing happened...");
        }
        public void UpHit(GameTime gameTime)
        {
            Console.WriteLine("Did something");
            if (m_currentSelection > 0)
            {
                this.m_currentSelection -= 1;
            }
            else
            {
                this.m_currentSelection = MenuState.HighDefinition;
            }

            foreach (MenuObject item in m_menuObjects)
            {
                item.selectionChanged(this.m_gameStates[this.m_currentSelection]);
                currentSelection = this.m_gameStates[this.m_currentSelection];

            }

        }
        public void DownHit(GameTime gameTime)
        {
            Console.WriteLine("Did something cooler");
            if ((int)m_currentSelection < 1)
            {
                this.m_currentSelection += 1;
            }
            else
            {
                this.m_currentSelection = MenuState.PoorDefinition;
            }

            foreach (MenuObject item in m_menuObjects)
            {
                item.selectionChanged(this.m_gameStates[this.m_currentSelection]);
                currentSelection = this.m_gameStates[this.m_currentSelection];
            }


        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            
            keyboard.Update(gameTime);
            m_resolutionItems.processInput(gameTime);





            if (isEnterUp && canUseMouse)
            {


                string itemSelected = currentSelection;
                foreach (MenuObject item in m_menuObjects)
                {

                    string idk = item.isHoveredOver();
                    if (item.isHoveredOver() != "")
                    {
                        itemSelected = item.isHoveredOver();
                        break;
                    }

                }

                if (itemSelected != currentSelection)
                {
                    if (itemSelected == "<--")
                    {
                        Console.WriteLine("IDK");
                    }
                    foreach (MenuObject item in m_menuObjects)
                    {
                        item.selectionChanged(itemSelected);
                    }
                    currentSelection = itemSelected;
                    foreach (KeyValuePair<MenuState, string> tempitem in m_gameStates)
                    {
                        if (tempitem.Value == currentSelection)
                        {
                            m_currentSelection = tempitem.Key;
                        }
                    }
                }

                foreach (MenuObject menuObject in m_menuObjects)
                {
                    menuObject.processInput(gameTime);
                }



            }
            if (Keyboard.GetState().IsKeyUp(Keys.Enter))
            {
                isEnterUp = true;
            }


            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                canUseMouse = false;
            }
            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                canUseMouse = true;
            }

            if (backButtonClicked)
            {
                backButtonClicked = false;
                return GameStateEnum.MainMenu;
            }

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
            /*  float bottom = drawMenuItem(m_fontMenu, "Settings", m_graphics.PreferredBackBufferHeight / 1080f * 100f, Color.Black);
              bottom = drawMenuItem(m_currentSelection == KeySelection.Up ? m_fontMenuSelect : m_fontMenu, "Up: " + up.ToString(), bottom, m_currentSelection == KeySelection.Up && isKeySelected ? Color.Blue : Color.White);

              bottom = drawMenuItem(m_currentSelection == KeySelection.Left ? m_fontMenuSelect : m_fontMenu, "Left: " + left.ToString(), bottom, m_currentSelection == KeySelection.Left && isKeySelected ? Color.Blue : Color.White);
              bottom = drawMenuItem(m_currentSelection == KeySelection.Right ? m_fontMenuSelect : m_fontMenu, "Right: " + right.ToString(), bottom, m_currentSelection == KeySelection.Right && isKeySelected ? Color.Blue : Color.White);
              bottom = drawMenuItem(m_currentSelection == KeySelection.Down ? m_fontMenuSelect : m_fontMenu, "Down: " + down.ToString(), bottom, m_currentSelection == KeySelection.Down && isKeySelected ? Color.Blue : Color.White);
  */
            /*     bottom = drawMenuItem(m_fontMenu, "Press Enter To Select a Key Binding To Change", bottom + stringSize2.Y, Color.LightGray);
                 bottom = drawMenuItem(m_fontMenu, "Once Blue, Select The Preferred Key For That Control", bottom + stringSize2.Y, Color.LightGray);
                 bottom = drawMenuItem(m_fontMenu, "Press Escape If You Change Your Mind (If Blue)", bottom + stringSize2.Y, Color.LightGray);*/
            foreach (MenuObject menuObject in m_menuObjects)
            {
                menuObject.draw();
            }
            /*m_resolutionItems.draw();*/
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
