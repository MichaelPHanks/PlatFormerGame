using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformerGameClient.Enums;
using PlatformerGameClient.Interfaces;
using PlatformerGameClient.Views;
using System.Collections.Generic;
using System.Runtime;

namespace PlatformerGame
{
    public class PlatformerMainClient : Game
    {
        private GraphicsDeviceManager m_graphics;
        private SpriteBatch m_spriteBatch;
        private Dictionary<GameStateEnum, IGameState> m_gameStates;
        private IGameState m_currentState;

        public PlatformerMainClient()
        {
            m_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            m_gameStates = new Dictionary<GameStateEnum, IGameState>
            {
                { GameStateEnum.MainMenu, new MainMenuView() },
                { GameStateEnum.GamePlay, new GamePlayView()},
                { GameStateEnum.Settings, new SettingsView()}
            };
            m_currentState = m_gameStates[GameStateEnum.MainMenu];
            foreach (var item in m_gameStates)
            {
                item.Value.initialize(this.GraphicsDevice, m_graphics);
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
            foreach (var item in m_gameStates)
            {
                item.Value.loadContent(this.Content);
            }

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            GameStateEnum nextStateEnum = this.m_currentState.processInput(gameTime);

            if (nextStateEnum == GameStateEnum.Exit)
            {
                Exit();
            }
            else
            {
                m_currentState.update(gameTime);


              /*  if (m_prevState == m_gameStates[GameStateEnum.Tutorial] && nextStateEnum == GameStateEnum.GamePlay)
                {
                    m_gamePlayView.ConnectToServer();

                }

                *//*if (m_prevState == m_gameStates[GameStateEnum.GamePlay] && nextStateEnum == GameStateEnum.Paused)
                {
                    savedGamePlay = m_currentState;
                }*//*





                if (nextStateEnum == GameStateEnum.Settings && m_gameState != GameStateEnum.Settings)
                {

                    m_settings.prevState = m_gameState;


                }


                if (nextStateEnum == GameStateEnum.Help && m_gameState != GameStateEnum.Help)
                {

                    m_helpView.helpPrevState = m_gameState;


                }

                if (nextStateEnum == GameStateEnum.HighScores)
                {
                    m_gameStates[nextStateEnum] = null;
                    m_gameStates[nextStateEnum] = new HighScoresView();
                    m_gameStates[nextStateEnum].initialize(this.GraphicsDevice, m_graphics);
                    m_gameStates[nextStateEnum].loadContent(this.Content);


                }*/


                m_currentState = m_gameStates[nextStateEnum];
             /*   m_prevState = m_gameStates[nextStateEnum];
                m_gameState = nextStateEnum;*/

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            m_currentState.render(gameTime);

            base.Draw(gameTime);
        }
    }
}
