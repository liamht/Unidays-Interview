using Microsoft.AspNetCore.Mvc;
using Unidays.Interview.UI.Controllers;
using Xunit;

namespace Unidays.Interview.UI.Test.Controllers
{
    public class HomeControllerTests
    {
        private HomeController _subject;

        public HomeControllerTests()
        {
            _subject = new HomeController();
        }

        [Fact]
        public void Index_WhenMessageIsSet_SetsPropertyInViewbag()
        {
            Assert.Null(_subject.ViewBag.Message);

            var message = "this message is a unit test";
            var result = _subject.Index(message) as ViewResult;

            Assert.Equal(message, _subject.ViewBag.Message);
        }
        
        [Fact]
        public void Index_WhenMessageIsNotSet_DoesNotSetPropertyInViewbag()
        {
            Assert.Null(_subject.ViewBag.Message);

            var result = _subject.Index() as ViewResult;

            Assert.Null(_subject.ViewBag.Message);
        }

        [Fact]
        public void Index_OnGet_ReturnsDefaultView()
        {
            var result = _subject.Index() as ViewResult;

            Assert.Null(result.ViewName);
        }
    }
}
