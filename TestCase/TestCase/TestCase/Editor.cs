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

        Color[] DebugColor = new Color[3] 
        { 
            Color.Red, 
            Color.Green,
            Color.CornflowerBlue,
        };
        Color m_CurrentDebugColor;

        public Editor()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            GameScreenMapper.AddEntry(GameState.Editor, new IntroScreen(Services, m_Graphics));

            m_StateMachine = new StateMachine();
            m_StateMachine.AddState(GameState.Editor, Status.OnEnter, OnEnterEditor);
            m_StateMachine.AddState(GameState.Editor, Status.OnUpdate, OnUpdateEditor);
            m_StateMachine.AddState(GameState.Editor, Status.OnExit, OnExitEditor);

            m_StateMachine.SetState(GameState.Editor);
            m_CurrentDebugColor = DebugColor[2];
        }

        public void OnEnterEditor()
        {

        }

        public void OnUpdateEditor()
        {
            if (MouseHelper.MouseKeyPress(MouseButton.Left))
            {
                Console.Write("Mouse Left click");
                m_CurrentDebugColor = DebugColor[0];
            }
            else if (MouseHelper.MouseKeyHold(MouseButton.Right))
            {
                Console.Write("Mouse Right hold");
                m_CurrentDebugColor = DebugColor[1];
            }
            else
            {
                Console.Write("No button selected");
                m_CurrentDebugColor = DebugColor[2];
            }
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
            GraphicsDevice.Clear(m_CurrentDebugColor);
            base.Draw(gameTime);
        }
    }
}
