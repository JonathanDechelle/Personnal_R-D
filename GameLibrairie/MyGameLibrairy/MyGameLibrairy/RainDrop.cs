using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyGameLibrairy
{
    /// <summary>
    /// Classe créant la pluie
    /// </summary>
    public class RainDrop
    {
        Texture2D m_Texture;
        Vector2 m_Position;
        Vector2 m_Speed;

        public Vector2 Position
        {
            get { return m_Position; }
        }

        public RainDrop(Texture2D aTexture, Vector2 aPosition, Vector2 aSpeed)
        {
            m_Texture = aTexture;
            m_Position = aPosition;
            m_Speed = aSpeed;
        }

        public void Update()
        {
            m_Position += m_Speed;
        }

        public void Draw(SpriteBatch aSpritebatch)
        {
            aSpritebatch.Draw(m_Texture, m_Position, Color.White);
        }
    }
}
