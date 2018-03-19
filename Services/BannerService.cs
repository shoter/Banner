using Common.Results;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.DataModels;
using Infrastructure;

namespace Services
{
    public class BannerService : IBannerService
    {
        private readonly IHtmlService htmlService;
        private readonly IUnitOfWork unit;

        public BannerService(IUnitOfWork unitOfWork, IHtmlService htmlService)
        {
            this.htmlService = htmlService;
            this.unit = unitOfWork;
        }

        public Banner CreateBanner(string html)
        {
            if (ValidateHtml(html).IsError)
                throw new Exception("HTML is not valid!");

            var banner = new Banner()
            {
                Id = 0,
                Html = html,
                Created = DateTime.Now,
                Modified = DateTime.Now
            };
            unit.BannerRepository.Insert(banner);
            return banner;
        }

        public Banner UpdateBanner(int id, string html)
        {
            if (ValidateHtml(html).IsError)
                throw new Exception("HTML is not valid!");

            var banner = unit.BannerRepository.FindById(id);
            banner.Html = html;
            banner.Modified = DateTime.Now;

            unit.BannerRepository.Update(id, banner);
            return banner;
        }

        public MethodResult ValidateHtml(string html)
        {
            if (htmlService.IsValidHtml(html) == false)
                return new MethodResult("HTML for this banner is not valid!");

            return MethodResult.Success;
        }

         
    }
}
