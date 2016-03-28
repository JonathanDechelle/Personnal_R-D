using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace MyGameLibrairy
{
    public class Diapositive
    {
        public Texture2D m_Image;
        public string m_Text;
        public Color m_TextColor;
        public Color m_BackgroundColor;
        public SpriteFont m_Font;
        private string m_TexureName;

        public Diapositive(string aTextureName, string aText, Color aTextColor, Color aBackgroundColor, SpriteFont aFont)
        {
            m_TexureName = aTextureName;
            m_Text = aText;
            m_TextColor = aTextColor;
            m_BackgroundColor = aBackgroundColor;
            m_Font = aFont;
        }

        public void LoadContent(ContentManager aContent)
        {
            m_Image = aContent.Load<Texture2D>(m_TexureName);
        }
    }

    public class Cinematique
    {
        private Diapositive[] m_Diapositives;
        private Diapositive m_CurrentDiapo
        {
            get { return m_Diapositives[m_IndexDiapo]; }
        }

        private int m_IndexDiapo;
        public System.Action OnCinematicFinished;

        public Cinematique(Diapositive[] aDiapostives)
        {
            m_Diapositives = aDiapostives;
        }

        public void Play()
        {
            m_IndexDiapo = 0;
        }

        public void Update()
        {
            if (KeyboardHelper.KeyPressed(Keys.Space))
            {
                m_IndexDiapo++;
            }

            if (m_IndexDiapo > m_Diapositives.Length - 1)
            {
                if (OnCinematicFinished != null)
                {
                    OnCinematicFinished();
                }
            }
        }

        public void Draw(SpriteBatch aSpritebatch, GameTime aGameTime)
        {
            aSpritebatch.GraphicsDevice.Clear(m_CurrentDiapo.m_BackgroundColor);
            aSpritebatch.Draw(m_CurrentDiapo.m_Image, new Rectangle(0, 0, 800, 500), Color.White);
            aSpritebatch.DrawString(m_CurrentDiapo.m_Font, m_CurrentDiapo.m_Text, new Vector2(30, 30), m_CurrentDiapo.m_TextColor);
            aSpritebatch.DrawString(m_CurrentDiapo.m_Font, "Appuyer \n sur \n Espace", new Vector2(670, 200), m_CurrentDiapo.m_TextColor);
        }
    }
} 


