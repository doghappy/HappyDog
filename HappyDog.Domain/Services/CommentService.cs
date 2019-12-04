using AutoMapper;
using AutoMapper.QueryableExtensions;
using HappyDog.Domain.DataTransferObjects.Comment;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using HappyDog.Domain.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyDog.Domain.Postman;

namespace HappyDog.Domain.Services
{
    public class CommentService : ICommentService
    {
        public CommentService(HappyDogContext db, IMapper mapper, ICommentNotificationPostman commentNotificationPostman)
        {
            _db = db;
            _mapper = mapper;
            _commentNotificationPostman = commentNotificationPostman;
        }

        readonly HappyDogContext _db;
        readonly IMapper _mapper;
        readonly ICommentNotificationPostman _commentNotificationPostman;

        public async Task<CommentDto> CreateAsync(PostCommentDto dto)
        {
            var comment = _mapper.Map<Comment>(dto);
            comment.CreateTime = DateTimeOffset.Now;
            comment.Status = BaseStatus.Enabled;
            await _db.Comments.AddAsync(comment);
            await _db.SaveChangesAsync();

            await Task.Factory.StartNew(async () => await _commentNotificationPostman.PostAsync(dto));

            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<List<CommentDto>> GetCommentDtosAsync(int articleId)
        {
            return await _db.Comments
                .AsNoTracking()
                .Where(c => c.ArticleId == articleId && c.Status == BaseStatus.Enabled)
                .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
