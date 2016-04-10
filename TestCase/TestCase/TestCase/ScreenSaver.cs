using System.Reflection;
using MyGameLibrairy;
using Microsoft.Xna.Framework.Content;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

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
                SerializeScreen(aScreen, buttons[i]);
                /*
                FieldInfo[] fields = buttons[i].GetType().GetFields(m_BindingFlags);
                for (int j = 0; j < fields.Length; j++)
                {
                    Text += GetFieldNameAndValue(fields[j], buttons[i]);
                }*/
            }
        }

        private static void SerializeScreen(GameScreen aScreen, EditableButton aButton)
        {
            var serializer = new XmlSerializer(typeof(EditableButton));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            using (var writer = new StreamWriter(m_ScreenRootDirectory + aScreen.GetType().Name.ToString() + ".xml"))
            using (var xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true }))
            {
                serializer.Serialize(xmlWriter, aButton, ns);
            }
        }

        private static string GetFieldNameAndValue(FieldInfo aField, object aObj)
        {
            return aField.Name + " " + aField.GetValue(aObj) + "\r\n";
        }
    }
}
