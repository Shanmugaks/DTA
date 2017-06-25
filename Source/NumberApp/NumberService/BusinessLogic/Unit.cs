using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumberService.BusinessLogic
{
    public class Unit
    {
        UnitMetaInfo unitMetaInfo;
        int Number;        
        public Unit(UnitMetaInfo UnitMetaInfo_in, int Number_in)
        {
            unitMetaInfo = UnitMetaInfo_in;
            Number = Number_in;
        }
    }
}