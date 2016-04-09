using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyGameLibrairy;

namespace TestCase
{
    public class EditorManager
    {
        private static List<ButtonSaver> m_ButtonSaved = new List<ButtonSaver>();

        public static void RegisterButton(EditableButton aButton, GameScreen aGameScreen)
        {
            ButtonSaver button = new ButtonSaver(aButton, aGameScreen);
            m_ButtonSaved.Add(button);
        }

        public static void UnRegisterButton(EditableButton aButton, GameScreen aGameScreen)
        {
            int index;
            if (ContainButton(aButton, aGameScreen, out index))
            {
                m_ButtonSaved.RemoveAt(index);
                aButton = null;
            }
        }

        public static bool ContainButton(EditableButton aButton, GameScreen aGameScreen, out int aIndex)
        {
            aIndex = -1;
            for(int i = 0; i < m_ButtonSaved.Count; i++)
            {
                if(m_ButtonSaved[i].m_Screen != aGameScreen)
                {
                    continue;
                }

                if(m_ButtonSaved[i].m_Button != aButton)
                {
                    continue;
                }

                aIndex = i;
                return true;
            }

            return false;
        }

        public static List<EditableButton> GetAllButtonInScreen(GameScreen aGameScreen)
        {
            List<EditableButton> buttons = new List<EditableButton>();
            for (int i = 0; i < m_ButtonSaved.Count; i++)
            {
                if (m_ButtonSaved[i].m_Screen != aGameScreen)
                {
                    continue;
                }

                buttons.Add(m_ButtonSaved[i].m_Button);
            }

            return buttons;
        }
    }

    public class ButtonSaver
    {
        public EditableButton m_Button;
        public GameScreen m_Screen;

        public ButtonSaver(EditableButton aButton, GameScreen aScreen)
        {
            m_Button = aButton;
            m_Screen = aScreen;
        }
    }
}
