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
        private TextField m_PositionXField;
        private TextField m_PositionYField;
        private TextField m_SizeXField;
        private TextField m_SizeYField;

        private Label m_PositionXLabel;
        private Label m_PositionYLabel;
        private Label m_SizeXLabel;
        private Label m_SizeYLabel;

        private Label m_ButtonPositionXValue;
        private Label m_ButtonPositionYValue;
        private Label m_ButtonSizeXValue;
        private Label m_ButtonSizeYValue;

        private const float OFFSET_FIELD_Y = 70;
        private const float OFFSET_RECT_VALUE_X = 150f;
        private Vector2 m_OffsetField = new Vector2(300, 0);
        private Vector2 m_OffsetLabel = new Vector2(10, 5);

        public ButtonInfo(Button aButton)
        {
            m_Button = aButton;
            m_PositionXField = new TextField(
                  GameRessources.m_EmptyTextField,
                  aButton.m_Position + m_OffsetField,
                  aButton.m_GraphicsDevice);

            m_PositionXLabel = new Label("Position X ", m_PositionXField.m_Position + m_OffsetLabel);
            m_ButtonPositionXValue = new Label(aButton.m_Position.X.ToString(), m_PositionXLabel.m_Position + (Vector2.UnitX * OFFSET_RECT_VALUE_X), Color.Blue);

            m_OffsetField.Y += OFFSET_FIELD_Y;

            m_PositionYField = new TextField(
                 GameRessources.m_EmptyTextField,
                 aButton.m_Position + m_OffsetField,
                 aButton.m_GraphicsDevice);

            m_PositionYLabel = new Label("Position Y ", m_PositionYField.m_Position + m_OffsetLabel);
            m_ButtonPositionYValue = new Label(aButton.m_Position.Y.ToString(), m_PositionYLabel.m_Position + (Vector2.UnitX * OFFSET_RECT_VALUE_X), Color.Blue);

            m_OffsetField.Y += OFFSET_FIELD_Y;

            m_SizeXField = new TextField(
                 GameRessources.m_EmptyTextField,
                 aButton.m_Position + m_OffsetField,
                 aButton.m_GraphicsDevice);

            m_SizeXLabel = new Label("Size X ", m_SizeXField.m_Position + m_OffsetLabel);
            m_ButtonSizeXValue = new Label(aButton.SizeX.ToString(), m_SizeXLabel.m_Position + (Vector2.UnitX * OFFSET_RECT_VALUE_X), Color.Blue);

            m_OffsetField.Y += OFFSET_FIELD_Y;

            m_SizeYField = new TextField(
                 GameRessources.m_EmptyTextField,
                 aButton.m_Position + m_OffsetField,
                 aButton.m_GraphicsDevice);

            m_SizeYLabel = new Label("Size Y ", m_SizeYField.m_Position + m_OffsetLabel);
            m_ButtonSizeYValue = new Label(aButton.SizeY.ToString(), m_SizeYLabel.m_Position + (Vector2.UnitX * OFFSET_RECT_VALUE_X), Color.Blue);
        }

        public void Update()
        {
            m_PositionXField.Update();
            m_PositionYField.Update();
            m_SizeXField.Update();
            m_SizeYField.Update();

            m_ButtonPositionXValue.m_IsSelected = m_PositionXField.m_IsToggleActive;
            m_ButtonPositionYValue.m_IsSelected = m_PositionYField.m_IsToggleActive;
            m_ButtonSizeXValue.m_IsSelected = m_SizeXField.m_IsToggleActive;
            m_ButtonSizeYValue.m_IsSelected = m_SizeYField.m_IsToggleActive;

            m_ButtonPositionXValue.Update();
            m_ButtonPositionYValue.Update();
            m_ButtonSizeXValue.Update();
            m_ButtonSizeYValue.Update();

            m_Button.PositionX = m_ButtonPositionXValue.GetNumericValue();
            m_Button.PositionY = m_ButtonPositionYValue.GetNumericValue();
            m_Button.SizeX = m_ButtonSizeXValue.GetNumericValue();
            m_Button.SizeY = m_ButtonSizeYValue.GetNumericValue();
        }

        public void Draw(SpriteBatch aSpritebatch)
        {
            m_PositionXField.Draw(aSpritebatch);
            m_PositionYField.Draw(aSpritebatch);
            m_SizeXField.Draw(aSpritebatch);
            m_SizeYField.Draw(aSpritebatch);

            m_PositionXLabel.Draw(aSpritebatch);
            m_PositionYLabel.Draw(aSpritebatch);
            m_SizeXLabel.Draw(aSpritebatch);
            m_SizeYLabel.Draw(aSpritebatch);

            m_ButtonPositionXValue.Draw(aSpritebatch);
            m_ButtonPositionYValue.Draw(aSpritebatch);
            m_ButtonSizeXValue.Draw(aSpritebatch);
            m_ButtonSizeYValue.Draw(aSpritebatch);
        }
    }
}