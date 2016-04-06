﻿using System;
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
    public class Property
    {
        private TextField m_ValueContainer;
        private Label m_TitleLabel;
        private Label m_ValueLabel;

        private const float OFFSET_RECT_VALUE_X = 150f;
        private Vector2 m_OffsetLabel = new Vector2(10, 5);

        public Property(GraphicsDevice aGraphicDevice, Vector2 aPosition, string aTitleValue, float aInitialValue = 0)
        {
            m_ValueContainer = new TextField(
                  GameRessources.m_EmptyTextField,
                  aPosition,
                  aGraphicDevice);

            Vector2 titleAlignement = m_ValueContainer.m_Position + m_OffsetLabel;
            m_TitleLabel = new Label(aTitleValue, titleAlignement);

            Vector2 valueAlignement = m_TitleLabel.m_Position + (Vector2.UnitX * OFFSET_RECT_VALUE_X);
            m_ValueLabel = new Label(aInitialValue.ToString(), valueAlignement, Color.Blue);  
        }

        public void Update()
        {
            m_ValueContainer.Update();

            m_ValueLabel.m_IsSelected = m_ValueContainer.m_IsToggleActive;
            m_ValueLabel.Update();
        }

        public int GetLabelValue()
        {
            return m_ValueLabel.GetNumericValue();
        }

        public void Draw(SpriteBatch aSpritebatch)
        {
            m_ValueContainer.Draw(aSpritebatch);
            m_TitleLabel.Draw(aSpritebatch);
            m_ValueLabel.Draw(aSpritebatch);
        }
    }
}