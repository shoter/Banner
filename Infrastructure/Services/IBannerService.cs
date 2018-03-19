using Common.Results;
using Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IBannerService
    {
        MethodResult ValidateHtml(string html);
        Banner CreateBanner(int id, string html);
        Banner UpdateBanner(int id, string html);

        void RemoveBanner(int id);
    }
}
