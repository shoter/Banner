using Infrastructure.DataModels;
using Infrastructure.Db.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Banners
{
    public class BannerPageViewModel
    {
        public long Total { get; set; }
        public List<BannerViewModel> Banners { get; set; }

        public BannerPageViewModel(Page<Banner> page)
        {
            Total = page.Total;
            Banners = page.Items
                .Select(item => new BannerViewModel(item))
                .ToList();
        }
    }
}