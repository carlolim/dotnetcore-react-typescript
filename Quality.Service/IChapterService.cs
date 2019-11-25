using System.Collections.Generic;
using System.Threading.Tasks;
using Quality.Common;
using Quality.Common.Dto.NovelChapter;

namespace Quality.Service
{
    public interface IChapterService
    {
        Task<Result> Create(NewChapterDto data);
        Task<List<NovelChapterDto>> ByNovelId(int id);
    }
}