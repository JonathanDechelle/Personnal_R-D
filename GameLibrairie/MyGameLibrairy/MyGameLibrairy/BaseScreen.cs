using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGameLibrairy
{
    class BaseScreen : GameScreen
    {
        /// <summary>
        /// Ecran de base !! copier coller =) !!  
        /// </summary>
        /// <param name="aServiceProvider"></param>
        /// <param name="aGraphics"></param>
        public BaseScreen(IServiceProvider aServiceProvider, GraphicsDeviceManager aGraphics)
            : base(aServiceProvider, aGraphics)
        {

        }

        public override void Load(GraphicsDevice aGraphicDevice = null)
        {
          
        }

        public override void Update(GameTime aGameTime)
        {
            base.Update(aGameTime);
        }

        public override void Draw(GameTime aGameTime, SpriteBatch aSpriteBatch)
        {
            base.Draw(aGameTime, aSpriteBatch);
            aSpriteBatch.GraphicsDevice.Clear(Color.White);
        }
    }
}
