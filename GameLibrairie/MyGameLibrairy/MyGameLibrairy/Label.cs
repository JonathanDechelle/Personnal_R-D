using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGameLibrairy;

namespace MyGameLibrairy
{
    /// <summary>
    /// Classe servant à crée labels
    /// </summary>
    /// 
    public class Label
    {
        private string m_Text;
        private Color m_Color;
        private Color m_InvertedColor;
        
        public Vector2 m_Position;
        public bool m_IsSelected;
        private bool m_IsNumericLabel = true;
        private SpriteFont m_Font;

        public Label(string aText, Vector2 aPosition, SpriteFont aFont)
        {
            m_Text = aText;
            m_Position = aPosition;
            m_Color = Color.Black;
            m_Font = aFont;
            m_InvertedColor = Color.White;
        }

        public Label(string aText, Vector2 aPosition, SpriteFont aFont, Color aColor)
            :this(aText,aPosition, aFont)
        {
            m_Color = aColor;
            m_InvertedColor = new Color(
                255 - m_Color.R,
                m_Color.G,
                255 - m_Color.B,
                255);
        }

        public void Update()
        {
            if (m_IsSelected)
            {
                Keys[] keys = KeyboardHelper.KeyPressed();
                string newKey;
                int number;
                bool result;
                for (int i = 0; i < keys.Length; i++)
                {
                    newKey = keys[i].ToString();
                    newKey = newKey.Replace("D", "");
                    result = Int32.TryParse(newKey,out number);
                    if (result)
                    {
                        m_Text += newKey;
                    }
                    else if (newKey == "Back" && m_Text.Length > 0)
                    {
                       m_Text = m_Text.Remove(m_Text.Length - 1, 1);
                    }
                }
            }
        }

        public int GetNumericValue()
        {
            int number;
            try
            {
                number = Convert.ToInt32(m_Text);
                m_Text = number.ToString();
                return number;
            }
            catch
            {
                number = 0;
                m_Text = number.ToString();
                return number;
            }
        }

        public void Draw(SpriteBatch aSpriteBatch)
        {
            Color aColor = m_IsSelected ?
            m_InvertedColor :
            m_Color;

            aSpriteBatch.DrawString(m_Font, m_Text, m_Position, aColor);
        }
    }
}