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
    public enum GameState
    {
        Intro,
        MainMenu,
        Options,
        Level1,
    }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        StateMachine m_StateMachine;
        GameState m_CurrentGameState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

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
            int a = 3;
        }

        public void OnUpdateIntro()
        {
            int a = 2; 
            KeyboardHelper.PlayerState = Keyboard.GetState();
            if (KeyboardHelper.KeyPressed(Keys.A))
            {
                m_StateMachine.SetState(GameState.MainMenu);
            }
            KeyboardHelper.PlayerStateLast = Keyboard.GetState();
        }

        public void OnExitIntro()
        {
            int a = 3;
        }

        public void OnEnterMainMenu()
        {
            int a = 2;
        }

        public void OnUpdateMainMenu()
        {
            int a = 2;
        }

        public void OnExitMainMenu()
        {
            int a = 2;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            m_StateMachine.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
