using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGameLibrairy
{
    /// <summary>
    /// Classe Servant a enregistrer des ecrans avec un Identifiant (utiliser pour creer qune seule fois les ecrans)
    /// </summary>
    public class GameScreenMapper
    {
        private class Entry
        {
            public Enum m_ID;
            public GameScreen m_Screen;
        }

        private static List<Entry> m_Entries = new List<Entry>();

        public static void AddEntry(Enum aID, GameScreen aScreen)
        {
            Entry entry = new Entry();
            entry.m_ID = aID;
            entry.m_Screen = aScreen;

            m_Entries.Add(entry);
        }

        public static GameScreen GetValue(Enum aID)
        {
            for(int i = 0; i < m_Entries.Count; i++)
            {
                int listID = Convert.ToInt32(m_Entries[i].m_ID);
                int IDToCheck = Convert.ToInt32(aID);

                if (listID == IDToCheck)
                {
                    return m_Entries[i].m_Screen;
                }
            }

            return null;
        }
    }
}
