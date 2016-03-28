using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGameLibrairy
{
    /// <summary>
    /// Classe servant a lire les animations 
    /// </summary>
    public struct AnimationPlayer
    {
        public float m_Rotation;
        public int m_FrameIndex;
        public Animation m_Animation;

        private float m_Timer;
        private Rectangle m_CurrentFrame;
        private Rectangle m_CurrentResizedFrame;

        public Vector2 Origin
        {
            get { return new Vector2(m_Animation.m_FrameWidth / 2, m_Animation.m_FrameHeight); }
        }

        public void PlayAnimation(Animation aAnimation)
        {
            if (m_Animation == aAnimation)
                return;

            m_Animation = aAnimation;
            m_FrameIndex = 0;
            m_Timer = 0;
        }

        public void ComputeFrameInformation(GameTime aGametime, SpriteBatch aSpritebatch, Vector2 aPosition)
        {
            GetCurrentFrame(aGametime);
            m_CurrentResizedFrame = new Rectangle(
                (int)aPosition.X,
                (int)aPosition.Y,
                (int)(m_Animation.m_FrameWidth * m_Animation.m_Resize),
                (int)(m_Animation.m_FrameHeight * m_Animation.m_Resize));
        }

        public void ComputeFrameInformation(GameTime aGametime, SpriteBatch aSpritebatch, Rectangle aRectangle)
        {
            GetCurrentFrame(aGametime);
            m_CurrentResizedFrame = new Rectangle(
                aRectangle.X,
                aRectangle.Y,
                (int)m_Animation.m_FrameWidth * (aRectangle.Height / m_Animation.m_FrameWidth),
                (int)m_Animation.m_FrameWidth * (aRectangle.Height / m_Animation.m_FrameWidth));
        }

        private void GetCurrentFrame(GameTime aGametime)
        {
            m_Timer += (float)aGametime.ElapsedGameTime.TotalSeconds;
            while (m_Timer >= m_Animation.m_FrameTime)
            {
                m_Timer -= m_Animation.m_FrameTime;
                if (m_Animation.m_IsLooping)
                    try
                    {
                        m_FrameIndex = (m_FrameIndex + 1) % m_Animation.m_FrameCount;
                    }
                    catch
                    {
                        m_FrameIndex = 0;
                    }
                else m_FrameIndex = Math.Min(m_FrameIndex + 1, m_Animation.m_FrameCount - 1);
            }

            m_CurrentFrame = new Rectangle(
                m_FrameIndex * m_Animation.m_FrameWidth,
                0,
                m_Animation.m_FrameWidth,
                m_Animation.m_FrameHeight);
        }

        public void Draw(GameTime aGametime, SpriteBatch aSpritebatch, Vector2 aPosition, SpriteEffects aSpriteEffet)
        {
            if (m_Animation != null)
            {
                ComputeFrameInformation(aGametime, aSpritebatch, aPosition);
                aSpritebatch.Draw(m_Animation.m_Texture, m_CurrentResizedFrame, m_CurrentFrame, Color.White, m_Rotation, Origin, aSpriteEffet, 0);
            }
        }

        public void Draw(GameTime aGametime, SpriteBatch aSpriteBatch, Vector2 aPosition, SpriteEffects aSpriteEffet, Color aColor)
        {
            if (m_Animation != null)
            {
                ComputeFrameInformation(aGametime, aSpriteBatch, aPosition);
                aSpriteBatch.Draw(m_Animation.m_Texture, m_CurrentResizedFrame, m_CurrentFrame, aColor, m_Rotation, Origin, aSpriteEffet, 0);
            }
        }

        public void Draw(GameTime aGametime, SpriteBatch aSpriteBatch, Vector2 aPosition, SpriteEffects aSpriteEffet, float aRotation)
        {
            if (m_Animation != null)
            {
                ComputeFrameInformation(aGametime, aSpriteBatch, aPosition);
                aSpriteBatch.Draw(m_Animation.m_Texture, m_CurrentResizedFrame, m_CurrentFrame, Color.White, aRotation, Origin, aSpriteEffet, 0);
            }
        }

        public void Draw(GameTime aGametime, SpriteBatch aSpritebatch, Rectangle aRecPosition, SpriteEffects aSpriteEffet)
        {
            if (m_Animation != null)
            {
                ComputeFrameInformation(aGametime, aSpritebatch, aRecPosition);
                aSpritebatch.Draw(m_Animation.m_Texture, m_CurrentResizedFrame, m_CurrentFrame, Color.White, m_Rotation, Origin, aSpriteEffet, 0);
            }
        }
    }
}

