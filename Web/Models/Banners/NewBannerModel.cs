using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Banners
{
    public class NewBannerModel
    {
        /// <summary>
        /// Id number of new banner. Must be unique.
        /// </summary>
        public int Id { get; set; }
        public string Html { get; set; }
    }
}