using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HappyDog.Domain.DataTransferObjects.Comment;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using HappyDog.WebUI.Extensions;

namespace HappyDog.WebUI.Controllers
{
    public class CommentController : Controller
    {
        public CommentController(IMapper mapper, CommentService commentService)
        {
            _mapper = mapper;
            _commentService = commentService;
        }

        readonly IMapper _mapper;
        readonly CommentService _commentService;

        public async Task<IActionResult> Post(PostCommentDto dto)
        {
            if (ModelState.IsValid)
            {
                var comment = _mapper.Map<Comment>(dto);
                comment.IPv4 = Request.Host.ToString();
                await _commentService.CreateAsync(comment);
            }
            return RedirectToAction("Detail", "Article", new { id = dto.ArticleId });
        }
    }
}