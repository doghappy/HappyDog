using HappyDog.Domain.DataTransferObjects.Tag;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HappyDog.Domain.IServices
{
    public interface ITagService
    {
        Task<TagDto> PostAsync(PostTagDto dto);
        Task<TagDto> GetTagDtoAsync(int id);
        Task<List<TagDto>> GetTagsDtoAsync();
        Task<List<TagDto>> SearchTagsDtoAsync(string q);
        Task<TagDto> PutAsync(int id, PutTagDto dto);
    }
}
