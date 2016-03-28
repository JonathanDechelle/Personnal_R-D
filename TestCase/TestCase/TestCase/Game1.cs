using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MyGameLibrairy;

namespace TestCase
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager m_Graphics;
        SpriteBatch m_SpriteBatch;

        GameState m_CurrentGameState;
        StateMachine m_StateMachine; 
        GameScreen m_CurrentGameScreen;

        public Game1()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            GameScreenMapper.AddEntry(GameState.Intro, new IntroScreen(Services, m_Graphics));
            GameScreenMapper.AddEntry(GameState.MainMenu, new MainMenuScreen(Services, m_Graphics));

            m_StateMachine = new StateMachine();
            m_StateMachine.AddState(GameState.Intro, Status.OnEnter, OnEnterIntro);
            m_StateMachine.AddState(GameState.Intro, Status.OnUpdate, OnUpdateIntro);
            m_StateMachine.AddState(GameState.Intro, Status.OnExit, OnExitIntro);
            m_StateMachine.AddState(GameState.MainMenu, Status.OnEnter, OnEnterMainMenu);
            m_StateMachine.AddState(GameState.MainMenu, Status.OnUpdate, OnUpdateMainMenu);
            m_StateMachine.AddState(GameState.MainMenu, Status.OnExit, OnExitMainMenu);

            m_StateMachine.SetState(GameState.Intro);
        }

        public void OnEnterIntro()
        {
            m_CurrentGameScreen = GameScreen.ChangeScreen(GameScreenMapper.GetValue(GameState.Intro));
        }

        public void OnUpdateIntro()
        {
            if (KeyboardHelper.KeyPressed(Keys.A))
            {
                m_StateMachine.SetState(GameState.MainMenu);
            }
        }

        public void OnExitIntro()
        {
            /* You Quit State for another */
        }

        public void OnEnterMainMenu()
        {
            m_CurrentGameScreen = GameScreen.ChangeScreen(GameScreenMapper.GetValue(GameState.MainMenu));
        }

        public void OnUpdateMainMenu()
        {
            if (KeyboardHelper.KeyPressed(Keys.A))
            {
                m_StateMachine.SetState(GameState.Intro);
            }
        }

        public void OnExitMainMenu()
        {
            /* You Quit State for another */
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardHelper.PlayerState = Keyboard.GetState();

            m_StateMachine.Update();
            m_CurrentGameScreen.Update(gameTime);
            m_CurrentGameState = (GameState) m_StateMachine.GetCurrentState(); //Just for debug for now

            KeyboardHelper.PlayerStateLast = Keyboard.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            m_CurrentGameScreen.Draw(gameTime, m_SpriteBatch);
            base.Draw(gameTime);
        }
    }
}
