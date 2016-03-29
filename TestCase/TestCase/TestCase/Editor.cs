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

        GameState m_CurrentGameState;
        StateMachine m_StateMachine;

        Button m_CreateButton;

        public Editor()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            GameScreenMapper.AddEntry(GameState.Editor, new IntroScreen(Services, m_Graphics));

            m_StateMachine = new StateMachine();
            m_StateMachine.AddState(GameState.Editor, Status.OnEnter, OnEnterEditor);
            m_StateMachine.AddState(GameState.Editor, Status.OnUpdate, OnUpdateEditor);
            m_StateMachine.AddState(GameState.Editor, Status.OnExit, OnExitEditor);

            IsMouseVisible = true;
        }

        public void OnEnterEditor()
        {
            m_CreateButton = new Button(
                GameRessources.m_EmptyButton,
                new Vector2(100, 100),
                GraphicsDevice);
        }

        public void OnUpdateEditor()
        {
            m_CreateButton.Update();
        }

        public void OnExitEditor()
        {

        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            GameRessources.LoadContent(Content);

            m_StateMachine.SetState(GameState.Editor);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            MouseHelper.m_PlayerState = Mouse.GetState();

            m_StateMachine.Update();

            MouseHelper.m_LastPlayerState = Mouse.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            m_SpriteBatch.Begin();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            m_CreateButton.Draw(m_SpriteBatch); // put on a new screen !!
            m_SpriteBatch.DrawString(GameRessources.m_SpriteFont, m_CreateButton.ToString(), Vector2.Zero, Color.Red);
            
            base.Draw(gameTime);

            m_SpriteBatch.End();
        }
    }
}
