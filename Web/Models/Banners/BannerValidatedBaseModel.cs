using Common.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.Banners
{
    public class BannerValidatedBaseModel : IValidatableObject
    {

        public string Html { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (HtmlValidator.IsValidHtml(Html) == false)
                yield return new ValidationResult("Invalid Html", new string[] { nameof(Html) });
        }
    }
}