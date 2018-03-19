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
        public BannerViewModel Get(int id)
        {
            var banner = unit.BannerRepository.FindById(id);
            return new BannerViewModel(banner);
        }

        public List<BannerViewModel> Get(int pageNumber, int pageSize)
        {
            var banners = unit.BannerRepository
        }

        public BannerViewModel Post(string html)
        {
            throwExceptionIfInvalidHtml(html);

            var banner = bannerService.CreateBanner(html);
            return new BannerViewModel(banner);
        }

        public BannerViewModel Post(int id, string html)
        {
            throwExceptionIfInvalidHtml(html);

            var banner = bannerService.UpdateBanner(id, html);
            return new BannerViewModel(banner);
        }

        public void Delete(int id)
        {
            unit.BannerRepository.Remove(id);
        }

        private void throwExceptionIfInvalidHtml(string html)
        {
            var result = bannerService.ValidateHtml(html);
            if (result.IsError)
                ThrowHttpException(HttpStatusCode.BadRequest, result.ToString());
        }
    }
}