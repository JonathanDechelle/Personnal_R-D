using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TestCase
{
    public class GameRessources
    {
        public static SpriteFont m_SpriteFont;
        public static Texture2D m_EmptyButton;

        public static void LoadContent(ContentManager aContent)
        {
            m_EmptyButton = aContent.Load<Texture2D>("EmptyButton");
            m_SpriteFont = aContent.Load<SpriteFont>("Font");
        }
    }
}
