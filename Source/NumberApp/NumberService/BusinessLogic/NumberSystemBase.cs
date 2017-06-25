using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumberService.BusinessLogic
{
    public abstract class NumberSystemBase : INumberSystem
    {
        protected string Name;
        protected Dictionary<int, string> RepetetiveNumbersInTextDictionary;
        protected Dictionary<int, UnitMetaInfo> UnitMetaInfoDictionary;
        protected int MaxDigitsConfigured;
        protected string CurrencyName;
        protected string CoinName;

        public virtual string GetCurrencyName()
        {
            return CurrencyName;
        }
        public virtual string GetCoinName()
        {
            return CoinName;
        }
        public NumberSystemBase()
        {
            MaxDigitsConfigured = 0;
            RepetetiveNumbersInTextDictionary = new Dictionary<int, string>();
            UnitMetaInfoDictionary = new Dictionary<int, UnitMetaInfo>();

        }
        virtual public bool GetRepetetiveNumbersInText(int givenNumber, out string givenNumberToText)
        {
            if (RepetetiveNumbersInTextDictionary.TryGetValue(givenNumber, out givenNumberToText) == true)
            {
                return true;
            }
            return false;
        }

        virtual public bool GetUnitMetaInfo(int UnitID, out UnitMetaInfo unitMetaInfo)
        {
            if (UnitMetaInfoDictionary.TryGetValue(UnitID, out unitMetaInfo) == true)
            {
                return true;
            }
            return false;
        }

        virtual public int GetUnitIDByDigitsLength(int DigitsLength)
        {
            if (MaxDigitsConfigured < DigitsLength)
            {
                throw new InvalidProgramException("Given Input Number is exceeding Max number Digits Configured....");
            }
            int UnitMetaInfoDictionaryCount = UnitMetaInfoDictionary.Count;
            for (int index = 1; index <= UnitMetaInfoDictionaryCount; index++)
            {
                UnitMetaInfo unitMetaInfoTmp = null;
                if (GetUnitMetaInfo(index, out unitMetaInfoTmp) == false)
                {
                    throw new InvalidProgramException("Possibly Number system not configured properly..");
                }

                int CurrUnitDigitsMaxLength = unitMetaInfoTmp.GetDigitsMaxLength();

                if (CurrUnitDigitsMaxLength >= DigitsLength)
                {
                    return unitMetaInfoTmp.GetUnitID();
                }
            }

            throw new InvalidProgramException("Possibly Number system not configured properly..");

        }
        public abstract string ToText(int Number, string UnitName, bool ConjuctionRequired = false);
        public abstract void Build();
        public abstract void BuildRepetetiveNumbers();
        public abstract void BuildUnitMetaInfo();

    }
}