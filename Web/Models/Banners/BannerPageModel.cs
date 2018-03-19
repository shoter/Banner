using Infrastructure.DataModels;
using Infrastructure.Db.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Banners
{
    public class BannerPageModel
    {
        /// <summary>
        /// Total number of banners held by service
        /// </summary>
        public long Total { get; set; }
        /// <summary>
        /// List of banners on this page
        /// </summary>
        public List<BannerModel> Banners { get; set; }

        public BannerPageModel(Page<Banner> page)
        {
            Total = page.Total;
            Banners = page.Items
                .Select(item => new BannerModel(item))
                .ToList();
        }
    }
}