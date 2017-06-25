using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http.Cors;
using System.Web.Http;
using NumberService.BusinessLogic;

namespace NumberService.Controllers
{
    /// <summary>
    /// Description for NumberController.
    /// </summary>
    public class NumberController : ApiController
    {
        /// <summary>
        /// This API converts given number into text
        /// </summary>
        /// <param name="NumberToConvert"></param>
        /// <remarks>This API converts given number into text</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>     

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Get(string NumberToConvert)
        {
            IHttpActionResult Result = null;
            try
            {
                bool IsNumberSignAvailable = false;
                bool IsDecimalNumber = false;

                if (string.IsNullOrEmpty(NumberToConvert) == true)
                {
                    Result = BadRequest("Invalid Input (NULL)");
                }
                if (NumberToConvert.ToLower().Contains('e') == true)
                {
                    Result = BadRequest("Invalid Input");
                }
                else if (IsNumberOnly(NumberToConvert, out IsNumberSignAvailable, out IsDecimalNumber) == true)
                {
                    INumberToTextProcessor NumberToTextProcessor = new NumberToTextDefaultProcessor(NumberToConvert, new NumberSystemWestern(), IsNumberSignAvailable, IsDecimalNumber);
                    NumberToTextProcessor.Process();
                    string ToText = NumberToTextProcessor.GetResult();
                    Result = Ok(ToText);
                }
                else
                {
                    Result = BadRequest("Invalid Input"); ;
                }
            }
            catch (Exception e)
            {
                Result = InternalServerError(e);
            }

            return Result;
        }

        /// <summary>
        /// Description for IsNumberOnly.
        /// </summary>

        bool IsNumberOnly(string NumberToConvert, out bool IsNumberSignAvailable, out bool IsDecimalNumber)
        {
            int nIndex = 0;
            int nDotCounter = 0;
            IsNumberSignAvailable = false;
            IsDecimalNumber = false;
            foreach (char ch in NumberToConvert)
            {
                nIndex++;
                if (nIndex == 1 && (ch == '-' || ch == '+'))
                {
                    IsNumberSignAvailable = true;
                    continue;
                }
                else if (ch >= '0' && ch <= '9')
                {
                    continue;
                }
                else if (nDotCounter == 0 && ch == '.')
                {
                    ++nDotCounter;
                    IsDecimalNumber = true;
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
