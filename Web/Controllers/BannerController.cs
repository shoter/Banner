using Infrastructure;
using Infrastructure.DataModels;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Web.Models.Banners;

namespace Web.Controllers
{
    public class BannerController : BaseController
    {
        private readonly IUnitOfWork unit;
        private readonly IBannerService bannerService;

        public BannerController(IUnitOfWork unit, IBannerService bannerService)
        {
            this.unit = unit;
            this.bannerService = bannerService;
        }
        public BannerModel Get(int id)
        {
            var banner = unit.BannerRepository.FindById(id);
            return new BannerModel(banner);
        }

        public BannerPageModel Get(int pageNumber, int pageSize)
        {
            var page = unit.BannerRepository.Page( skipCount: pageSize * (pageNumber - 1), pageSize: pageSize);

            return new BannerPageModel(page);
        }

        public BannerModel Post([FromBody]NewBannerModel newBanner)
        {
            throwExceptionIfInvalidHtml(newBanner.Html);

            var banner = bannerService.CreateBanner(newBanner.Id, newBanner.Html);
            return new BannerModel(banner);
        }

        public BannerModel Post(int id, [FromBody]string html)
        {
            throwExceptionIfInvalidHtml(html);

            var banner = bannerService.UpdateBanner(id, html);
            return new BannerModel(banner);
        }

        public void Delete(int id)
        {
            bannerService.RemoveBanner(id);
        }

        private void throwExceptionIfInvalidHtml(string html)
        {
            var result = bannerService.ValidateHtml(html);
            if (result.IsError)
                ThrowHttpException(HttpStatusCode.BadRequest, result.ToString());
        }
    }
}