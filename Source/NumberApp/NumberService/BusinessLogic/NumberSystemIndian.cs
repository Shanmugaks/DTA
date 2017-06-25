using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumberService.BusinessLogic
{
    public class NumberSystemIndian : NumberSystemWestern
    {
        public override void BuildUnitMetaInfo()
        {
            UnitMetaInfoDictionary.Add(1, new UnitMetaInfo(1, "HUNDRED", 3, 3));
            UnitMetaInfoDictionary.Add(2, new UnitMetaInfo(2, "THOUSAND", 3, 6));
            UnitMetaInfoDictionary.Add(3, new UnitMetaInfo(3, "LAKH", 2, 8));
            UnitMetaInfoDictionary.Add(4, new UnitMetaInfo(4, "CRORE", 2, 10));

            CurrencyName = "RUPEES";
            CoinName = "PAISE";

        }
    }
}