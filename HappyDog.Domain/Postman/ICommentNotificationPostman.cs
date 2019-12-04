using HappyDog.Domain.DataTransferObjects.Comment;
using System.Threading.Tasks;

namespace HappyDog.Domain.Postman
{
    public interface ICommentNotificationPostman
    {
        Task PostAsync(PostCommentDto dto);
    }
}
