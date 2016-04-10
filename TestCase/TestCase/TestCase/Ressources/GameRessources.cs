using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;

namespace TestCase
{
    public class GameRessources
    {
        public static SpriteFont m_SpriteFont;
        public static Texture2D m_EmptyButton;
        public static Texture2D m_EmptyTextField;

        public static void LoadContent(ContentManager aContent)
        {
            m_EmptyButton = aContent.Load<Texture2D>("EmptyButton");
            m_EmptyTextField = aContent.Load<Texture2D>("EmptyTextField");
            m_SpriteFont = aContent.Load<SpriteFont>("Font");


            #region EditorManager
            Type myType = typeof(GameRessources);
            FieldInfo[] myField = myType.GetFields();

            EditorManager.RegisterTexture(m_EmptyButton, myField[1].Name);
            EditorManager.RegisterTexture(m_EmptyTextField, myField[2].Name);
            #endregion
        }
    }
}
