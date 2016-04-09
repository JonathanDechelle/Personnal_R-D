using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Content;
using System.Reflection;
using MyGameLibrairy;

namespace TestCase
{
    public class ScreenSaver
    {
        private static string m_ScreenRootDirectory;
        private static BindingFlags m_BindingFlags =
                          BindingFlags.Public |
                          BindingFlags.NonPublic |
                          BindingFlags.Instance |
                          BindingFlags.Static |
                          BindingFlags.DeclaredOnly;

        public static void SetRootDirectory(ContentManager aContent)
        {
            m_ScreenRootDirectory = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\x86\\Debug\\", "");
            m_ScreenRootDirectory += "Screen\\";
        }

        public static void SaveScreenContent(GameScreen aScreen)
        {
            List<EditableButton> buttons = EditorManager.GetAllButtonInScreen(aScreen);

            string Text = "";
            for (int i = 0; i < buttons.Count; i++)
            {
                FieldInfo[] fields = buttons[i].GetType().GetFields(m_BindingFlags);
                for (int j = 0; j < fields.Length; j++)
                {
                    Text += GetFieldNameAndValue(fields[j], buttons[i]);
                }
            }

            File.WriteAllText(m_ScreenRootDirectory + "Testallo.txt", Text);
        }

        private static string GetFieldNameAndValue(FieldInfo aField, object aObj)
        {
            return aField.Name + " " + aField.GetValue(aObj) + "\r\n";
        }
    }
}
