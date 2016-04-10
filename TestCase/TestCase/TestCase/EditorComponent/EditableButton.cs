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
        #region Serialization
        public class SerializedData
        {
            public Vector2 Position;
            public Vector2 Size;
            public string TextureName;

            public SerializedData(){}
        }
        #endregion

        private Button m_CreateButton;
        private ButtonInfo m_ButtonInfo;
        public SerializedData ButtonData;

        public EditableButton()
        {
            ButtonData = new SerializedData();
        }

        public EditableButton(Vector2 aPosition, GraphicsDevice aGraphicDevice)
        {
            m_CreateButton = new Button(
               GameRessources.m_EmptyButton,
               aPosition,
               aGraphicDevice);

            SetUpButtonData();
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
            SetUpButtonData(false);
        }

        private void SetUpButtonData(bool aUpdateOnceTexture = true)
        {
            if (ButtonData == null)
            {
                ButtonData = new SerializedData();
            }

            ButtonData.Size = m_CreateButton.m_Size;
            ButtonData.Position = m_CreateButton.m_Position;

            if (aUpdateOnceTexture)
            {
                ButtonData.TextureName = GameRessourcesManager.GetVariableName(m_CreateButton.m_Texture);
            }
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
