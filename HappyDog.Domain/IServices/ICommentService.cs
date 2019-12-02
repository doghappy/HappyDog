using HappyDog.Domain.DataTransferObjects.Comment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HappyDog.Domain.IServices
{
    public interface ICommentService
    {
        Task<CommentDto> CreateAsync(PostCommentDto dto);
        Task<List<CommentDto>> GetCommentDtosAsync(int articleId);
     }
}
