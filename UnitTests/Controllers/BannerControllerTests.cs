using Common.Results;
using Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TestsCommon;
using Web.Controllers;
using Xunit;

namespace UnitTests.Controllers
{
    public class BannerControllerTests
    {
        private BannerController bannerController => mockBannerController.Object;
        private readonly Mock<BannerController> mockBannerController;
        private readonly UnitOfWorkMock unit = new UnitOfWorkMock();
        private readonly Mock<IBannerService> bannerService = new Mock<IBannerService>();

        public BannerControllerTests()
        {
            mockBannerController = new Mock<BannerController>(unit, bannerService.Object);
            mockBannerController.CallBase = true;
        }

        [Fact]
        public void Delete_checkBannerExistence_test()
        {
            mockBannerController.Setup(x => x.ThrowExceptionIfBannerDoesNotExist(It.IsAny<int>()));
            bannerController.Delete(123);

            mockBannerController.Verify(x => x.ThrowExceptionIfBannerDoesNotExist(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Delete_removeBanner_test()
        {
            mockBannerController.Setup(x => x.ThrowExceptionIfBannerDoesNotExist(It.IsAny<int>()));
            bannerController.Delete(123);

            bannerService.Verify(x => x.RemoveBanner(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Update_()
        {
            mockBannerController.Setup(x => x.ThrowExceptionIfBannerDoesNotExist(It.IsAny<int>()));
            bannerController.Delete(123);

            bannerService.Verify(x => x.RemoveBanner(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void ThrowExceptionIfBannerDoesNotExist_exists_test()
        {
            unit.BannerRepositoryMock.Setup(x => x.Exists(It.IsAny<int>())).Returns(true);
            bannerController.ThrowExceptionIfBannerDoesNotExist(123);
        }

        [Fact]
        public void ThrowExceptionIfBannerDoesNotExist_notExists_test()
        {
            unit.BannerRepositoryMock.Setup(x => x.Exists(It.IsAny<int>())).Returns(false);
            Assert.Throws<HttpResponseException>(() => bannerController.ThrowExceptionIfBannerDoesNotExist(123));
        }
    }
}
