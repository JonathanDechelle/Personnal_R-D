using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MyGameLibrairy
{
    /// <summary>
    /// Création d'objet collisionable
    /// </summary>
    public class ObjCollisionable
    {
        public Rectangle m_DimObj;
        public bool m_Loop;
        public float m_Rotation;

        private Texture2D m_Texture;
        private Color m_Color;

        //CONSTRUCTOR
        public ObjCollisionable(int x, int y, int SizeX, int SizeY, Color Color)
        {
            m_DimObj = new Rectangle(x, y, SizeX, SizeY);
            m_Color = Color;
        }

        public ObjCollisionable(int x, int y, int SizeX, int SizeY, bool Loop)
        {
            m_DimObj = new Rectangle(x, y, SizeX, SizeY);
            m_Loop = Loop;
        }

        public ObjCollisionable(int x, int y, int SizeX, int SizeY, bool Loop, float Rotation)
        {
            m_DimObj = new Rectangle(x, y, SizeX, SizeY);
            m_Loop = Loop;
            m_Rotation = Rotation;
        }

        public ObjCollisionable(int x, int y, Texture2D Texture, int SizeX, int SizeY, Color Color)
        {
            m_Texture = Texture;
            m_DimObj = new Rectangle(x, y, SizeX, SizeY);
            m_Color = Color;
        }

        public void Draw(SpriteBatch g)
        {
            g.Draw(m_Texture, m_DimObj, m_Color);
        }
    }
}

