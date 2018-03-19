using Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Banners
{
    public class BannerModel
    {
        public int? Id { get; set; }
        public string Html { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public BannerModel() { }
        public BannerModel(Banner banner)
        {
            Id = banner?.Id;
            Html = banner?.Html;
            Created = banner?.Created;
            Modified = banner?.Modified;
        }
    }
}