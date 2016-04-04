using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestCase
{
    public enum EChallengeRarety
    {
        Gold,
        Silver,
        Bronze,
        Common
    }

    public enum EPowerColor
    {
        Red,
        Blue,
        Yellow,
        Green,
        White,
        None,
    }

    public class ChallengeManager
    {
        private ChallengeInventory m_Inventory;
        public ChallengeInventory Inventory
        {
            get
            {
                return m_Inventory;
            }
            set
            {
                m_Inventory = value;
            }
        }

        public ChallengeManager()
        {
            m_Inventory = new ChallengeInventory();
        }
    }

    public class ChallengeInventoryItem
    {
        private EChallengeRarety m_ChallengeRarety = EChallengeRarety.Common;
        public int m_Values;

        public ChallengeInventoryItem(EChallengeRarety aChallengeRarety, int aValues)
        {
            m_ChallengeRarety = aChallengeRarety;
            m_Values = aValues;
        }
    }

    public class ChallengeInventory
    {
        private int m_NbRedStuds = 0;
        private int m_NbBlueStuds = 0;
        private int m_NbYellowStuds = 0;
        private int m_NbGreenStuds = 0;
        private int m_NbWhiteStuds = 0;

        private int m_NbGoldStuds = 0;
        private int m_NbSilverStuds = 0;
        private int m_NbBronzeStuds = 0;

        private ChallengeInventoryItem GOLD_VALUE = new ChallengeInventoryItem(EChallengeRarety.Gold, 50);
        private ChallengeInventoryItem SILVER_VALUE = new ChallengeInventoryItem(EChallengeRarety.Silver, 25);
        private ChallengeInventoryItem BRONZE_VALUE = new ChallengeInventoryItem(EChallengeRarety.Bronze, 10);
        private ChallengeInventoryItem COMMON_VALUE = new ChallengeInventoryItem(EChallengeRarety.Common, 1);
        private bool m_HasAlreadyCountTotalStuds = false;
        private int m_TotalStuds = 0;

        public ChallengeInventory() { }

        public void AddStuds(EPowerColor aColor, int aQuantity)
        {
            switch (aColor)
            {
                case EPowerColor.Red: m_NbRedStuds += aQuantity; break;
                case EPowerColor.Blue: m_NbBlueStuds += aQuantity; break;
                case EPowerColor.Yellow: m_NbYellowStuds += aQuantity; break;
                case EPowerColor.Green: m_NbGreenStuds += aQuantity; break;
                case EPowerColor.White: m_NbWhiteStuds += aQuantity; break;
            }

            m_HasAlreadyCountTotalStuds = false;
            RebuildInventory();
        }

        public void AddStuds(EChallengeRarety aRarety, int aQuantity)
        {
            switch (aRarety)
            {
                case EChallengeRarety.Gold: m_NbGoldStuds += aQuantity; break;
                case EChallengeRarety.Silver: m_NbSilverStuds += aQuantity; break;
                case EChallengeRarety.Bronze: m_NbBronzeStuds += aQuantity; break;
            }

            m_HasAlreadyCountTotalStuds = false;
            RebuildInventory();
        }

        public void RemoveStuds(EPowerColor aColor, int aQuantity)
        {
            switch (aColor)
            {
                case EPowerColor.Red: m_NbRedStuds -= aQuantity; break;
                case EPowerColor.Blue: m_NbBlueStuds -= aQuantity; break;
                case EPowerColor.Yellow: m_NbYellowStuds -= aQuantity; break;
                case EPowerColor.Green: m_NbGreenStuds -= aQuantity; break;
                case EPowerColor.White: m_NbWhiteStuds -= aQuantity; break;
            }

            m_HasAlreadyCountTotalStuds = false;
        }

        public void RemoveStuds(EChallengeRarety aRarety, int aQuantity)
        {
            switch (aRarety)
            {
                case EChallengeRarety.Gold: m_NbGoldStuds -= aQuantity; break;
                case EChallengeRarety.Silver: m_NbSilverStuds -= aQuantity; break;
                case EChallengeRarety.Bronze: m_NbBronzeStuds -= aQuantity; break;
            }

            m_HasAlreadyCountTotalStuds = false;
        }

        private void RebuildInventory()
        {
            //added Additional bits
            int additionalRedCommon = m_NbRedStuds % COMMON_VALUE.m_Values;
            int additionalBlueCommon = m_NbBlueStuds % COMMON_VALUE.m_Values;
            int additionalYellowCommon = m_NbYellowStuds % COMMON_VALUE.m_Values;
            int additionalGreenCommon = m_NbGreenStuds % COMMON_VALUE.m_Values;
            int additionalWhiteCommon = m_NbWhiteStuds % COMMON_VALUE.m_Values;

            int additionalBronze =
                additionalRedCommon +
                additionalBlueCommon +
                additionalYellowCommon +
                additionalGreenCommon +
                additionalWhiteCommon;

            m_NbBronzeStuds += additionalBronze;

            int additionalSilver = m_NbBronzeStuds % BRONZE_VALUE.m_Values;
            m_NbSilverStuds += additionalSilver;

            int additionalGold = m_NbSilverStuds % SILVER_VALUE.m_Values;
            m_NbGoldStuds += additionalGold;

            //Reset values to respect original Range
            m_NbRedStuds -= additionalRedCommon;
            m_NbBlueStuds -= additionalBlueCommon;
            m_NbYellowStuds -= additionalYellowCommon;
            m_NbGreenStuds -= additionalGreenCommon;
            m_NbWhiteStuds -= additionalWhiteCommon;
            m_NbBronzeStuds -= additionalBronze;
            m_NbSilverStuds -= additionalSilver;
            m_NbGoldStuds -= additionalGold;
        }

        public int GetStudsQuantity(EPowerColor aColor)
        {
            switch (aColor)
            {
                case EPowerColor.Red: return m_NbRedStuds;
                case EPowerColor.Blue: return m_NbBlueStuds;
                case EPowerColor.Yellow: return m_NbYellowStuds;
                case EPowerColor.Green: return m_NbGreenStuds;
                case EPowerColor.White: return m_NbWhiteStuds;
                default: return 0;
            }
        }

        public int GetStudsQuantity(EChallengeRarety aRarety)
        {
            switch (aRarety)
            {
                case EChallengeRarety.Gold: return m_NbGoldStuds;
                case EChallengeRarety.Silver: return m_NbSilverStuds;
                case EChallengeRarety.Bronze: return m_NbBronzeStuds;
                default: return 0;
            }
        }

        public bool HasEnoughRessourcesFor(EPowerColor aColor, int aQuantity)
        {
            ReCountTotalValue();
            /*
            switch (aColor)
            {
                case EPowerColor.Red: return m_NbRedStuds >= aQuantity; 
                case EPowerColor.Blue: return m_NbBlueStuds >= aQuantity; 
                case EPowerColor.Yellow: return m_NbYellowStuds >= aQuantity; 
                case EPowerColor.Green: return m_NbGreenStuds >= aQuantity; 
                case EPowerColor.White: return m_NbWhiteStuds >= aQuantity;
                default: return false;
            }*/

            return m_TotalStuds > COMMON_VALUE.m_Values * aQuantity; 
        }

        public bool HasEnoughRessourcesFor(EChallengeRarety aRarety, int aQuantity)
        {
            ReCountTotalValue();
            /*
            switch (aRarety)
            {
                case EChallengeRarety.Gold: return m_NbGoldStuds >= aQuantity; 
                case EChallengeRarety.Silver: return m_NbSilverStuds >= aQuantity; 
                case EChallengeRarety.Bronze: return m_NbBronzeStuds >= aQuantity;
                default: return false;
            }*/

            switch (aRarety)
            {
                case EChallengeRarety.Gold: return m_TotalStuds >= aQuantity * GOLD_VALUE.m_Values;
                case EChallengeRarety.Silver: return m_TotalStuds >= aQuantity * SILVER_VALUE.m_Values;
                case EChallengeRarety.Bronze: return m_TotalStuds >= aQuantity * BRONZE_VALUE.m_Values;
                default: return false;
            }
        }

        private void ReCountTotalValue()
        {
            if (!m_HasAlreadyCountTotalStuds)
            {
                m_HasAlreadyCountTotalStuds = true;
                m_TotalStuds =
                    COMMON_VALUE.m_Values * m_NbRedStuds +
                    COMMON_VALUE.m_Values * m_NbBlueStuds +
                    COMMON_VALUE.m_Values * m_NbYellowStuds +
                    COMMON_VALUE.m_Values * m_NbGreenStuds +
                    COMMON_VALUE.m_Values * m_NbWhiteStuds +
                    BRONZE_VALUE.m_Values * m_NbBronzeStuds +
                    SILVER_VALUE.m_Values * m_NbSilverStuds +
                    GOLD_VALUE.m_Values   * m_NbGoldStuds;
            }
        }

        public bool HasNoRessourcesFor(EPowerColor aColor)
        {
            switch (aColor)
            {
                case EPowerColor.Red: return m_NbRedStuds == 0; 
                case EPowerColor.Blue: return m_NbBlueStuds == 0; 
                case EPowerColor.Yellow: return m_NbYellowStuds == 0; 
                case EPowerColor.Green: return m_NbGreenStuds == 0; 
                case EPowerColor.White: return m_NbWhiteStuds == 0;
                default: return false;
            }
        }

        public bool HasNoRessourcesFor(EChallengeRarety aRarety)
        {
            switch (aRarety)
            {
                case EChallengeRarety.Gold: return m_NbGoldStuds == 0;
                case EChallengeRarety.Silver: return m_NbSilverStuds == 0;
                case EChallengeRarety.Bronze: return m_NbBronzeStuds == 0;
                default: return false;
            }
        }
    }
}
