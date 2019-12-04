using Edi.Captcha;
using HappyDog.Domain.DataTransferObjects.Comment;
using HappyDog.Domain.Services;
using HappyDog.Infrastructure.Email;
using HappyDog.Test.Common;
using HappyDog.WebUI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HappyDog.WebUI.Test
{
    [TestClass]
    public class CommentControllerTest : TestBase
    {
        [TestMethod]
        public async Task ErrorCodePostTest()
        {
            var svc = new CommentService(DbContext, Mapper);
            var mockISessionBasedCaptcha = new Mock<ISessionBasedCaptcha>();
            mockISessionBasedCaptcha
                .Setup(m => m.ValidateCaptchaCode(It.IsAny<string>(), It.IsAny<ISession>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(false);
            var mockEmailSender = new Mock<IEmailSender>();
            mockEmailSender
                .Setup(m => m.SendAsync(It.IsAny<MailMessage>()))
                .Returns(Task.CompletedTask);
            var mockSession = new Mock<ISession>();
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext
                .SetupGet(m => m.Session)
                .Returns(mockSession.Object);
            var mockTempData = new Mock<ITempDataDictionary>();

            using var controller = new CommentController(svc, mockISessionBasedCaptcha.Object, mockEmailSender.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object
                },
                TempData = mockTempData.Object
            };

            var dto = new PostCommentDto
            {
                Content = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                ArticleId = 466
            };

            var actionResult = await controller.Post(dto);
            var redirectResult = actionResult as RedirectToActionResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Detail", redirectResult.ActionName);
            Assert.AreEqual("Article", redirectResult.ControllerName);
            Assert.AreEqual("466", redirectResult.RouteValues["id"].ToString());

            mockISessionBasedCaptcha.Verify(m => m.ValidateCaptchaCode(It.IsAny<string>(), It.IsAny<ISession>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once());
            mockEmailSender.Verify(m => m.SendAsync(It.IsAny<MailMessage>()), Times.Never());
        }

        [TestMethod]
        public async Task CorrectCodePostTest()
        {
            var svc = new CommentService(DbContext, Mapper);
            var mockISessionBasedCaptcha = new Mock<ISessionBasedCaptcha>();
            mockISessionBasedCaptcha
                .Setup(m => m.ValidateCaptchaCode(It.IsAny<string>(), It.IsAny<ISession>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(true);
            var mockEmailSender = new Mock<IEmailSender>();
            mockEmailSender
                .Setup(m => m.SendAsync(It.IsAny<MailMessage>()))
                .Returns(Task.CompletedTask);
            mockEmailSender
                .SetupGet(m => m.FromAddress)
                .Returns("hero_wong@outlook.com");
            var mockSession = new Mock<ISession>();
            var mockRequest = new Mock<HttpRequest>();
            mockRequest
                .SetupGet(m => m.Host)
                .Returns(new HostString("192.168.1.11"));
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext
                .SetupGet(m => m.Session)
                .Returns(mockSession.Object);
            mockHttpContext
                .SetupGet(m => m.Request)
                .Returns(mockRequest.Object);

            using var controller = new CommentController(svc, mockISessionBasedCaptcha.Object, mockEmailSender.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object,
                }
            };

            var dto = new PostCommentDto
            {
                Content = "Content",
                Email = "Email",
                ArticleId = 466
            };

            var actionResult = await controller.Post(dto);
            var redirectResult = actionResult as RedirectToActionResult;
            var entity = DbContext.Comments.First();

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Detail", redirectResult.ActionName);
            Assert.AreEqual("Article", redirectResult.ControllerName);
            Assert.AreEqual("466", redirectResult.RouteValues["id"].ToString());

            Assert.AreEqual(466, entity.ArticleId);
            Assert.AreEqual("Content", entity.Content);
            Assert.AreEqual("Email", entity.Email);

            mockISessionBasedCaptcha.Verify(m => m.ValidateCaptchaCode(It.IsAny<string>(), It.IsAny<ISession>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once());
            await Task.Delay(200);
            mockEmailSender.Verify(m => m.SendAsync(It.IsAny<MailMessage>()), Times.Once());
        }
    }
}
