using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyGameLibrairy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestCase
{
    public class EditableButton
    {
        Button m_CreateButton;
        ButtonInfo m_ButtonInfo = null;

        public EditableButton(Vector2 aPosition, GraphicsDevice aGraphicDevice)
        {
            m_CreateButton = new Button(
               GameRessources.m_EmptyButton,
               aPosition,
               aGraphicDevice);
        }

        public void Update()
        {
            //CREATE OR DELETE RESSOURCES WITH THE TOGGLE BUTTON
            if (m_CreateButton.m_IsToggleActive)
            {
                if (m_ButtonInfo == null)
                {
                    m_ButtonInfo = new ButtonInfo(m_CreateButton, GameRessources.m_SpriteFont, GameRessources.m_EmptyTextField);
                }
            }
            else
            {
                m_ButtonInfo = null;
            }

            //Update button Info
            bool hasButtonInfo = m_ButtonInfo != null;
            if (hasButtonInfo)
            {
                m_ButtonInfo.Update();
            }

            m_CreateButton.Update(hasButtonInfo);
        }

        public void Draw(SpriteBatch aSpritebatch)
        {
            m_CreateButton.Draw(aSpritebatch); 
            if (m_ButtonInfo != null)
            {
                m_ButtonInfo.Draw(aSpritebatch);
            }
        }
    }
}
