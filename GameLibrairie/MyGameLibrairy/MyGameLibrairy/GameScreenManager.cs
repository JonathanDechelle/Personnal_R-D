using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyGameLibrairy
{
    public class GameScreenManager
    {
        private static GameScreen m_CurrentGameScreen;
        protected static List<GameScreen> m_ListGameScreen = new List<GameScreen>();

        public static GameScreen ShowScreen(GameScreen aScreen, GraphicsDevice aGraphicDevice = null)
        {
            if (ContainScreen(aScreen))
            {
                m_CurrentGameScreen = aScreen;
                return aScreen;
            }

            aScreen.Load(aGraphicDevice);
            m_ListGameScreen.Add(aScreen);
            m_CurrentGameScreen = aScreen;
            return aScreen;
        }

        public static void HideScreen(GameScreen aScreen)
        {
            if (!ContainScreen(aScreen))
            {
                return;
            }

            m_ListGameScreen.Remove(aScreen);
            aScreen = null;
            m_CurrentGameScreen = m_ListGameScreen.Count > 0 ?
                m_ListGameScreen[m_ListGameScreen.Count - 1] :
                null;
        }

        private static void HideAllScreens(GameScreen aExcludedScreen = null)
        {
            for (int i = 0; i < m_ListGameScreen.Count; i++)
            {
                if (m_ListGameScreen[i] != aExcludedScreen)
                {
                    m_ListGameScreen.RemoveAt(i);
                    i--;
                }
            }
        }

        public static GameScreen GetCurrentScreen()
        {
            return m_CurrentGameScreen;
        }

        public static GameScreen GetScreen(GameScreen aScreen)
        {
            if (ContainScreen(aScreen))
            {
                for (int i = 0; i < m_ListGameScreen.Count; i++)
                {
                    if (m_ListGameScreen[i] == aScreen)
                    {
                        return aScreen;
                    }
                }
            }

            return null;
        }

        public static bool ContainScreen(GameScreen aScreen)
        {
            return m_ListGameScreen.Contains(aScreen);
        }

        public static void Update(GameTime aGametime)
        {
            if (m_CurrentGameScreen != null)
            {
                m_CurrentGameScreen.Update(aGametime);
            }
        }

        public static void Draw(GameTime aGametime, SpriteBatch aSpriteBatch)
        {
            if (m_CurrentGameScreen != null)
            {
                m_CurrentGameScreen.Draw(aGametime, aSpriteBatch);
            }
        }
    }
}
