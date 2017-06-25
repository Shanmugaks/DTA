using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumberService.Controllers;
using System.Web.Http.Results;
using System.Web.Http;

namespace NumberService.Tests
{
    [TestClass]
    public class TestNumberController
    {
        [TestMethod]
        public void TestGet_String()
        {
            var controller = new NumberController();
            IHttpActionResult Result = controller.Get("sdsdsd;skpwepwewewewcsdcxc");
            Assert.IsInstanceOfType(Result, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void TestGet_CorrectNumber_NoDecimal()
        {
            var controller = new NumberController();
            IHttpActionResult Result = controller.Get("12345678");

            Assert.IsNotNull(Result);           
            Assert.IsInstanceOfType(Result, typeof(System.Web.Http.Results.OkNegotiatedContentResult<string>));
        }


        [TestMethod]
        public void TestGet_CorrectNumber_WithDecimal()
        {
            var controller = new NumberController();
            IHttpActionResult Result = controller.Get("1234567842323.345");

            Assert.IsNotNull(Result);
            Assert.IsInstanceOfType(Result, typeof(System.Web.Http.Results.OkNegotiatedContentResult<string>));
        }

        [TestMethod]
        public void TestGet_CorrectNumber_WithExponential()
        {
            var controller = new NumberController();
            IHttpActionResult Result = controller.Get("1234567842323.345e");

            Assert.IsNotNull(Result);
            Assert.IsInstanceOfType(Result, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void TestGet_CorrectNumberPositiveSign()
        {
            var controller = new NumberController();
            IHttpActionResult Result = controller.Get("+12345678");

            Assert.IsNotNull(Result);
            Assert.IsInstanceOfType(Result, typeof(System.Web.Http.Results.OkNegotiatedContentResult<string>));
        }

        [TestMethod]
        public void TestGet_CorrectNumberNegativeSign()
        {
            var controller = new NumberController();
            IHttpActionResult Result = controller.Get("-12345678");

            Assert.IsNotNull(Result);
            Assert.IsInstanceOfType(Result, typeof(System.Web.Http.Results.OkNegotiatedContentResult<string>));
        }

    }
}
