using Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Banners
{
    public class BannerModel : BannerValidatedBaseModel
    {
        /// <summary>
        /// Unique banner id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// Date of banner creation
        /// </summary>
        public DateTime? Created { get; set; }
        /// <summary>
        /// Date of last banner modification
        /// </summary>
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