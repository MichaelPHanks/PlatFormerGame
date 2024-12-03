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
            Back,
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

        private string currentSelection = "New Game!";

        private ControllerInput ControllerInput;



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
                { MenuState.Settings, "Settings"},
                {MenuState.Back, "<--" }
            };




            keyInput = new KeyboardInput();

            keyInput.registerCommand(Keys.Up, true, new IInputDevice.CommandDelegate(UpHit));
            keyInput.registerCommand(Keys.Down, true, new IInputDevice.CommandDelegate(DownHit));

/*
            float scale = m_graphics.PreferredBackBufferWidth / 1920f;
            Vector2 stringSize = m_fontMenuSelect.MeasureString("New Game!") * scale;
            float bottom = m_graphics.PreferredBackBufferWidth / 4;
            newGame = new MenuItem("New Game!", new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)bottom, (int)stringSize.X, (int)stringSize.Y), m_graphics, m_fontMenuSelect, m_spriteBatch, new MenuItem.OnClick(newGameOnClick), true, m_fontMenuSelect);
            bottom += stringSize.Y;
            stringSize = m_fontMenu.MeasureString("Exit") * scale;

            exit = new MenuItem("Exit", new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)bottom, (int)stringSize.X, (int)stringSize.Y), m_graphics, m_fontMenu, m_spriteBatch, new MenuItem.OnClick(exitOnClick), false, m_fontMenuSelect);
            bottom += stringSize.Y;
            stringSize = m_fontMenu.MeasureString("Settings") * scale;

            settingsItem = new MenuItem("Settings", new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)bottom, (int)stringSize.X, (int)stringSize.Y), m_graphics, m_fontMenu, m_spriteBatch, new MenuItem.OnClick(settingsClicked), false, m_fontMenuSelect);

            


            menuItems = new List<MenuItem> { nzewGame, exit, settingsItem };
*/

            m_menuList = new MenuItemList(new List<string> {"New Game!", "Exit", "Settings" }, m_fontMenu, m_fontMenuSelect, "New Game!", this.m_graphics, this.m_spriteBatch, new Vector2(0, m_graphics.PreferredBackBufferWidth / 4));
            float scale = m_graphics.PreferredBackBufferWidth / 1920f;

            Vector2 stringSize = m_fontMenu.MeasureString("<--") * scale;
            backButton = new MenuItem("<--", new Rectangle(100, 100, (int)stringSize.X, (int)stringSize.Y), m_graphics, m_fontMenu, m_spriteBatch, new MenuItem.OnClick(backClicked), false, m_fontMenuSelect);
            backButton.registerHover(new MenuItem.OnHover(onHover));
            /*keyInput.registerCommand(Keys.Enter, true, new IInputDevice.CommandDelegate(EnterHit));*/
            m_menuObjects = new List<MenuObject> {m_menuList, backButton };

            ControllerInput = new ControllerInput();

            ControllerInput.registerCommand(Buttons.DPadUp, true, new IInputDevice.CommandDelegate(UpHit));
            ControllerInput.registerCommand(Buttons.DPadDown, true, new IInputDevice.CommandDelegate(DownHit));
            ControllerInput.registerCommand(Buttons.LeftThumbstickUp, true, new IInputDevice.CommandDelegate(UpHit));
            ControllerInput.registerCommand(Buttons.LeftThumbstickDown, true, new IInputDevice.CommandDelegate(DownHit));



            m_menuList.registerOnClick("Exit", new MenuItem.OnClick(exitOnClick));
            m_menuList.registerOnClick("Settings", new MenuItem.OnClick(settingsClicked));
            

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

            foreach (MenuObject item in m_menuObjects)
            {
                item.selectionChanged(this.m_gameStates[this.m_currentSelection]);
                currentSelection = this.m_gameStates[this.m_currentSelection];

            }


        }

        private void DownHit(GameTime gameTime)
        {
            if ((int)m_currentSelection < 3)
            {
                this.m_currentSelection += 1;
            }
            else
            {
                this.m_currentSelection = MenuState.Back;
            }

            foreach (MenuObject item in m_menuObjects)
            {
                item.selectionChanged(this.m_gameStates[this.m_currentSelection]);
                currentSelection = this.m_gameStates[this.m_currentSelection];
            }


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
            

        }
        public override GameStateEnum processInput(GameTime gameTime)
        {
            


            keyInput.Update(gameTime);
            ControllerInput.Update(gameTime);

            if (isEnterUp && canUseMouse)
            {
           

                string itemSelected = currentSelection;
                foreach(MenuObject item in m_menuObjects)
                {
                    

                    if (item.isHoveredOver() != "")
                    {
                        itemSelected = item.isHoveredOver();
                        break;
                    }

                }

                if (itemSelected != currentSelection)
                {
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

            
            foreach (MenuObject menuObject in m_menuObjects)
            {
                menuObject.draw();
            }

            m_spriteBatch.End();

        }
      

        public override void update(GameTime gameTime)
        {



        }
    }
    }
