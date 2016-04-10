using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TestCase
{
    public class GameRessourcesManager
    {
        private static Dictionary<Texture2D, string> m_NameDictionary = new Dictionary<Texture2D, string>();
        public static void RegisterTexture(Texture2D aTexture, string aName)
        {
            m_NameDictionary.Add(aTexture, aName);
        }

        public static string GetVariableName(Texture2D aTexture)
        {
            foreach (KeyValuePair<Texture2D, string> entry in m_NameDictionary)
            {
                if (entry.Key == aTexture)
                {
                    return entry.Value;
                }
            }

            return null;
        }
    }
}
