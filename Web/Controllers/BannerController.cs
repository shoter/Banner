using Infrastructure;
using Infrastructure.DataModels;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        /// <summary>
        /// Gets data about specific banner.
        /// </summary>
        /// <param name="id">Banner's Id</param>
        /// <returns>Banner data</returns>
        public BannerModel Get(int id)
        {
            var banner = unit.BannerRepository.FindById(id);
            return new BannerModel(banner);
        }

        /// <summary>
        /// Let's you see paged information about all banners in system.
        /// </summary>
        /// <param name="pageNumber">Number of page with banners to display</param>
        /// <param name="pageSize">Size of the page</param>
        /// <returns>Array with </returns>
        public BannerPageModel Get(int pageNumber, int pageSize)
        {
            var page = unit.BannerRepository.Page( skipCount: pageSize * (pageNumber - 1), pageSize: pageSize);

            return new BannerPageModel(page);
        }

        /// <summary>
        /// Creates new banner with given Id.
        /// </summary>
        /// <returns>New banner data with creation date.</returns>
        public BannerModel Post([FromBody]NewBannerModel newBanner)
        {
            throwExceptionIfBannerExists(newBanner?.Id);
            throwExceptionIfInvalidHtml(newBanner?.Html);

            var banner = bannerService.CreateBanner(newBanner.Id, newBanner.Html);
            return new BannerModel(banner);
        }

        private void throwExceptionIfBannerExists(int? id)
        {
            if (id.HasValue && unit.BannerRepository.Exists(id.Value))
                ThrowHttpException(HttpStatusCode.Conflict, "Banner with this id exists on the server!");
        }

        /// <summary>
        /// Updates banner with given Id
        /// </summary>
        /// <param name="id">Banner's Id</param>
        /// <param name="html">new html content</param>
        /// <returns>Updated banner data with new modification date.</returns>
        public BannerModel Post(int id, [FromBody]string html)
        {
            throwExceptionIfBannerDoesNotExist(id);
            throwExceptionIfInvalidHtml(html);

            var banner = bannerService.UpdateBanner(id, html);
            return new BannerModel(banner);
        }

        /// <summary>
        /// Removes banner from the server.
        /// </summary>
        /// <param name="id">Id of banner to remove</param>
        public void Delete(int id)
        {
            throwExceptionIfBannerDoesNotExist(id);

            bannerService.RemoveBanner(id);
        }

        private void throwExceptionIfBannerDoesNotExist(int id)
        {
            if (unit.BannerRepository.Exists(id) == false)
                ThrowHttpException(HttpStatusCode.Conflict, "Banner does not exist!");
        }

        /// <summary>
        /// Displays banner HTML as web page.
        /// </summary>
        /// <param name="id">Banner's id</param>
        /// <returns>HTTP response renderable in browser</returns>
        [Route("Api/Banner/Render/{id:int}")]
        [HttpGet]
        public HttpResponseMessage Render(int id)
        {
            var banner = unit.BannerRepository.FindById(id);
            var response = new HttpResponseMessage();
            response.Content = new StringContent(banner.Html);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

        private void throwExceptionIfInvalidHtml(string html)
        {
            var result = bannerService.ValidateHtml(html);
            if (result.IsError)
                ThrowHttpException(HttpStatusCode.BadRequest, result.ToString());
        }
    }
}