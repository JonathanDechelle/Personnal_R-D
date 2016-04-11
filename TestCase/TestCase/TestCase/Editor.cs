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
    public class Editor : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager m_Graphics;
        SpriteBatch m_SpriteBatch;

        StateMachine m_StateMachine;

        protected override void Initialize()
        {
            base.Initialize();
        }

        public Editor()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            GameScreenMapper.AddEntry(EGameState.Editor, new EditorScreen(Services, m_Graphics));

            m_StateMachine = new StateMachine();
            m_StateMachine.AddState(EGameState.Editor, Status.OnEnter, OnEnterEditor);

            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            GameRessources.LoadContent(Content);

            ScreenSaver.SetRootDirectory(Content);
            m_StateMachine.SetState(EGameState.Editor);
        }

        public void OnEnterEditor()
        {
            GameScreen editorScreen = GameScreenManager.ShowScreen(GameScreenMapper.GetValue(EGameState.Editor), GraphicsDevice);
            ScreenSaver.LoadScreenContent(editorScreen);
            ScreenSaver.SaveScreenContent(editorScreen);
        }

        protected override void Update(GameTime gameTime)
        {
            MouseHelper.m_PlayerState = Mouse.GetState();
            KeyboardHelper.PlayerState = Keyboard.GetState();

            m_StateMachine.Update();
            GameScreenManager.Update(gameTime);

            if (KeyboardHelper.KeyPressed(Keys.J))
            {
                ScreenSaver.SaveScreenContent(GameScreenManager.GetCurrentScreen());
            }

            MouseHelper.m_LastPlayerState = Mouse.GetState();
            KeyboardHelper.PlayerStateLast = Keyboard.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GameScreenManager.Draw(gameTime, m_SpriteBatch);
            base.Draw(gameTime);
        }
    }
}
