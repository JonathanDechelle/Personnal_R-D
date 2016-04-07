using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Content;

namespace MyGameLibrairy
{
    public class ScreenSaver
    {
        private static string m_RootDirectory;

        public static void SetRootDirectory(ContentManager aContent)
        {
            m_RootDirectory = aContent.RootDirectory;
        }

        public static void SaveScreenContent()
        {

        }
    }
}
