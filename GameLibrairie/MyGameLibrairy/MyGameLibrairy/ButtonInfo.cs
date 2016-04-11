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
    /// Classe servant à crée des infos sur les boutons
    /// </summary>
    public class ButtonInfo
    {
        private Button m_Button;
        private Property m_PositionXProperty;
        private Property m_PositionYProperty;
        private Property m_SizeXProperty;
        private Property m_SizeYProperty;

        private const float OFFSET_FIELD_Y = 70;
        private Vector2 m_OffsetField = new Vector2(300, 0);

        private bool[] m_ToggleState = new bool[4];
        private bool[] m_LastToggleState = new bool[4];

        public ButtonInfo(Button aButtonToUpdate,SpriteFont aFont, Texture2D aContainerTexture)
        {
            m_Button = aButtonToUpdate;

            Vector2 buttonPosition = aButtonToUpdate.m_Position + m_OffsetField;
            m_PositionXProperty = new Property(aFont, aContainerTexture, aButtonToUpdate.m_GraphicsDevice, buttonPosition, "Position X", m_Button.m_Position.X);

            buttonPosition.Y += OFFSET_FIELD_Y;
            m_PositionYProperty = new Property(aFont, aContainerTexture, aButtonToUpdate.m_GraphicsDevice, buttonPosition, "Position Y", m_Button.m_Position.Y);

            buttonPosition.Y += OFFSET_FIELD_Y;
            m_SizeXProperty = new Property(aFont, aContainerTexture, aButtonToUpdate.m_GraphicsDevice, buttonPosition, "Size X", m_Button.m_Size.X);

            buttonPosition.Y += OFFSET_FIELD_Y;
            m_SizeYProperty = new Property(aFont, aContainerTexture, aButtonToUpdate.m_GraphicsDevice, buttonPosition, "Size Y", m_Button.m_Size.Y);
        }

        public void Update()
        {
            m_PositionXProperty.Update();
            m_PositionYProperty.Update();
            m_SizeXProperty.Update();
            m_SizeYProperty.Update();

            UpdateToggles();

            m_Button.PositionX = m_PositionXProperty.GetLabelValue();
            m_Button.PositionY = m_PositionYProperty.GetLabelValue();
            m_Button.SizeX = m_SizeXProperty.GetLabelValue();
            m_Button.SizeY = m_SizeYProperty.GetLabelValue();
        }

        private void UpdateToggles()
        {
            m_ToggleState[0] = m_PositionXProperty.GetToggleActivity();
            m_ToggleState[1] = m_PositionYProperty.GetToggleActivity();
            m_ToggleState[2] = m_SizeXProperty.GetToggleActivity();
            m_ToggleState[3] = m_SizeYProperty.GetToggleActivity();

            DeactiveTheLastToggle();

            m_LastToggleState[0] = m_ToggleState[0];
            m_LastToggleState[1] = m_ToggleState[1];
            m_LastToggleState[2] = m_ToggleState[2];
            m_LastToggleState[3] = m_ToggleState[3];
        }

        private void DeactiveTheLastToggle()
        {
            int nbButtonToggle = 0;
            int indexToDeactive = - 1;
            bool hasTwoButtonToggle = false;
            
            //Get Numbers of button Toggle
            for (int i = 0; i < m_ToggleState.Length; i++)
            {
                if (m_ToggleState[i] == m_LastToggleState[i] && m_ToggleState[i] == true)
                {
                    nbButtonToggle++;
                    indexToDeactive = i;
                }              
            }

            //Has 2 button Toggle ? 
            for (int i = 0; i < m_ToggleState.Length; i++)
            {
                if (nbButtonToggle > 0 && m_ToggleState[i] != m_LastToggleState[i])
                {
                    hasTwoButtonToggle = true;
                    break;
                }
            }

            if (hasTwoButtonToggle)
            {
                //Reset Toggle
                m_ToggleState[indexToDeactive] = false;
                switch(indexToDeactive)
                {
                    case 0: m_PositionXProperty.SetToggleActive(false); break;
                    case 1: m_PositionYProperty.SetToggleActive(false); break;
                    case 2: m_SizeXProperty.SetToggleActive(false); break;
                    case 3: m_SizeYProperty.SetToggleActive(false); break;
                }
            }
        }

        public void Draw(SpriteBatch aSpritebatch)
        {
            m_PositionXProperty.Draw(aSpritebatch);
            m_PositionYProperty.Draw(aSpritebatch);
            m_SizeXProperty.Draw(aSpritebatch);
            m_SizeYProperty.Draw(aSpritebatch);
        }
    }
}