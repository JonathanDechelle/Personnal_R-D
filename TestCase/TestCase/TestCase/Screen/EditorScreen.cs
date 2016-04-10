using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyGameLibrairy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestCase
{
    public class EditorScreen : GameScreen
    {
        public EditableButton m_EditableButton;
        public EditorScreen(IServiceProvider aServiceProvider, GraphicsDeviceManager aGraphics)
            : base(aServiceProvider, aGraphics)
        {
        }

        public override void  Load(GraphicsDevice aGraphicDevice)
        {
            Vector2 editableButtonPosition = new Vector2(100, 100);
            m_EditableButton = new EditableButton(editableButtonPosition, aGraphicDevice);

            EditorManager.RegisterButton(m_EditableButton, this);
        }

        public override void Update(GameTime aGameTime)
        {
            m_EditableButton.Update();
            base.Update(aGameTime);
        }

        public override void Draw(GameTime aGameTime, SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Begin();
            base.Draw(aGameTime, aSpriteBatch);
            aSpriteBatch.GraphicsDevice.Clear(Color.CornflowerBlue);
            m_EditableButton.Draw(aSpriteBatch);
            aSpriteBatch.End();
        }
    }
}
