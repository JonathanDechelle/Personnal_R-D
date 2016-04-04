using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGameLibrairy;

namespace TestCase
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

        public Label(string aText, Vector2 aPosition)
        {
            m_Text = aText;
            m_Position = aPosition;
            m_Color = Color.Black;
            m_InvertedColor = Color.White;
        }

        public Label(string aText, Vector2 aPosition, Color aColor)
            :this(aText,aPosition)
        {
            m_Color = aColor;
            m_InvertedColor = new Color(
                255 - m_Color.R,
                m_Color.G,
                255 - m_Color.B,
                255);
        }

        public void Draw(SpriteBatch aSpriteBatch)
        {
            Color aColor = m_IsSelected ?
            m_InvertedColor :
            m_Color;

            aSpriteBatch.DrawString(GameRessources.m_SpriteFont, m_Text, m_Position, aColor);
        }
    }
}