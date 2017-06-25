using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberService.BusinessLogic
{
    public interface INumberToTextProcessor
    {
        void Process();
        void ParseInput();
        void SegregateIntoUnits();
        void BuildText();
        string GetResult();
        
    }
}
