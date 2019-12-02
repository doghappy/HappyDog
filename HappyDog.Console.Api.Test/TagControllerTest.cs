using HappyDog.Console.Api.Controllers;
using HappyDog.Domain.DataTransferObjects.Tag;
using HappyDog.Domain.Entities;
using HappyDog.Domain.IServices;
using HappyDog.Test.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace HappyDog.Console.Api.Test
{
    [TestClass]
    public class TagControllerTest : TestBase
    {
        [TestMethod]
        public async Task Get_tag_with_null()
        {
            var mockSvc = new Mock<ITagService>();
            mockSvc
                .Setup(t => t.GetTagDtoAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(default(TagDto)));

            var controller = new TagController(mockSvc.Object);
            var actionResult = await controller.GetTag(null);
            var notFoundResult = actionResult as NotFoundResult;

            Assert.IsNotNull(notFoundResult);
        }

        [TestMethod]
        public async Task Get_tag_with_name()
        {
            string guid = Guid.NewGuid().ToString();
            var mockSvc = new Mock<ITagService>();
            mockSvc
                .Setup(t => t.GetTagDtoAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new TagDto
                {
                    Name = guid
                }));

            var controller = new TagController(mockSvc.Object);
            var actionResult = await controller.GetTag(" net ");
            var jsonResult = actionResult as JsonResult;
            var model = jsonResult.Value as TagDto;

            Assert.AreEqual(guid, model.Name);
        }

        [TestMethod]
        public async Task Put_not_exists()
        {
            var mockSvc = new Mock<ITagService>();
            mockSvc
                .Setup(t => t.PutAsync(It.IsAny<int>(), It.IsAny<PutTagDto>()))
                .Returns(Task.FromResult(default(TagDto)));

            var controller = new TagController(mockSvc.Object);
            var actionResult = await controller.Put(1, new PutTagDto());
            var notFoundResult = actionResult as NotFoundResult;

            Assert.IsNotNull(notFoundResult);
        }

        [TestMethod]
        public async Task Put()
        {
            string guid = Guid.NewGuid().ToString();
            var mockSvc = new Mock<ITagService>();
            mockSvc
                .Setup(t => t.PutAsync(It.IsAny<int>(), It.IsAny<PutTagDto>()))
                .Returns(Task.FromResult(new TagDto
                {
                    Name = guid
                }));

            var controller = new TagController(mockSvc.Object);
            var actionResult = await controller.Put(It.IsAny<int>(), It.IsAny<PutTagDto>());
            var jsonResult = actionResult as JsonResult;
            var model = jsonResult.Value as TagDto;

            Assert.AreEqual(guid, model.Name);
        }
    }
}
