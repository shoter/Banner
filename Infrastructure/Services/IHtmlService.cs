using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IHtmlService
    {
        /// <summary>
        /// Should return true if provided html markup is valid
        /// </summary>
        bool IsValidHtml(string html);
    }
}
