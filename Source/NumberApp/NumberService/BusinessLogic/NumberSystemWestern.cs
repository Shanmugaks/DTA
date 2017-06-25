using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumberService.BusinessLogic
{
    public class NumberSystemWestern : NumberSystemBase
    {
        const string SPACER = " ";
        const string CONJUCTION = " AND ";
        const string HYPHEN = "-";

        public override void Build()
        {

            BuildRepetetiveNumbers();
            BuildUnitMetaInfo();
            int lastUnitID = UnitMetaInfoDictionary.Count();
            UnitMetaInfo unitMetaInfo = null;
            if (GetUnitMetaInfo(lastUnitID, out unitMetaInfo) == false)
            {
                throw new InvalidProgramException("Possibly Number system not configured properly..");
            }

            MaxDigitsConfigured = unitMetaInfo.GetDigitsMaxLength();

            CurrencyName = "DOLLAR";
            CoinName = "CENT";
        }
        public override void BuildRepetetiveNumbers()
        {
            RepetetiveNumbersInTextDictionary.Add(0, "ZERO");
            RepetetiveNumbersInTextDictionary.Add(1, "ONE");
            RepetetiveNumbersInTextDictionary.Add(2, "TWO");
            RepetetiveNumbersInTextDictionary.Add(3, "THREE");
            RepetetiveNumbersInTextDictionary.Add(4, "FOUR");
            RepetetiveNumbersInTextDictionary.Add(5, "FIVE");
            RepetetiveNumbersInTextDictionary.Add(6, "SIX");
            RepetetiveNumbersInTextDictionary.Add(7, "SEVEN");
            RepetetiveNumbersInTextDictionary.Add(8, "EIGHT");
            RepetetiveNumbersInTextDictionary.Add(9, "NINE");
            RepetetiveNumbersInTextDictionary.Add(10, "TEN");
            RepetetiveNumbersInTextDictionary.Add(11, "ELEVEN");
            RepetetiveNumbersInTextDictionary.Add(12, "TWELVE");
            RepetetiveNumbersInTextDictionary.Add(13, "THIRTEEN");
            RepetetiveNumbersInTextDictionary.Add(14, "FOURTEEN");
            RepetetiveNumbersInTextDictionary.Add(15, "FIFTEEN");
            RepetetiveNumbersInTextDictionary.Add(16, "SIXTEEN");
            RepetetiveNumbersInTextDictionary.Add(17, "SEVENTEEN");
            RepetetiveNumbersInTextDictionary.Add(18, "EIGHTEEN");
            RepetetiveNumbersInTextDictionary.Add(19, "NINENTEN");
            RepetetiveNumbersInTextDictionary.Add(20, "TWENTY");
            RepetetiveNumbersInTextDictionary.Add(30, "THIRTY");
            RepetetiveNumbersInTextDictionary.Add(40, "FORTY");
            RepetetiveNumbersInTextDictionary.Add(50, "FIFTY");
            RepetetiveNumbersInTextDictionary.Add(60, "SIXTY");
            RepetetiveNumbersInTextDictionary.Add(70, "SEVENTY");
            RepetetiveNumbersInTextDictionary.Add(80, "EIGHTY");
            RepetetiveNumbersInTextDictionary.Add(90, "NINENTY");
            RepetetiveNumbersInTextDictionary.Add(100, "HUNDRED");
        }

        public override void BuildUnitMetaInfo()
        {
            UnitMetaInfoDictionary.Add(1, new UnitMetaInfo(1, "HUNDRED", 3, 3));
            UnitMetaInfoDictionary.Add(2, new UnitMetaInfo(2, "THOUSAND", 3, 6));
            UnitMetaInfoDictionary.Add(3, new UnitMetaInfo(3, "MILLION", 3, 9));
            UnitMetaInfoDictionary.Add(4, new UnitMetaInfo(4, "BILLION", 3, 12));
            UnitMetaInfoDictionary.Add(5, new UnitMetaInfo(5, "TRILLION", 3, 15));
            UnitMetaInfoDictionary.Add(6, new UnitMetaInfo(6, "QUADRILLION", 3, 18));
            UnitMetaInfoDictionary.Add(7, new UnitMetaInfo(7, "QUINTILLION", 3, 21));
            UnitMetaInfoDictionary.Add(8, new UnitMetaInfo(8, "SEXTILLION", 3, 24));
            UnitMetaInfoDictionary.Add(9, new UnitMetaInfo(9, "SEPTILLION", 3, 27));
            UnitMetaInfoDictionary.Add(10, new UnitMetaInfo(10, "OCTILLION", 3, 30));
            UnitMetaInfoDictionary.Add(11, new UnitMetaInfo(11, "NONILLION", 3, 33));
            UnitMetaInfoDictionary.Add(12, new UnitMetaInfo(12, "DECILLION", 3, 36));
            UnitMetaInfoDictionary.Add(13, new UnitMetaInfo(13, "UNDECILLION", 3, 39));
            UnitMetaInfoDictionary.Add(14, new UnitMetaInfo(14, "DUODECILLION", 3, 42));
            UnitMetaInfoDictionary.Add(15, new UnitMetaInfo(15, "TREDECILLION", 3, 45));
            UnitMetaInfoDictionary.Add(16, new UnitMetaInfo(16, "QUATTUORDECILLION", 3, 48));
            UnitMetaInfoDictionary.Add(17, new UnitMetaInfo(17, "QUINDECILLION", 3, 51));
            UnitMetaInfoDictionary.Add(18, new UnitMetaInfo(18, "SEXDECILLION", 3, 54));
            UnitMetaInfoDictionary.Add(19, new UnitMetaInfo(19, "SEPTENDECILLION", 3, 57));

         

        }
        public override string ToText(int Number, string UnitName, bool ConjuctionRequired = false)
        {
            string Result = null;
            string TensPartText = null;
            int TensPart = Number % 100;
            int HundredsPart = Number / 100;

            if (GetRepetetiveNumbersInText(TensPart, out TensPartText) == false)
            {
                if (TensPart % 10 == 0)
                {
                    TensPartText = RepetetiveNumbersInTextDictionary[(TensPart / 10) * 10] + SPACER + RepetetiveNumbersInTextDictionary[TensPart % 10];
                }
                else
                {
                    TensPartText = RepetetiveNumbersInTextDictionary[(TensPart / 10) * 10] + HYPHEN + RepetetiveNumbersInTextDictionary[TensPart % 10];
                }
            }

            if (HundredsPart <= 0)
            {
                Result = TensPartText;

                if (ConjuctionRequired == false)
                {
                    Result = Result + SPACER + UnitName;
                }
            }
            else
            {

                if (ConjuctionRequired == true)
                {
                    if (TensPart != 0)
                        Result = RepetetiveNumbersInTextDictionary[HundredsPart] + SPACER + RepetetiveNumbersInTextDictionary[100] + CONJUCTION + TensPartText;
                    else
                        Result = RepetetiveNumbersInTextDictionary[HundredsPart] + SPACER + RepetetiveNumbersInTextDictionary[100];
                }
                else
                {
                    if (TensPart != 0)
                        Result = RepetetiveNumbersInTextDictionary[HundredsPart] + SPACER + RepetetiveNumbersInTextDictionary[100] + SPACER + TensPartText + SPACER + UnitName;
                    else
                        Result = RepetetiveNumbersInTextDictionary[HundredsPart] + SPACER + RepetetiveNumbersInTextDictionary[100] + SPACER + UnitName;
                }
            }

            return Result + SPACER;
        }
    }
}