using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumberService.BusinessLogic
{
    public class NumberToTextDefaultProcessor : NumberToTextProcessorBase
    {
        public NumberToTextDefaultProcessor(string Number_in, INumberSystem NumberSystem_in, bool IsNumberSignAvailable_in, bool IsDecimalNumber_in)
        {
            InputNumber = Number_in;
            NumberSystem = NumberSystem_in;
            IsNumberSignAvailable = IsNumberSignAvailable_in;
            IsDecimalNumber = IsDecimalNumber_in;
        }

        string InputNumber;
        string IntegerPartofInputNumber;
        string DecimalPartofInputNumber;
        string InputNumberText;
        string IntegerPartofInputNumberText;
        string DecimalPartofInputNumberText;
        string sign;
        bool IsNumberSignAvailable = false;
        bool IsDecimalNumber = false;
        int DecimalNumber;
        public override void ParseInput()
        {
            sign = null;
            DecimalNumber = 0;

            if (IsNumberSignAvailable == true)
            {
                if (InputNumber[0] == '-')
                {
                    sign = "MINUS";
                }

                InputNumber = InputNumber.Substring(1, InputNumber.Length - 1);
            }
            string[] subStrings = InputNumber.Split('.');
            int SubStringCount = subStrings.Count();

            if (SubStringCount == 0)
            {
                IntegerPartofInputNumber = null;
                DecimalPartofInputNumber = null;
            }
            else if (SubStringCount == 1)
            {
                if (subStrings[0] == ".")
                {
                    IntegerPartofInputNumber = null;
                    DecimalPartofInputNumber = subStrings[0];
                }
                else
                {
                    IntegerPartofInputNumber = subStrings[0];
                    DecimalPartofInputNumber = null;
                }
            }
            else if (SubStringCount == 2)
            {
                // Input have both Integer & Decimal part
                IntegerPartofInputNumber = subStrings[0];
                DecimalPartofInputNumber = subStrings[1];

                int nDecimalPartLength = DecimalPartofInputNumber.Length;
                if (nDecimalPartLength == 1)
                {
                    DecimalNumber = (int)Char.GetNumericValue(DecimalPartofInputNumber[0]);
                }
                else if (nDecimalPartLength == 2)
                {
                    int FirstDigit = (int)Char.GetNumericValue(DecimalPartofInputNumber[0]);
                    int SecondDigit = (int)Char.GetNumericValue(DecimalPartofInputNumber[1]);
                    DecimalNumber = (FirstDigit * 10) + SecondDigit;
                }
                else if (nDecimalPartLength >= 3)
                {
                    int FirstDigit = (int)Char.GetNumericValue(DecimalPartofInputNumber[0]);
                    int SecondDigit = (int)Char.GetNumericValue(DecimalPartofInputNumber[1]);
                    int ThirdDigit = (int)Char.GetNumericValue(DecimalPartofInputNumber[2]);
                    DecimalNumber = (FirstDigit * 10) + SecondDigit;
                    if (ThirdDigit >4)
                        DecimalNumber = DecimalNumber + 1;
                }
                else
                {
                    DecimalPartofInputNumber = null;
                }
            }
            else
            {
                IntegerPartofInputNumber = null;
                DecimalPartofInputNumber = null;
                throw new InvalidProgramException();
            }
        }
        public override void SegregateIntoUnits()
        {
            SegregateIntoUnitsForInteger();
            SegregateIntoUnitsForDecimal();
        }

        void SegregateIntoUnitsForInteger()
        {
            if (string.IsNullOrEmpty(IntegerPartofInputNumber) == true)
            {
                IntegerPartofInputNumberText = "Zero";
            }

            string IntegerPartofInputNumberTmp = IntegerPartofInputNumber;
            int nTotalLength = IntegerPartofInputNumberTmp.Length;
            int nCurrentUnitLength = nTotalLength;


            string TempNumberStr = IntegerPartofInputNumber;

            int UnitID = NumberSystem.GetUnitIDByDigitsLength(nTotalLength);

            int startIndex = 0, EndIndex = 0, ActualUnitLength = 0, ActualFullLength;
            ActualFullLength = nTotalLength;
            for (; UnitID > 0; UnitID--)
            {
                UnitMetaInfo CurrUnitMetaInfo = null;

                if (NumberSystem.GetUnitMetaInfo(UnitID, out CurrUnitMetaInfo) == false)
                {
                    throw new InvalidProgramException("");
                }

                if (startIndex == 0)
                {
                    ActualUnitLength = IntegerPartofInputNumberTmp.Length - CurrUnitMetaInfo.GetDigitsMaxLength() + CurrUnitMetaInfo.GetUnitMaxLength();
                }
                else
                {
                    ActualUnitLength = CurrUnitMetaInfo.GetUnitMaxLength();
                }

                EndIndex = startIndex + ActualUnitLength;
                TempNumberStr = IntegerPartofInputNumberTmp.Substring(startIndex, ActualUnitLength);

                // Convert string to Integer datatype
                int TempNumber = 0;
                if (Int32.TryParse(TempNumberStr, out TempNumber) == false)
                {
                    continue;
                }

                // Check not exceed 999
                if (TempNumber != 0)
                {
                    bool IsConjuctionRequired = UnitID == 1 ? true : false;
                    IntegerPartofInputNumberText = IntegerPartofInputNumberText + NumberSystem.ToText(TempNumber, CurrUnitMetaInfo.GetUnitName(), IsConjuctionRequired);
                }
                startIndex = EndIndex;
            }

        }

        void SegregateIntoUnitsForDecimal()
        {
            string UnitName = NumberSystem.GetCoinName();
            if (DecimalNumber >1)
            {
                UnitName = UnitName + "S";
            }
            DecimalPartofInputNumberText = NumberSystem.ToText(DecimalNumber, UnitName, false);            
        }
        public override void BuildText()
        {
            BuildTextForInteger();
            BuildTextForDecimal();
        }

        void BuildTextForInteger()
        {
            string UnitName = NumberSystem.GetCurrencyName();
            string TmpStr = "ONE";
            if (string.Equals(IntegerPartofInputNumberText, TmpStr, StringComparison.OrdinalIgnoreCase) == false)
            {
                UnitName = UnitName + "S";
            }

            if(string.IsNullOrEmpty(IntegerPartofInputNumberText) == false)
            {
                IntegerPartofInputNumberText = IntegerPartofInputNumberText + UnitName;
            }
            else
            {
                IntegerPartofInputNumberText = null;
            }
            
        }
        void BuildTextForDecimal()
        {
            
        }

        public override string GetResult()
        {
            if (IntegerPartofInputNumberText != null && IsDecimalNumber == true)
            {
                InputNumberText = IntegerPartofInputNumberText + " AND " + DecimalPartofInputNumberText;
            }
            else if (IntegerPartofInputNumberText != null && IsDecimalNumber == false)
            {
                InputNumberText = IntegerPartofInputNumberText;
            }
            else if (IntegerPartofInputNumberText == null && IsDecimalNumber == true)
            {
                InputNumberText = DecimalPartofInputNumberText;
            }

            if(sign != null)
            {
                InputNumberText = sign + " " + InputNumberText;
            }

            // Trim both start & End
            InputNumberText.Trim();

            return InputNumberText;
        }
    }   
}