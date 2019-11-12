using AutoMapper;
using AutoMapper.QueryableExtensions;
using HappyDog.Domain.DataTransferObjects.Comment;
using HappyDog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyDog.Domain.Services
{
    public class CommentService
    {
        public CommentService(HappyDogContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        readonly HappyDogContext _db;
        readonly IMapper _mapper;

        public async Task<Comment> CreateAsync(Comment comment)
        {
            comment.CreateTime = DateTimeOffset.Now;
            await _db.Comments.AddAsync(comment);
            await _db.SaveChangesAsync();
            return comment;
        }

        public async Task<List<CommentDto>> GetCommentDtosAsync(int articleId)
        {
            return await _db.Comments
                .AsNoTracking()
                .Where(c => c.ArticleId == articleId)
                .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
