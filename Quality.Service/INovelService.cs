using System.Collections.Generic;
using System.Threading.Tasks;
using Quality.Common;
using Quality.Common.Dto.Novel;
using Quality.Common.Dto.NovelChapter;

namespace Quality.Service
{
    public interface INovelService
    {
        Task<Result> Create(NewNovelDto data);
        Task<List<NovelDto>> All();
        Task<NovelDto> ById(int id);
    }
}