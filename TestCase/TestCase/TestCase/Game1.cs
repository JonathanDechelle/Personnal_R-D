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

        EGameState m_CurrentGameState;
        StateMachine m_StateMachine; 

        public Game1()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            GameScreenMapper.AddEntry(EGameState.Intro, new IntroScreen(Services, m_Graphics));
            GameScreenMapper.AddEntry(EGameState.MainMenu, new MainMenuScreen(Services, m_Graphics));

            m_StateMachine = new StateMachine();
            m_StateMachine.AddState(EGameState.Intro, Status.OnEnter, OnEnterIntro);
            m_StateMachine.AddState(EGameState.Intro, Status.OnUpdate, OnUpdateIntro);
            m_StateMachine.AddState(EGameState.Intro, Status.OnExit, OnExitIntro);
            m_StateMachine.AddState(EGameState.MainMenu, Status.OnEnter, OnEnterMainMenu);
            m_StateMachine.AddState(EGameState.MainMenu, Status.OnUpdate, OnUpdateMainMenu);
            m_StateMachine.AddState(EGameState.MainMenu, Status.OnExit, OnExitMainMenu);

            m_StateMachine.SetState(EGameState.Intro);
        }

        public void OnEnterIntro()
        {
            GameScreenManager.ShowScreen(GameScreenMapper.GetValue(EGameState.Intro));
        }

        public void OnUpdateIntro()
        {
            if (KeyboardHelper.KeyPressed(Keys.A))
            {
                m_StateMachine.SetState(EGameState.MainMenu);
            }

            if (KeyboardHelper.KeyPressed(Keys.W))
            {
                GameScreenManager.HideScreen(GameScreenMapper.GetValue(EGameState.Intro));
            }
        }

        public void OnExitIntro()
        {
            GameScreenManager.HideScreen(GameScreenMapper.GetValue(EGameState.Intro));
        }

        public void OnEnterMainMenu()
        {
            GameScreenManager.ShowScreen(GameScreenMapper.GetValue(EGameState.MainMenu));
        }

        public void OnUpdateMainMenu()
        {
            if (KeyboardHelper.KeyPressed(Keys.A))
            {
                m_StateMachine.SetState(EGameState.Intro);
            }

            if (KeyboardHelper.KeyPressed(Keys.W))
            {
                GameScreenManager.HideScreen(GameScreenMapper.GetValue(EGameState.MainMenu));
            }
        }

        public void OnExitMainMenu()
        {
            GameScreenManager.HideScreen(GameScreenMapper.GetValue(EGameState.MainMenu));
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
            m_CurrentGameState = (EGameState) m_StateMachine.GetCurrentState(); //Just for debug for now

            GameScreenManager.Update(gameTime);

            KeyboardHelper.PlayerStateLast = Keyboard.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GameScreenManager.Draw(gameTime, m_SpriteBatch);

            base.Draw(gameTime);
        }
    }
}
