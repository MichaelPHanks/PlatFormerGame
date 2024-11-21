using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformerGameClient.Enums;
using PlatformerGameClient.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerGameClient.Views
{
    public class MainMenuView : GameStateView
    {

        private Texture2D mainBackground;
        private SpriteFont m_fontMenu;
        private SpriteFont m_fontMenuSelect;
        private SpriteFont m_fontTitle;
        private Rectangle about = new Rectangle();
        private Rectangle quit = new Rectangle();
        private Rectangle help = new Rectangle();
        private Rectangle highScores = new Rectangle();
        private Rectangle settings = new Rectangle();
        private Rectangle gameplay = new Rectangle();

        private Dictionary<string, Rectangle> m_recs = new Dictionary<string, Rectangle>();





        private bool isEnterUp = false;
        SoundEffect hover;
        SoundEffectInstance soundInstance;
        public bool canUseMouse = false;


        private RenderMenuItem renderNormal;
        private RenderMenuItem renderSelected;


        private enum MenuState
        {
            NewGame,
            HighScores,
            Help,
            Settings,
            About,
            Quit,
            None,
        }

        private MenuState m_currentSelection = MenuState.NewGame;
        private MenuState m_prevSelection = MenuState.NewGame;
        private bool m_waitForKeyRelease = false;
        public override void loadContent(ContentManager contentManager)
        {
            m_recs = new Dictionary<string, Rectangle>
            { {"Join Game", this.gameplay },
                { "High Scores", this.highScores},
                { "Help", this.help},
                { "About", this.about},
                { "Quit", this.quit},
                { "Settings", this.settings}
            };
            m_fontMenu = contentManager.Load<SpriteFont>("Fonts/menu");


            /*  renderNormal = new RenderMenuItem(m_fontMenuSelect, );
              renderSelected = new RenderMenuItem();*/

        }

        //throw new NotImplementedException();


        public override GameStateEnum processInput(GameTime gameTime)
        {
            return GameStateEnum.MainMenu;
            
        }

        public override void render(GameTime gameTime)
        {
            m_spriteBatch.Begin();
            // Rend the background

            // m_spriteBatch.Draw(mainBackground, new Rectangle(0, 0, m_graphics.PreferredBackBufferWidth, m_graphics.PreferredBackBufferHeight), Color.White);
            float bottom = m_graphics.PreferredBackBufferHeight / 4;

            foreach (string key in m_recs.Keys) 
            {
                bottom = renderMenuItem(m_fontMenu, key, bottom, Color.Black, m_spriteBatch, m_graphics);
            }





       /*     float bottom = drawMenuItem(m_fontTitle, "SNAKE GAME!", m_graphics.PreferredBackBufferHeight / 4, Color.Black);
            bottom = drawMenuItem(m_currentSelection == MenuState.NewGame ? m_fontMenuSelect : m_fontMenu, "Join Game", bottom, m_currentSelection == MenuState.NewGame ? Color.White : Color.LightGray);

            bottom = drawMenuItem(m_currentSelection == MenuState.HighScores ? m_fontMenuSelect : m_fontMenu, "High Scores", bottom, m_currentSelection == MenuState.HighScores ? Color.White : Color.LightGray);

            bottom = drawMenuItem(m_currentSelection == MenuState.Help ? m_fontMenuSelect : m_fontMenu, "Help", bottom, m_currentSelection == MenuState.Help ? Color.White : Color.LightGray);
            bottom = drawMenuItem(m_currentSelection == MenuState.Settings ? m_fontMenuSelect : m_fontMenu, "Settings", bottom, m_currentSelection == MenuState.Settings ? Color.White : Color.LightGray);

            bottom = drawMenuItem(m_currentSelection == MenuState.About ? m_fontMenuSelect : m_fontMenu, "About", bottom, m_currentSelection == MenuState.About ? Color.White : Color.LightGray);
            drawMenuItem(m_currentSelection == MenuState.Quit ? m_fontMenuSelect : m_fontMenu, "Quit", bottom, m_currentSelection == MenuState.Quit ? Color.White : Color.LightGray);
*/

            m_spriteBatch.End();
        }

        public float renderMenuItem(SpriteFont font, string text, float y, Color color, SpriteBatch m_spriteBatch, GraphicsDeviceManager m_graphics)
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

            return y + stringSize.Y;
        }

        public override void update(GameTime gameTime)
        {
        }
    }

}
