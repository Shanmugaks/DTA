using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumberService.BusinessLogic
{
    public class UnitMetaInfo
    {
        int UnitID;
        string UnitName;
        int UnitsMaxLength;
        int DigitsMaxLength;
        public UnitMetaInfo(int UnitID_in, string UnitName_in, int UnitsMaxLength_in, int DigitsMaxLength_in)
        {
            UnitID = UnitID_in;
            UnitName = UnitName_in;
            UnitsMaxLength = UnitsMaxLength_in;
            DigitsMaxLength = DigitsMaxLength_in;
        }

        public string GetUnitName()
        {
            return UnitName;
        }

        public int GetUnitID()
        {
            return UnitID;
        }

        public int GetUnitMaxLength()
        {
            return UnitsMaxLength;
        }

        public int GetDigitsMaxLength()
        {
            return DigitsMaxLength;
        }
    }
}