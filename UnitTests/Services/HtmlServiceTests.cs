using Moq;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Services
{
    public class HtmlServiceTests
    {
        private HtmlService htmlService => mockHtmlService.Object;
        private Mock<HtmlService> mockHtmlService = new Mock<HtmlService>();

        public HtmlServiceTests()
        {
            mockHtmlService = new Mock<HtmlService>();
            mockHtmlService.CallBase = true;
        }

        [Theory]
        [InlineData("<span>test</span>")]
        [InlineData("<div><h4>title</h4></div>")]
        [InlineData("<div>test</div>")]
        public void IsValidHtml_valid_tests(string html)
        {
            Assert.True(htmlService.IsValidHtml(html));
        }

        [Theory]
        [InlineData("<span>test</spa>")]
        [InlineData("<div><h4>title</div></h4>")]
        [InlineData("<div>test<span></div></span>")]
        public void IsValidHtml_invalid_tests(string html)
        {
            Assert.False(htmlService.IsValidHtml(html));
        }
    }
}
