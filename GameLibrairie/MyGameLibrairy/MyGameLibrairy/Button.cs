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
        public bool m_IsHold;
        public bool m_IsSelected;

        public enum ButtonSection
        {
            Top,
            Bottom,
            Left,
            Right
        }

        private ButtonSection m_ButtonSectionClicked;
        private Texture2D m_OriginalTexture;
        private Texture2D m_SelectedTexture;
        private Vector2 m_FinalPosition;
        private Vector2 m_StartPosition;
        private Vector2 m_FirstMousePosition;
        private Vector2 m_MouseOffset;
        private Vector2 m_StartSize;
        private Vector2 m_FinalSize;
        private Rectangle m_ButtonRectangle;
        private Rectangle m_MouseRectangle;
        private Color m_Color = new Color(MAX_COLOR, MAX_COLOR, MAX_COLOR, MAX_COLOR);
        private byte m_ChangeColorSpeed;

        private const int PIXEL_SIZE = 1;
        private const int MAX_COLOR = 255;
        private const int MIN_COLOR = 255;
        private const int CONTOUR_SIZE = 3;

        private const float CENTER = 0.5f;
        
        public Button(Texture2D aTexture, Vector2 aPosition, GraphicsDevice aGraphicsDevice)
        {
            this.m_OriginalTexture = aTexture;
            m_StartPosition = aPosition;
            m_FinalPosition = m_StartPosition;

            m_ChangeColorSpeed = 0; /* useless for now*/

            m_MouseRectangle = new Rectangle(-100, -100, PIXEL_SIZE, PIXEL_SIZE);
            m_ButtonRectangle = new Rectangle((int)m_FinalPosition.X, (int)m_FinalPosition.Y, (int)aTexture.Width, (int)aTexture.Height);

            m_StartSize = new Vector2(m_OriginalTexture.Width, m_OriginalTexture.Height);
            m_FinalSize = m_StartSize;
            BuildSelectedTexture(aGraphicsDevice);
        }

        private void BuildSelectedTexture(GraphicsDevice aGraphicsDevice)
        {
            int width = m_OriginalTexture.Width;
            int height = m_OriginalTexture.Height;
        
            Color[] dataColors = new Color[width * height];
            Color[] originalColors = new Color[width * height];
            m_OriginalTexture.GetData<Color>(originalColors);
            
			//Set Contour In Opposite Color
            for (int i = 0; i < dataColors.Count(); i++)
            {
                if (i           < (width * CONTOUR_SIZE)                        ||  //Haut
                    i           > (width * height) - (width * CONTOUR_SIZE)     ||  //Bas
                    i % width   < CONTOUR_SIZE                                  ||  //Gauche
                    i % width   > width - CONTOUR_SIZE)                             //Droite
                {
                    dataColors[i].A = originalColors[i].A;

                    byte colorR = Convert.ToByte(Math.Abs(originalColors[i].R - MAX_COLOR));
                    byte colorG = Convert.ToByte(Math.Abs(originalColors[i].G - MAX_COLOR));
                    byte colorB = Convert.ToByte(Math.Abs(originalColors[i].B - MAX_COLOR));

                    dataColors[i].R = colorR;
                    dataColors[i].G = colorG;
                    dataColors[i].B = colorB;
                }
                else
                {
                    dataColors[i] = originalColors[i];
                }
            }

            m_SelectedTexture = new Texture2D(aGraphicsDevice, m_OriginalTexture.Width, m_OriginalTexture.Height);
            m_SelectedTexture.SetData<Color>(dataColors);
        }

        public void Update()
        {
            m_IsClicked = false;
            m_IsHold = false;

            m_MouseRectangle.X = MouseHelper.MouseX();
            m_MouseRectangle.Y = MouseHelper.MouseY();

            m_ButtonRectangle.X = (int)(m_StartPosition.X + m_MouseOffset.X);
            m_ButtonRectangle.Y = (int)(m_StartPosition.Y + m_MouseOffset.Y);
            m_ButtonRectangle.Width  = (int)m_FinalSize.X;
            m_ButtonRectangle.Height = (int)m_FinalSize.Y;

            m_IsSelected = m_MouseRectangle.Intersects(m_ButtonRectangle);
            if (m_IsSelected)
            {
                if (MouseHelper.MouseKeyPress(MouseButton.Left)) 
                {
                    m_IsClicked = true;
                    m_FirstMousePosition = MouseHelper.MousePosition() - m_MouseOffset;
                }

                //Move the sprite
                if (MouseHelper.MouseKeyHold(MouseButton.Left))
                {
                    m_IsHold = true;
                    m_MouseOffset =  MouseHelper.MousePosition() - m_FirstMousePosition; // get the center of sprite
                }
                else
                {
                    m_FinalPosition.X = m_ButtonRectangle.X;
                    m_FinalPosition.Y = m_ButtonRectangle.Y;
                }

                /* Resize Sprite */
                if (MouseHelper.MouseKeyPress(MouseButton.Right))
                {
                    m_ButtonSectionClicked = GetMoreClosestSide();
                }

                if (MouseHelper.MouseKeyHold(MouseButton.Right))
                {
                    ResizeRectangle();
                }
                else
                {
                    m_FinalSize.Y = m_ButtonRectangle.Height;
                }
            }


            #region To DElete
            /*
            if (m_ChangeColorSpeed > 0)
            {
                //ChangeColor();
            }    */
            #endregion
        }

        #region To Delete
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
        #endregion

        private ButtonSection GetMoreClosestSide()
        {
            if (GetMousePositionHeightRatio() > 0.5f)
            {
                return ButtonSection.Bottom;
            }
            else
            {
                return ButtonSection.Top;
            }
        }

        private float GetMousePositionHeightRatio()
        {
            return GetMouseCenterVector().Y / m_FinalSize.Y;
        }

        private float GetMousePositionWidthRatio()
        {
            return GetMouseCenterVector().X / m_FinalSize.X;
        }

        private Vector2 GetMouseCenterVector()
        {
            return(new Vector2(MouseHelper.MouseX() - m_FinalPosition.X, MouseHelper.MouseY() - m_FinalPosition.Y));
        }

        private void ResizeRectangle()
        {
            int height = m_ButtonRectangle.Height;
            float posY = m_MouseOffset.Y;

            switch (m_ButtonSectionClicked)
            {
                case ButtonSection.Bottom:
                    height = (int)(m_FinalSize.Y * (GetMousePositionHeightRatio() / CENTER));
                    
                    break;
                case ButtonSection.Top:
                    posY = MouseHelper.MousePosition().Y - (m_StartPosition.Y / CENTER);
                    height = (int)(m_FinalSize.Y * (GetMousePositionHeightRatio() / CENTER) + m_StartSize.Y);

                    break;
            }

            m_FinalSize.Y = height;
            m_MouseOffset.Y = posY;
        }

        public override string ToString()
        {
            return
            "RECTANGLE BUTTON " + m_ButtonRectangle.ToString() + "\n" +
            "FINAL POSITION " + m_FinalPosition + "\n" +
            "OFFSET MOUSE " + m_MouseOffset + "\n" +
            "FINAL SIZE " + m_FinalSize + "\n"

            + MouseHelper.ToText();
        }

        public void Draw(SpriteBatch aSpritebatch)
        {
            Texture2D currentTexture = m_IsSelected ?
            m_SelectedTexture :
            m_OriginalTexture ;

            aSpritebatch.Draw(currentTexture, m_ButtonRectangle, m_Color);
        }
    }
}