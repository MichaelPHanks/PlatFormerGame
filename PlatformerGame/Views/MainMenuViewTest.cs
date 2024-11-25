using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using PlatformerGameClient.Enums;
using PlatformerGameClient.InputHandling;
using PlatformerGameClient.Views.MenuComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerGameClient.Views
{
    /// <summary>
    /// This is a test file for testing new techniques out on.
    /// </summary>
    public class MainMenuVewTest : GameStateView
    {

        private Texture2D mainBackground;
        private SpriteFont m_fontMenu;
        private SpriteFont m_fontMenuSelect;
        private SpriteFont m_fontTitle;
        private Rectangle gameplay = new();
        private Rectangle about = new();
        private Rectangle quit = new();
        private Rectangle help = new();
        private Rectangle highScores = new();
        private Rectangle settings = new();

        private Dictionary<MenuState, MenuItem> m_menuItems = new Dictionary<MenuState, MenuItem>();





        private bool isEnterUp = false;
        SoundEffect hover;
        SoundEffectInstance soundInstance;
        public bool canUseMouse = false;




        private MenuItemList m_menuList;
        private MenuItem newGame;
        private MenuItem exit;
        private MenuItem settingsItem;

        private MenuItem backButton;


        // TODO: Make a 'MenuItemsList' Class, which will handle a list of items and how they are moved around and stuff.
        private List<MenuItem> menuItems;



        private enum MenuState
        {
            NewGame,
            /*HighScores,
            Help,
            Settings,
            About,*/
            Quit,
            /*None,*/
            Settings
        }

        private MenuState m_currentSelection = MenuState.NewGame;
        private MenuState m_prevSelection = MenuState.NewGame;
        private bool m_waitForKeyRelease = false;
        private Dictionary<MenuState, string> m_gameStates;
        private Dictionary<Rectangle, MenuState> m_shapeToState;


        private bool settingsFlag = false;
        private bool exitFlag = false;

        private KeyboardInput keyInput;

        private List<MenuObject> m_menuObjects;



        public override void loadContent(ContentManager contentManager)
        {
            m_fontMenu = contentManager.Load<SpriteFont>("Fonts/menu");
            m_fontMenuSelect = contentManager.Load<SpriteFont>("Fonts/menu-selected");
            hover = contentManager.Load<SoundEffect>("Sounds/little_robot_sound_factory_multimedia_Click_Electronic_14");
            /* mainBackground = contentManager.Load<Texture2D>("MainBackground");*/
            m_fontTitle = contentManager.Load<SpriteFont>("Fonts/mainmenuTitle");
            soundInstance = hover.CreateInstance();


            m_gameStates = new Dictionary<MenuState, string>
            {
                {MenuState.NewGame, "New Game!" },
                {MenuState.Quit, "Exit" },
                { MenuState.Settings, "Settings"}
            };




            keyInput = new KeyboardInput();

            keyInput.registerCommand(Keys.Up, true, new IInputDevice.CommandDelegate(UpHit));
            keyInput.registerCommand(Keys.Down, true, new IInputDevice.CommandDelegate(DownHit));


            float scale = m_graphics.PreferredBackBufferWidth / 1920f;
            Vector2 stringSize = m_fontMenuSelect.MeasureString("New Game!") * scale;
            float bottom = m_graphics.PreferredBackBufferWidth / 4;
            newGame = new MenuItem("New Game!", new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)bottom, (int)stringSize.X, (int)stringSize.Y), m_graphics, m_fontMenuSelect, m_spriteBatch, new MenuItem.OnClick(newGameOnClick), true);
            bottom += stringSize.Y;
            stringSize = m_fontMenu.MeasureString("Exit") * scale;

            exit = new MenuItem("Exit", new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)bottom, (int)stringSize.X, (int)stringSize.Y), m_graphics, m_fontMenu, m_spriteBatch, new MenuItem.OnClick(exitOnClick), false);
            bottom += stringSize.Y;
            stringSize = m_fontMenu.MeasureString("Settings") * scale;

            settingsItem = new MenuItem("Settings", new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)bottom, (int)stringSize.X, (int)stringSize.Y), m_graphics, m_fontMenu, m_spriteBatch, new MenuItem.OnClick(settingsClicked), false);

            


            menuItems = new List<MenuItem> { newGame, exit, settingsItem };

            m_menuList = new MenuItemList(menuItems, m_fontMenu, m_fontMenuSelect, "New Game!");

            stringSize = m_fontMenu.MeasureString("<--") * scale;
            backButton = new MenuItem("<--", new Rectangle(100, 100, (int)stringSize.X, (int)stringSize.Y), m_graphics, m_fontMenu, m_spriteBatch, new MenuItem.OnClick(backClicked), false);
            backButton.registerHover(new MenuItem.OnHover(onHover));
            /*keyInput.registerCommand(Keys.Enter, true, new IInputDevice.CommandDelegate(EnterHit));*/
            m_menuObjects = new List<MenuObject> {m_menuList, backButton };
        }


        public void onHover(GameTime gameTime, MenuItem menuItem)
        {
            Console.WriteLine("Something amazing happened...");
            menuItem.changeFont(m_fontMenuSelect);

        }
        public void backClicked(GameTime gameTime)
        {
            Console.WriteLine("Something amazing happened...");
        }
        public void settingsClicked(GameTime gameTime)
        {
            Console.WriteLine("Settings button was clicked!");
            settingsFlag = true;
        }


        public void newGameOnClick(GameTime gameTime)
        {
            Console.WriteLine("New Game was clicked! Or hit!");

        }


        public void exitOnClick(GameTime gameTime)
        {
            Console.WriteLine("Exit was clicked!");
            exitFlag = true;
        }

        /// <summary>
        /// This method is for when we leave this view, 
        /// we are setting the values to false because when we come back, 
        /// we want a small delay in what we can and cannot hit on the screen.
        /// </summary>
        /// <param name="gameTime"></param>
        private void setKeyAndMouseDefaults(GameTime gameTime)
        {
            isEnterUp = false;
            canUseMouse = false;
        }



        private void UpHit(GameTime gameTime)
        {
            if (m_currentSelection > 0)
            {
                this.m_currentSelection -= 1;
            }
            else
            {
                this.m_currentSelection = MenuState.Settings;
            }


           /* float bottom = 0;
            foreach (MenuItem item in menuItems)
            {
                if (m_gameStates[this.m_currentSelection] == item.text)
                {
                    // This item is selected.
                    item.setIsSelected(true);
                    item.changeFont(m_fontMenuSelect);
                    if (item != menuItems[0])
                    {
                        item.setBottom(bottom);
                    }

                }

                else
                {
                    // TODO: Find a way so we do not have to do this every time!
                    item.setIsSelected(false);
                    item.changeFont(m_fontMenu);
                    if (item != menuItems[0])
                    {
                        item.setBottom(bottom);
                    }
                }
                bottom = item.getStringSize();

            }*/
        }

        private void DownHit(GameTime gameTime)
        {
            if ((int)m_currentSelection < 2)
            {
                this.m_currentSelection += 1;
            }
            else
            {
                this.m_currentSelection = MenuState.NewGame;
            }

            /*float bottom = 0;
            foreach (MenuItem item in menuItems)
            {
                if (m_gameStates[this.m_currentSelection] == item.text)
                {
                    // This item is selected.
                    item.setIsSelected(true);
                    item.changeFont(m_fontMenuSelect);
                    if (item != menuItems[0])
                    {
                        item.setBottom(bottom);
                    }

                }

                else
                {
                    // TODO: Find a way so we do not have to do this every time!
                    item.setIsSelected(false);
                    item.changeFont(m_fontMenu);
                    if (item != menuItems[0])
                    {
                        item.setBottom(bottom);
                    }
                }
                bottom = item.getStringSize();

            }*/
        }
        private void modifyY()
        {
            foreach (MenuItem item in menuItems)
            {
                float bottom = item.getStringSize();

            }
        }

        private void modifyHover(MenuItem item)
        {
            /*float bottom = 0;
            foreach (MenuItem item2 in menuItems)
            {
                if (item2.text == item.text)
                {
                    // This item is selected.
                    item2.setIsSelected(true);
                    item2.changeFont(m_fontMenuSelect);
                    if (item2 != menuItems[0])
                    {
                        item2.setBottom(bottom);
                    }

                }

                else
                {
                    // TODO: Find a way so we do not have to do this every time!
                    item2.setIsSelected(false);
                    item2.changeFont(m_fontMenu);
                    if (item2 != menuItems[0])
                    {
                        item2.setBottom(bottom);
                    }
                }
                bottom = item2.getStringSize();

            }*/

        }
        public override GameStateEnum processInput(GameTime gameTime)
        {
            // Check if the mouse is hovering over any of the menu items!

            /*foreach (MenuItem item in menuItems)
            {

                if (item.isHoveredOver())
                {
                    // Notify the rest of the items that this is being hovered over
                    this.modifyHover(item);
                    break;
                }

            }*/
            
            /*m_menuList.processInput(gameTime);*/


            keyInput.Update(gameTime);


            if (isEnterUp && canUseMouse)
            {
                /*foreach (MenuItem item in menuItems)
                {
                    item.processInput(gameTime);
                }*/
                /*m_menuList.processInput(gameTime);*/
                foreach (MenuObject menuObject in m_menuObjects)
                {
                    menuObject.processInput(gameTime);
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Enter))
            {
                isEnterUp = true;
            }

            /* keyInput.Update(gameTime);*/

            /* if (Keyboard.GetState().IsKeyUp(Keys.Enter))
             {
                 isEnterUp = true;
             }

             if (Keyboard.GetState().IsKeyDown(Keys.Enter) && isEnterUp)
             {
                 setKeyAndMouseDefaults(gameTime);
                 try
                 {
                     return m_gameStates[m_currentSelection];
                 }
                 catch (Exception e)
                 {
                     Console.WriteLine(e);
                     return GameStateEnum.MainMenu;
                 }

             }
             Console.WriteLine("Got in here");

             Point mousePoint = Mouse.GetState().Position;*/
            /* if (canUseMouse)
             {
                 // Loop through the rectangles
                 *//*foreach (Rectangle rec in m_rectangles.Values)
                 {
                     // If the mouse point is not in the rectangle, skip the rest of the logic.
                     if (rec.Contains(mousePoint))
                     {
                         if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                         {
                             setKeyAndMouseDefaults(gameTime);
                             return m_gameStates[m_shapeToState[rec]];
                         }

                         m_currentSelection = m_shapeToState[rec];
                     }





                 }*//*





                 if (gameplay.Contains(Mouse.GetState().Position))
                 {
                     if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                     {
                         canUseMouse = false;
                         isEnterUp = false;

                         return GameStateEnum.EnterName;
                     }
                     m_currentSelection = MenuState.NewGame;



                 }
                 else if (help.Contains(Mouse.GetState().Position))
                 {
                     if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                     {
                         canUseMouse = false;
                         isEnterUp = false;

                         return GameStateEnum.Help;
                     }
                     m_currentSelection = MenuState.Help;

                 }
                 else if (about.Contains(Mouse.GetState().Position))
                 {
                     if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                     {
                         canUseMouse = false;
                         isEnterUp = false;

                         return GameStateEnum.About;
                     }
                     m_currentSelection = MenuState.About;

                 }
                 else if (highScores.Contains(Mouse.GetState().Position))
                 {
                     if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                     {
                         canUseMouse = false;
                         isEnterUp = false;

                         return GameStateEnum.HighScores;
                     }
                     m_currentSelection = MenuState.HighScores;

                 }
                 else if (quit.Contains(Mouse.GetState().Position))
                 {
                     if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                     {
                         canUseMouse = false;
                         isEnterUp = false;

                         return GameStateEnum.Exit;
                     }
                     m_currentSelection = MenuState.Quit;

                 }

                 else if (settings.Contains(Mouse.GetState().Position))
                 {
                     if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                     {
                         canUseMouse = false;
                         isEnterUp = false;

                         return GameStateEnum.Settings;
                     }
                     m_currentSelection = MenuState.Settings;

                 }
             }

             if (m_prevSelection != m_currentSelection && m_currentSelection != MenuState.None)
             {
                 if (soundInstance.State == SoundState.Playing)
                 {
                     soundInstance.Stop();

                 }
                 soundInstance.Play();

             }



             if (Mouse.GetState().LeftButton == ButtonState.Pressed)
             {
                 canUseMouse = false;
             }
             if (Mouse.GetState().LeftButton == ButtonState.Released)
             {
                 canUseMouse = true;
             }
             m_prevSelection = m_currentSelection;*/
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                canUseMouse = false;
            }
            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                canUseMouse = true;
            }
            if (settingsFlag)
            {
                setKeyAndMouseDefaults(gameTime);
                settingsFlag = false;
                return GameStateEnum.Settings;
            }

            if (exitFlag)
            {
                setKeyAndMouseDefaults(gameTime);
                exitFlag = false;
                return GameStateEnum.Exit;
            }



            return GameStateEnum.MainMenu;
        }

        public override void render(GameTime gameTime)
        {
            m_spriteBatch.Begin();
            // Rend the background

            /* m_spriteBatch.Draw(mainBackground, new Rectangle(0, 0, m_graphics.PreferredBackBufferWidth, m_graphics.PreferredBackBufferHeight), Color.White);*/

            /*float bottom = drawMenuItem(m_fontTitle, "Insert Platformer Game Name Here...", m_graphics.PreferredBackBufferHeight / 4, Color.Black);
            bottom = drawMenuItem(m_currentSelection == MenuState.NewGame ? m_fontMenuSelect : m_fontMenu, "Join Game", bottom, m_currentSelection == MenuState.NewGame ? Color.White : Color.LightGray);

            bottom = drawMenuItem(m_currentSelection == MenuState.HighScores ? m_fontMenuSelect : m_fontMenu, "High Scores", bottom, m_currentSelection == MenuState.HighScores ? Color.White : Color.LightGray);

            bottom = drawMenuItem(m_currentSelection == MenuState.Help ? m_fontMenuSelect : m_fontMenu, "Help", bottom, m_currentSelection == MenuState.Help ? Color.White : Color.LightGray);
            bottom = drawMenuItem(m_currentSelection == MenuState.Settings ? m_fontMenuSelect : m_fontMenu, "Settings", bottom, m_currentSelection == MenuState.Settings ? Color.White : Color.LightGray);

            bottom = drawMenuItem(m_currentSelection == MenuState.About ? m_fontMenuSelect : m_fontMenu, "About", bottom, m_currentSelection == MenuState.About ? Color.White : Color.LightGray);
            drawMenuItem(m_currentSelection == MenuState.Quit ? m_fontMenuSelect : m_fontMenu, "Quit", bottom, m_currentSelection == MenuState.Quit ? Color.White : Color.LightGray);*/
            foreach (MenuObject menuObject in m_menuObjects)
            {
                menuObject.draw();
            }

            m_spriteBatch.End();

        }
        private float drawMenuItem(SpriteFont font, string text, float y, Color color)
        {

            float scale = m_graphics.PreferredBackBufferWidth / 1920f;
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
            if (text == "Join Game")
            {
                gameplay = new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)y, (int)stringSize.X, (int)stringSize.Y);
            }
            if (text == "High Scores")
            {
                highScores = new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)y, (int)stringSize.X, (int)stringSize.Y);

            }
            if (text == "Help")
            {
                help = new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)y, (int)stringSize.X, (int)stringSize.Y);

            }
            if (text == "About")
            {
                about = new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)y, (int)stringSize.X, (int)stringSize.Y);

            }
            if (text == "Quit")
            {
                quit = new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)y, (int)stringSize.X, (int)stringSize.Y);

            }
            if (text == "Settings")
            {
                settings = new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)y, (int)stringSize.X, (int)stringSize.Y);
            }

            return y + stringSize.Y;
        }


        public override void update(GameTime gameTime)
        {



        }
    }
    }
