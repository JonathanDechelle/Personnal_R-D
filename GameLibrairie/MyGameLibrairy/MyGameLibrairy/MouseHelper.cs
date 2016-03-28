using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyGameLibrairy
{
    /// <summary>
    /// Classe associer aux controles de la souris
    /// </summary>
    public class MouseHelper
    {
        public static MouseState m_PlayerState;
        public static MouseState m_LastPlayerState;

        public static bool MouseKeyPress(MouseButton aButton)
        {
            ButtonState buttonState = GetMouseButtonState(aButton, m_PlayerState);
            ButtonState lastButtonState = GetMouseButtonState(aButton, m_LastPlayerState);

            return buttonState == ButtonState.Pressed && lastButtonState == ButtonState.Released;
        }

        public static bool MouseKeyHold(MouseButton aButton)
        {
            ButtonState lastButtonState = GetMouseButtonState(aButton, m_LastPlayerState);
            return lastButtonState == ButtonState.Pressed;
        }

        private static ButtonState GetMouseButtonState(MouseButton aMouseButton, MouseState aMouseState)
        {
            switch (aMouseButton)
            {
                case MouseButton.Left:
                    return aMouseState.LeftButton;
                    break;
                case MouseButton.Middle:
                    return aMouseState.MiddleButton;
                    break;
                case MouseButton.Right:
                    return aMouseState.RightButton;
                    break;
                default: return ButtonState.Released;
            }
        }
    }

    public enum MouseButton
    {
        Left,
        Middle,
        Right,
        Count,
    }
}
