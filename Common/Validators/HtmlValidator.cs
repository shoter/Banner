using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Validators
{
    public static class HtmlValidator
    {
        public static bool IsValidHtml(string html)
        {
            if (html == null)
                return false;

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            return doc.ParseErrors.Count() == 0;
        }
    }
}
