using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyGameLibrairy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestCase
{
    class IntroScreen : GameScreen
    {
        public IntroScreen(IServiceProvider aServiceProvider, GraphicsDeviceManager aGraphics)
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
            aSpriteBatch.GraphicsDevice.Clear(Color.Blue);
        }
    }
}
