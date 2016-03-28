using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGameLibrairy
{
    /// <summary>
    /// Classe Servant a enregistrer des objects avec un Enum (utilise ca pour ne pas creer 50 000 objets)
    ///  PS : tres utile pour les ecrans (voir GameScreenMapper)
    /// </summary>
    public class EnumMapper
    {
        private class Entry
        {
            public Enum m_ID;
            public Object m_Object;
        }

        private List<Entry> m_Entries = new List<Entry>();

        public void AddEntry(Enum aID, Object aObject)
        {
            Entry entry = new Entry();
            entry.m_ID = aID;
            entry.m_Object = aObject;

            m_Entries.Add(entry);
        }

        public Object GetValue(Enum aID)
        {
            for(int i = 0; i < m_Entries.Count; i++)
            {
                if(m_Entries[i].m_ID == aID)
                {
                    return m_Entries[i];
                }
            }

            return null;
        }
    }
}
