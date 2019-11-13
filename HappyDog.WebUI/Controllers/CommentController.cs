﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HappyDog.Domain.DataTransferObjects.Comment;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using HappyDog.WebUI.Extensions;
using Edi.Captcha;
using HappyDog.Infrastructure.Email;
using System.Net.Mail;
using System.Text;
using Markdig;

namespace HappyDog.WebUI.Controllers
{
    public class CommentController : Controller
    {
        public CommentController(
            IMapper mapper,
            CommentService commentService,
            ISessionBasedCaptcha captcha,
            NetEase126Sender emailSender)
        {
            _mapper = mapper;
            _commentService = commentService;
            _captcha = captcha;
            _emailSender = emailSender;
        }

        readonly IMapper _mapper;
        readonly CommentService _commentService;
        readonly ISessionBasedCaptcha _captcha;
        readonly NetEase126Sender _emailSender;

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post(PostCommentDto dto)
        {
            if (ModelState.IsValid)
            {
                if (_captcha.ValidateCaptchaCode(dto.Code, HttpContext.Session))
                {
                    var comment = _mapper.Map<Comment>(dto);
                    comment.IPv4 = Request.Host.ToString();
                    await _commentService.CreateAsync(comment);
                    await Task.Factory.StartNew(async () =>
                    {
                        var builder = new StringBuilder();
                        var pipeline = new MarkdownPipelineBuilder()
                            .UsePipeTables()
                            .Build();
                        builder.Append(Markdown.ToHtml(dto.Content, pipeline));
                        string link = $"https://doghappy.wang/Article/Detail/{dto.ArticleId}";
                        builder.AppendLine($"<br /> <a href=\"{link}\">{link}</a>");
                        await _emailSender.SendAsync(new MailMessage(_emailSender.FromAddress, "hero_wong@outlook.com")
                        {
                            Subject = "doghappy 有新评论了",
                            Body = builder.ToString(),
                            IsBodyHtml = true
                        });
                    });
                    return RedirectToAction("Detail", "Article", new { id = dto.ArticleId });
                }
                else
                {
                    TempData["ErrorCaptcha"] = true;
                }
            }
            TempData.Put("ErrorModel", dto);
            return RedirectToAction("Detail", "Article", new { id = dto.ArticleId });
        }

        public IActionResult Captcha()
        {
            return _captcha.GenerateCaptchaImageFileStream(HttpContext.Session);
        }
    }
}