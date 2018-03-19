using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.Banners
{
    public class NewBannerModel : BannerValidatedBaseModel
    {
        /// <summary>
        /// Id number of new banner. Must be unique.
        /// </summary>
        [Required]
        public int? Id { get; set; }
    }
}