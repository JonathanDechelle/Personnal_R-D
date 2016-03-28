using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGameLibrairy
{
    /// <summary>
    /// Classe servant a Crée des balles graphiquement
    /// </summary>
    public class Bullets
    {
        public Texture2D m_Texture;
        public SpriteEffects m_SpriteEffect;
        public Vector2 m_Position;
        public Vector2 m_Speed;
        public Vector2 m_Origin;
        public Rectangle m_Rectangle;
        public bool m_IsVisible;

        public Bullets(Texture2D aTexture)
        {
            m_Texture = aTexture;
            m_IsVisible = false;
        }

        public void Draw(SpriteBatch aSpritebatch)
        {
            aSpritebatch.Draw(m_Texture, m_Position, null, Color.White, 0f, m_Origin, 1f, m_SpriteEffect, 0);
        }
    }
}
