using Infrastructure;
using Infrastructure.Services;
using Mongo;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AcceptanceTests
{
    [Collection("Non-parallel")]
    public class BannerServiceTests
    {
        private readonly IUnitOfWork unit;
        private readonly IBannerService bannerService;
        private int tenMilisecondsTicks = 100_000;

        public BannerServiceTests()
        {
            unit = new UnitOfWork(MongoDatabaseProvider.MongoDatabase);
            bannerService = new BannerService(unit, new HtmlService());

            //To have clean db before each test
            unit.BannerRepository.RemoveAll();
        }

        [Fact]
        public void CreateBanner_validResponse_test()
        {
            var now = DateTime.Now;
            var html = "<p>test</p>";

            var response = bannerService.CreateBanner(0, html);

            Assert.Equal(0, response.Id);
            Assert.Equal(html, response.Html);
            Assert.True(response.Created.Ticks - now.Ticks >= -tenMilisecondsTicks);
            Assert.Null(response.Modified);
        }
        
        [Fact]
        public void CreateBanner_invalidHtml_test()
        {
            var html = "<p>test</pasdsad>";

            Assert.Throws<Exception>(() => bannerService.CreateBanner(0, html));
        }

        [Fact]
        public void CreateBanner_queryDataAfterInsert_test()
        {
            var now = DateTime.Now;
            var html = "<p>test</p>";

            bannerService.CreateBanner(0, html);
            var data = unit.BannerRepository.FindById(0);

            Assert.Equal(0, data.Id);
            Assert.Equal(html, data.Html);
            Assert.True(data.Created.Ticks - now.Ticks >= -tenMilisecondsTicks);
            Assert.Null(data.Modified);
        }

        [Fact]
        public void UpdateBanner_validResponse_test()
        {
            var now = DateTime.Now;
            var html = "<p>test</p>";

            bannerService.CreateBanner(0, "<div>divvvv</div>");
            var response = bannerService.UpdateBanner(0, html);

            Assert.Equal(0, response.Id);
            Assert.Equal(html, response.Html);
            Assert.True(response.Created.Ticks - now.Ticks >= -tenMilisecondsTicks);
            Assert.True(response.Modified.Value.Ticks - now.Ticks >= -tenMilisecondsTicks);
            Assert.NotEqual(response.Created, response.Modified);
        }

        [Fact]
        public void UpdateBanner_queryDataAfterUpdate_test()
        {
            var now = DateTime.Now;
            var html = "<p>test</p>";

            bannerService.CreateBanner(0, "<div>divvvv</div>");
            bannerService.UpdateBanner(0, html);
            var data = unit.BannerRepository.FindById(0);

            Assert.Equal(0, data.Id);
            Assert.Equal(html, data.Html);
            Assert.True(data.Created.Ticks - now.Ticks >= -tenMilisecondsTicks);
            Assert.True(data.Modified.Value.Ticks - now.Ticks >= -tenMilisecondsTicks);
            Assert.NotEqual(data.Created, data.Modified);
        }

        [Fact]
        public void UpdateBanner_invalidHtml_test()
        {
            bannerService.CreateBanner(0, "<div>divvvv</div>");
            Assert.Throws<Exception>(() => bannerService.CreateBanner(0, "<div>asd</asdasd>"));
        }


        [Fact]
        public void RemoveBanner_noData_test()
        {
            bannerService.CreateBanner(0, "<div>divvvv</div>");
            bannerService.RemoveBanner(0);
            var data = unit.BannerRepository.FindById(0);

            Assert.Null(data);
        }
    }
}
