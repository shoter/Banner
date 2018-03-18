using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.App_Start
{
    public class NinjectWebCommon
    {
        public static IKernel Init()
        {
            return createKernel();
        }
        private static IKernel createKernel()
        {
            var kernel = new StandardKernel();
            registerServices(kernel);
            return kernel;
        }


        private static void registerServices(IKernel kernel)
        {
           // kernel.Bind<IMongoDatabase
        }

    }
}