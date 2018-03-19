using Infrastructure;
using Infrastructure.Services;
using Mongo;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Web.Controllers;
using Xunit;

namespace AcceptanceTests
{
    [Collection("Non-parallel")]
    public class BannerControllerTests
    {
        private readonly IUnitOfWork unit;
        private readonly IBannerService bannerService;
        private int tenMilisecondsTicks = 100_000;
        private readonly BannerController bannerController;

        public BannerControllerTests()
        {
            unit = new UnitOfWork(MongoDatabaseProvider.MongoDatabase);
            bannerService = new BannerService(unit);
            bannerController = new BannerController(unit, bannerService);
            bannerController.Configuration = new HttpConfiguration();
            unit.BannerRepository.RemoveAll();
        }

        [Fact]
        public void Create_validResponse_test()
        {
            var now = DateTime.Now;
            var html = "<p>test</p>";

            var response = bannerController.Post(new Web.Models.Banners.NewBannerModel()
            {
                Id = 0,
                Html = html
            });

            Assert.Equal(0, response.Id);
            Assert.Equal(html, response.Html);
            Assert.True(response.Created.Value.Ticks - now.Ticks >= -tenMilisecondsTicks);
            Assert.Null(response.Modified);
        }

        [Fact]
        public void Create_invalidHtml_test()
        {
            var model = new Web.Models.Banners.NewBannerModel()
            {
                Id = 0,
                Html = "<di>asdsad</div>"
            };


            bannerController.Validate(model);
            Assert.Throws<HttpResponseException>(() => bannerController.Post(model));
        }

        [Fact]
        public void Create_queryDataAfterInsert_test()
        {
            var now = DateTime.Now;
            var html = "<p>test</p>";

            var response = bannerController.Post(new Web.Models.Banners.NewBannerModel()
            {
                Id = 0,
                Html = html
            });
            var data = unit.BannerRepository.FindById(0);

            Assert.Equal(0, data.Id);
            Assert.Equal(html, data.Html);
            Assert.True(data.Created.Ticks - now.Ticks >= -tenMilisecondsTicks);
            Assert.Null(data.Modified);
        }

        [Fact]
        public void Update_validResponse_test()
        {
            var now = DateTime.Now;
            var html = "<p>test</p>";

            bannerService.CreateBanner(0, "<div>divvvv</div>");
            var response = bannerController.Post(0, html);

            Assert.Equal(0, response.Id);
            Assert.Equal(html, response.Html);
            Assert.True(response.Created.Value.Ticks - now.Ticks >= -tenMilisecondsTicks);
            Assert.True(response.Modified.Value.Ticks - now.Ticks >= -tenMilisecondsTicks);
            Assert.NotEqual(response.Created, response.Modified);
        }

        [Fact]
        public void Update_queryDataAfterUpdate_test()
        {
            var now = DateTime.Now;
            var html = "<p>test</p>";

            bannerService.CreateBanner(0, "<div>divvvv</div>");
            bannerController.Post(0, html);
            var data = unit.BannerRepository.FindById(0);

            Assert.Equal(0, data.Id);
            Assert.Equal(html, data.Html);
            Assert.True(data.Created.Ticks - now.Ticks >= -tenMilisecondsTicks);
            Assert.True(data.Modified.Value.Ticks - now.Ticks >= -tenMilisecondsTicks);
            Assert.NotEqual(data.Created, data.Modified);
        }

        [Fact]
        public void Update_invalidHtml_test()
        {
            var html = "<div>a</div>";
            bannerService.CreateBanner(0, html);

            Assert.Throws<HttpResponseException>(() => bannerController.Post(0, "<di>asdsad</div>"));
        }

        [Fact]
        public void Delete_noData_test()
        {
            bannerService.CreateBanner(0, "<div>divvvv</div>");
            bannerController.Delete(0);
            var data = unit.BannerRepository.FindById(0);

            Assert.Null(data);
        }

        [Fact]
        public void Get_singleRecord_test()
        {
            var now = DateTime.Now;
            var html = "<div>test</div>";

            bannerService.CreateBanner(0, html);
            var response = bannerController.Get(0);

            Assert.Equal(0, response.Id);
            Assert.Equal(html, response.Html);
            Assert.True(response.Created.Value.Ticks - now.Ticks >= -tenMilisecondsTicks);
            Assert.Null(response.Modified);
        }

        [Fact]
        public void Get_fewRecords_test()
        {
            int recordCount = 10;
            int pageSize = 4;
            int pageNumber = 3;
            for (int i = 0; i < recordCount; ++i)
                bannerService.CreateBanner(i, $"<div>test{i}</div>");

            var response = bannerController.Get(pageNumber, pageSize);

            Assert.Equal(2, response.Banners.Count);
            Assert.Equal(recordCount, response.Total);

            Assert.Equal(8, response.Banners[0].Id);
            Assert.Equal(9, response.Banners[1].Id);
        }


    }
}

