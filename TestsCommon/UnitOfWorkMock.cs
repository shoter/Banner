using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using Moq;

namespace TestsCommon
{
    public class UnitOfWorkMock : IUnitOfWork
    {
        public readonly Mock<IBannerRepository> BannerRepositoryMock = new Mock<IBannerRepository>();
        public IBannerRepository BannerRepository => BannerRepositoryMock.Object;
    }
}
