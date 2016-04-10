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
            SerializeScreen(aScreen, EditorManager.GetAllButtonInScreen(aScreen));
            DeSerializeScreen(aScreen);
        }

        private static void SerializeScreen(GameScreen aScreen, List<EditableButton> aButtons)
        {
            var serializer = new XmlSerializer(typeof(List<EditableButton>));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            using (var writer = new StreamWriter(GetScreenPath(aScreen)))
            using (var xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true }))
            {
                serializer.Serialize(xmlWriter, aButtons, ns);
            }
        }

        private static void DeSerializeScreen(GameScreen aScreen)
        {
            var serializer = new XmlSerializer(typeof(List<EditableButton>));

            List<EditableButton> screenButtons;
            using (var reader = new StreamReader(GetScreenPath(aScreen)))
                screenButtons = (List<EditableButton>)serializer.Deserialize(reader);
        }

        private static string GetScreenPath(GameScreen aScreen)
        {
            return m_ScreenRootDirectory + aScreen.GetType().Name.ToString() + ".xml";
        }
    }
}
