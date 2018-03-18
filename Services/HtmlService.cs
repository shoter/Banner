using HtmlAgilityPack;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class HtmlService : IHtmlService
    {
        public bool IsValidHtml(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            return doc.ParseErrors.Count() == 0;
        }
    }
}
