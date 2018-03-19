using Common.Validators;
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
    public class HtmlValidatorTests
    {
        [Theory]
        [InlineData("<span>test</span>")]
        [InlineData("<div><h4>title</h4></div>")]
        [InlineData("<div>test</div>")]
        public void IsValidHtml_valid_tests(string html)
        {
            Assert.True(HtmlValidator.IsValidHtml(html));
        }

        [Theory]
        [InlineData("<span>test</spa>")]
        [InlineData("<div><h4>title</div></h4>")]
        [InlineData("<div>test<span></div></span>")]
        public void IsValidHtml_invalid_tests(string html)
        {
            Assert.False(HtmlValidator.IsValidHtml(html));
        }
    }
}
