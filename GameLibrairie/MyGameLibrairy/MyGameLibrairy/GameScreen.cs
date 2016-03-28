using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MyGameLibrairy
{
    /// <summary>
    /// Represent a basic class for every screen that will need the be drawn.
    /// </summary>
    public class GameScreen
    {
        protected ContentManager m_Content;
        protected IServiceProvider m_ServiceProvider;
        protected static List<GameScreen> m_ListGameScreen = new List<GameScreen>();
        protected static GraphicsDeviceManager m_GraphicsDeviceManager;

        public static GameScreen ChangeScreen(GameScreen Screen)
        {
            Screen.Load();
            RemoveAllScreens();
            m_ListGameScreen.Add(Screen);

            return Screen;
        }

        public static void RemoveScreen(int Pos)
        {
            m_ListGameScreen[Pos].Alive = false;
        }

        public static void RemoveScreen(GameScreen Screen)
        {
            Screen.Alive = false;
        }

        public static void RemoveAllScreens(GameScreen ExcludedScreen = null)
        {
            for (int S = 0; S < m_ListGameScreen.Count; S++)
                if (m_ListGameScreen[S] != ExcludedScreen)
                    m_ListGameScreen[S].Alive = false;
        }


        /// <summary>
        /// Decide if the current GameScreen is active or need to be removed.
        /// </summary>
        public bool Alive = true;

        /// <summary>
        /// Tell if the current GameScreen is on the of the screen.
        /// </summary>
        public bool IsOnTop = true;

        /// <summary>
        /// Force the screen to idle if the current GameScreen IsOnTop is false.
        /// </summary>
        public bool RequireFocus = true;

        /// <summary>
        /// Force the screen to hide if the current GameScreen IsOnTop is false.
        /// </summary>
        public bool RequireDrawFocus = false;

        /// <summary>
        /// Decide at which transparancy the current GameScreen need to be drawn(such as popup screen).
        /// </summary>
        public int Transparancy = 255;//Work in progress

        /// <summary>
        /// Empty constructor if no ContentManager is required.
        /// </summary>
        public GameScreen() { }

        /// <summary>
        /// Instanciate a new GameScreen object.
        /// </summary>
        /// <param name="serviceProvider">A IServiceProvider used to create a new ContentManager.</param>
        public GameScreen(IServiceProvider serviceProvider,GraphicsDeviceManager graphics)
        {
            m_Content = new ContentManager(serviceProvider);
            m_Content.RootDirectory = "Content";
            m_GraphicsDeviceManager = graphics;
            this.m_ServiceProvider = serviceProvider;
        }

        public virtual void Load()
        {

        }

        /// <summary>
        /// Override the Update to make your own game logic for the screen.
        /// </summary>
        public virtual void Update(GameTime gameTime)
        {
            /*
            for (int i = 0; i < m_ListGameScreen.Count; i++)
            {
                m_ListGameScreen[i].IsOnTop = (i == m_ListGameScreen.Count - 1);

                //If the GameScreen requires to be on top and is on top or doesn't requires focus to be updated.
                if ((m_ListGameScreen[i].RequireFocus && m_ListGameScreen[i].IsOnTop) || !m_ListGameScreen[i].RequireFocus)
                {
                    //Update everything in the GameScreen List and delete it if not Alive.
                    m_ListGameScreen[i].Update(gameTime);
                    if (!m_ListGameScreen[i].Alive)
                    {
                        m_ListGameScreen.RemoveAt(i--);
                    }
                }
            }*/
        }

        /// <summary>
        /// Override the Update to make your own drawing logic for the screen.
        /// </summary>
        public virtual void Draw(GameTime aGametime, SpriteBatch aSpriteBatch)
        {
         /* for (int i = 0; i < m_ListGameScreen.Count; i++)
            {
                GameScreen currentScreen = m_ListGameScreen[i];
                if ((currentScreen.RequireDrawFocus && currentScreen.IsOnTop) || !currentScreen.RequireDrawFocus)
                {
                    if (currentScreen.Alive)
                    {
                        currentScreen.Draw(aGametime, aSpriteBatch);
                    }
                }
            }
          * */
        }
    }

    /// <summary>
    /// Represent a temporary GameScreen that will unload every other GameScreen then load a new one.
    /// </summary>
    public class LoadScreen : GameScreen
    {
        private GameScreen[] Screens;
        private Texture2D BackgroundBuffer;

        /// <summary>
        /// Initialize a new LoadScreen that will clear the other screens and load the new one automatically.
        /// </summary>
        /// <param name="Game">Requires the main form of the project to have access to its members.</param>
        /// <param name="Screens">An array of GameScreen to create while loading.</param>
        /// <param name="BackgroundBuffer">A background picture to use for the loading screen. Can be null.</param>
        /// <param name="graphics">Pour utiliser tout ce qui attrait a la fenetre</param>
        public LoadScreen(IServiceProvider serviceProvider, GameScreen[] Screens, Texture2D BackgroundBuffer, GraphicsDeviceManager graphics)
            : base(serviceProvider, graphics)
        {
            this.Screens = Screens;
            this.BackgroundBuffer = BackgroundBuffer;

            for (int i = 0; i < GameScreen.m_ListGameScreen.Count; i++)
            {
                GameScreen.RemoveScreen(i);
            }
        }
        public override void Load()
        { 

        }

        public override void Update(GameTime gameTime)
        {
            if (GameScreen.m_ListGameScreen.Count == 1)
            {
                GameScreen.RemoveScreen(this);
                for (int i = 0; i < Screens.Length; i++)
                {
                    GameScreen.ChangeScreen(Screens[i]);
                }
            }
        }
        public override void Draw(GameTime gametime,SpriteBatch g)
        {
            if (BackgroundBuffer != null)
            {
                g.Draw(BackgroundBuffer, new Vector2(0, 0), Color.White);
            }
           // g.DrawString(Game.fntArial, "Loading", new Vector2(Game.Width - 100, Game.Height - 80), Color.Black);
        }
    }
}
