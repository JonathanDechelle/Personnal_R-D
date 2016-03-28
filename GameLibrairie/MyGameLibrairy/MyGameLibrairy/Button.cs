﻿using System;
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
        public bool m_IsSelected;

        private Texture2D m_OriginalTexture;
        private Texture2D m_SelectedTexture;
        private Vector2 m_Position;
        private Rectangle m_ButtonRectangle;
        private Rectangle m_MouseRectangle;
        private Color m_Color = new Color(MAX_COLOR, MAX_COLOR, MAX_COLOR, MAX_COLOR);
        private byte m_ChangeColorSpeed;

        private const int PIXEL_SIZE = 1;
        private const int MAX_COLOR = 255;
        private const int MIN_COLOR = 255;
        private const int CONTOUR_SIZE = 3;
        
        public Button(Texture2D aTexture, Vector2 aPosition, GraphicsDevice aGraphicsDevice)
        {
            this.m_OriginalTexture = aTexture;
            m_Position = aPosition;
            m_ChangeColorSpeed = 0; /* useless for now*/

            m_MouseRectangle = new Rectangle(-100, -100, PIXEL_SIZE, PIXEL_SIZE);
            m_ButtonRectangle = new Rectangle((int)m_Position.X, (int)m_Position.Y, (int)aTexture.Width, (int)aTexture.Height);

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

            m_MouseRectangle.X = MouseHelper.MouseX();
            m_MouseRectangle.Y = MouseHelper.MouseY();

            m_IsSelected = m_MouseRectangle.Intersects(m_ButtonRectangle);
            if (m_IsSelected)
            {
                if (MouseHelper.MouseKeyPress(MouseButton.Left)) 
                {
                    m_IsClicked = true;
                }
            }
            

            if (m_ChangeColorSpeed > 0)
            {
                //ChangeColor();
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
            Texture2D currentTexture = m_IsSelected ?
            m_SelectedTexture :
            m_OriginalTexture ;

            aSpritebatch.Draw(currentTexture, m_ButtonRectangle, m_Color);
        }
    }
}