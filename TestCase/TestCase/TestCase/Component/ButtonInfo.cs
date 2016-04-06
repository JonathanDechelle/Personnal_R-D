using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGameLibrairy;

namespace TestCase
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

        public ButtonInfo(Button aButton)
        {
            m_Button = aButton;

            Vector2 buttonPosition = aButton.m_Position + m_OffsetField;
            m_PositionXProperty = new Property(aButton.m_GraphicsDevice, buttonPosition, "Position X", m_Button.m_Position.X);

            buttonPosition.Y += OFFSET_FIELD_Y;
            m_PositionYProperty = new Property(aButton.m_GraphicsDevice, buttonPosition, "Position Y", m_Button.m_Position.Y);

            buttonPosition.Y += OFFSET_FIELD_Y;
            m_SizeXProperty = new Property(aButton.m_GraphicsDevice, buttonPosition, "Size X", m_Button.m_Size.X);

            buttonPosition.Y += OFFSET_FIELD_Y;
            m_SizeYProperty = new Property(aButton.m_GraphicsDevice, buttonPosition, "Size Y", m_Button.m_Size.Y);
        }

        public void Update()
        {
            m_PositionXProperty.Update();
            m_PositionYProperty.Update();
            m_SizeXProperty.Update();
            m_SizeYProperty.Update();

            m_Button.PositionX = m_PositionXProperty.GetLabelValue();
            m_Button.PositionY = m_PositionYProperty.GetLabelValue();
            m_Button.SizeX = m_SizeXProperty.GetLabelValue();
            m_Button.SizeY = m_SizeYProperty.GetLabelValue();
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