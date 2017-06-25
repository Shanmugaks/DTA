using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumberService.BusinessLogic
{
    public abstract class NumberToTextProcessorBase : INumberToTextProcessor
    {
        protected INumberSystem NumberSystem;

        // Template Method
        public void Process()
        {
            NumberSystem.Build();
            ParseInput();
            SegregateIntoUnits();
            BuildText();
        }

        public abstract void ParseInput();
        public abstract void SegregateIntoUnits();
        public abstract void BuildText();
        public abstract string GetResult();        

    }
}