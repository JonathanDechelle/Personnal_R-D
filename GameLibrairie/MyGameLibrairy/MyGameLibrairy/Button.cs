using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGameLibrairy
{
    /// <summary>
    /// Classe servant à crée des boutons autonomes
    /// </summary>
    public class Button
    {
        public bool m_IsClicked;

        private Texture2D m_Texture;
        private Vector2 m_Position;
        private Vector2 m_Size;
        private Rectangle m_ButtonRectangle;
        private Rectangle m_MouseRectangle;
        private Color m_Color = new Color(MAX_COLOR, MAX_COLOR, MAX_COLOR, MAX_COLOR);
        private byte m_ChangeColorSpeed;

        private const int PIXEL_SIZE = 1;
        private const int MAX_COLOR = 255;
        private const int MIN_COLOR = 255;

        public Button(Texture2D aTexture, Vector2 aPosition, float aResize, byte aChangeColorSpeed = 4)
        {
            this.m_Texture = aTexture;
            m_Position = aPosition;
            
            m_Size = new Vector2(
                aTexture.Width  / aResize, 
                aTexture.Height / aResize);

            m_ChangeColorSpeed = aChangeColorSpeed;

            m_MouseRectangle = new Rectangle(-100, -100, PIXEL_SIZE, PIXEL_SIZE);
            m_ButtonRectangle = new Rectangle((int)m_Position.X, (int)m_Position.Y, (int)m_Size.X, (int)m_Size.Y);
        }

        public void Update()
        {
            m_IsClicked = false;

            m_MouseRectangle.X = MouseHelper.MouseX();
            m_MouseRectangle.Y = MouseHelper.MouseY();

            if (m_MouseRectangle.Intersects(m_ButtonRectangle))
            {
                if (MouseHelper.MouseKeyPress(MouseButton.Left)) 
                {
                    m_IsClicked = true;
                }
            }

            if (m_ChangeColorSpeed > 0)
            {
                ChangeColor();
            }             
        }

        public void ChangeColor()
        {
            if (m_MouseRectangle.Intersects(m_ButtonRectangle))
            {
                m_Color.A -= m_ChangeColorSpeed;
            }
            else if (m_ChangeColorSpeed > 0 && m_Color.A < MAX_COLOR)
            {
                byte newAlpha = m_Color.A;
                newAlpha += m_ChangeColorSpeed;
                if (newAlpha < m_Color.A || newAlpha > MAX_COLOR)
                {
                    newAlpha = MAX_COLOR;
                }

                m_Color.A = newAlpha;
            }
        }

        public void SetPosition(Vector2 aPosition)
        {
            m_Position = aPosition;
        }

        public void Draw(SpriteBatch aSpritebatch)
        {
            aSpritebatch.Draw(m_Texture, m_ButtonRectangle, m_Color);
        }
    }
}