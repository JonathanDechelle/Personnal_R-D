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
    /// Classe servant à crée des Cases de textes
    /// </summary>
    public class TextField : Button
    {
        public TextField(Texture2D aTexture, Vector2 aPosition, GraphicsDevice aGraphicsDevice) : base(aTexture, aPosition, aGraphicsDevice)
        {
            
        }

        public override void Update(bool aIsInEditMode = false)
        {
            base.Update(true);
        }

        public override void Draw(SpriteBatch aSpritebatch)
        {
            base.Draw(aSpritebatch);
        }
    }
}