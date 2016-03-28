using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace MyGameLibrairy
{
    /// <summary>
    /// Créateur de particules
    /// </summary>
    public  class ParticleGenerator
    {
        private Texture2D m_Texture;
        private float m_SpawnWidth;
        private float m_Density;
        private float m_Timer;
        private Random m_Rand1, m_Rand2;
        private List<RainDrop> m_Raindrops = new List<RainDrop>();

        private const float START_HEIGHT = -50f;
        private const float MIN_SPEED = 1f;

        public ParticleGenerator(Texture2D aTexture, float aSpawnWidth, float aDensity)
        {
            m_Texture = aTexture;
            m_SpawnWidth = aSpawnWidth;
            m_Density = aDensity;

            m_Rand1 = new Random();
            m_Rand2 = new Random();
        }

        public void CreateParticle()
        {
            m_Raindrops.Add(new RainDrop(
                m_Texture,
                new Vector2(START_HEIGHT + (float)m_Rand1.NextDouble() * m_SpawnWidth, 0),
                new Vector2(MIN_SPEED, m_Rand2.Next(5, 8))));                                 
        }

        public void Update(GameTime aGametime, GraphicsDevice graphics)
        {
            m_Timer += (float)aGametime.ElapsedGameTime.TotalSeconds;

            while (m_Timer > 0)
            {
                m_Timer -= 1f / m_Density;
                CreateParticle();
            }

            for (int i = 0; i < m_Raindrops.Count; i++)
            {
                m_Raindrops[i].Update();
                if (m_Raindrops[i].Position.Y > graphics.Viewport.Height)
                {
                    m_Raindrops.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch g)
        {
            foreach (RainDrop raindrop in m_Raindrops)
                raindrop.Draw(g);
        }
    }
}
