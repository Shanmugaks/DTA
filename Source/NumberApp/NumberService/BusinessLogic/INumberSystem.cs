using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberService.BusinessLogic
{
    public interface INumberSystem
    {
        bool GetRepetetiveNumbersInText(int givenNumber, out string givenNumberToText);
        bool GetUnitMetaInfo(int UnitID, out UnitMetaInfo unitMetaInfo);
        string ToText(int Number, string UnitName, bool ConjuctionRequired = false);
        int GetUnitIDByDigitsLength(int DigitsLength);
        void Build();
        void BuildRepetetiveNumbers();
        void BuildUnitMetaInfo();
        string GetCurrencyName();
        string GetCoinName();
    }
}
