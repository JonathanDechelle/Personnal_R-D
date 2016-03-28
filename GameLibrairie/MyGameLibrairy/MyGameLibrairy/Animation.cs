using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGameLibrairy
{
    /// <summary>
    /// classe servant a créer une action en animation
    /// </summary>
    public class Animation
    {
        public Texture2D m_Texture;
        public int m_FrameWidth;
        public int m_FrameHeight;
        public int m_FrameCount;
        public float m_FrameTime;
        public float m_Resize;
        public bool m_IsLooping;

        public Animation(Texture2D aTexture, int aFrameWidth, float aFrameTime, float aResize, bool aIsLooping)
        {
            m_Texture = aTexture;
            m_FrameWidth = aFrameWidth;
            m_FrameHeight = m_Texture.Height;
            m_FrameTime = aFrameTime;
            m_Resize = aResize;
            m_IsLooping = aIsLooping;
            m_FrameCount = aTexture.Width / this.m_FrameWidth;
        }
    }
}
