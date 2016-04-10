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
        public bool m_IsToggleActive;
        public bool m_IsSelected;

        public GraphicsDevice m_GraphicsDevice;

        private Texture2D m_OriginalTexture;
        private Texture2D m_SelectedTexture;
        private Rectangle m_ButtonRectangle;
        private Rectangle m_MouseRectangle;
        private Color m_Color = new Color(MAX_COLOR, MAX_COLOR, MAX_COLOR, MAX_COLOR);

        private const int PIXEL_SIZE = 1;
        private const int MAX_COLOR = 255;
        private const int MIN_COLOR = 255;
        private const int CONTOUR_SIZE = 3;

        #region get set
        public Texture2D m_Texture
        {
            get
            {
                return m_OriginalTexture;
            }
        }

        public Vector2 m_Position
        {
            get
            {
                return new Vector2(
                    PositionX,
                    PositionY
                    );
            }
        }

        public Vector2 m_Size
        {
            get
            {
                return new Vector2(
                    SizeX,
                    SizeY
                    );
            }
        }

        public int PositionX
        {
            get
            {
                return m_ButtonRectangle.X;
            }
            set
            {
                m_ButtonRectangle.X = value;
            }
        }

        public int PositionY
        {
            get
            {
                return m_ButtonRectangle.Y;
            }
            set
            {
                m_ButtonRectangle.Y = value;
            }
        }

        public int SizeX
        {
            get
            {
                return m_ButtonRectangle.Width;
            }
            set
            {
                m_ButtonRectangle.Width = value;
            }
        }

        public int SizeY
        {
            get
            {
                return m_ButtonRectangle.Height;
            }
            set
            {
                m_ButtonRectangle.Height = value;
            }
        }
        #endregion

        public Button(Texture2D aTexture, Vector2 aPosition, GraphicsDevice aGraphicsDevice)
        {
            this.m_OriginalTexture = aTexture;

            m_MouseRectangle = new Rectangle(-100, -100, PIXEL_SIZE, PIXEL_SIZE);
            m_ButtonRectangle = new Rectangle((int)aPosition.X, (int)aPosition.Y, (int)aTexture.Width, (int)aTexture.Height);

            m_GraphicsDevice = aGraphicsDevice;
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

        public virtual void Update(bool aIsInEditMode = false)
        {
            m_IsClicked = false;

            m_MouseRectangle.X = MouseHelper.MouseX();
            m_MouseRectangle.Y = MouseHelper.MouseY();

            bool mouseOnButton = m_MouseRectangle.Intersects(m_ButtonRectangle);
            m_IsSelected = aIsInEditMode ?
                mouseOnButton || m_IsToggleActive:
                mouseOnButton;

            if (mouseOnButton)
            {
                ToggleIfMouseLeftClicked();
            }
        }

        private void ToggleIfMouseLeftClicked()
        {
            if (MouseHelper.MouseKeyPress(MouseButton.Left))
            {
                m_IsClicked = true;
                m_IsToggleActive = !m_IsToggleActive;
            }
        }

        public virtual void Draw(SpriteBatch aSpritebatch)
        {
            Texture2D currentTexture = m_IsSelected ?
            m_SelectedTexture :
            m_OriginalTexture ;

            aSpritebatch.Draw(currentTexture, m_ButtonRectangle, m_Color);
        }
    }
}