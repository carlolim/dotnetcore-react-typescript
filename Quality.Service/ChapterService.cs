using AutoMapper;
using Quality.Common;
using Quality.Common.Dto.NovelChapter;
using Quality.DataAccess;
using Quality.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quality.Service
{
    public class ChapterService : IChapterService
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public ChapterService(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<NovelChapterDto>> ByNovelId(int id)
        {
            var chapters = await _dbContext.NovelChapter.GetAllFilteredBy(m => m.NovelId == id);
            return _mapper.Map<List<NovelChapter>, List<NovelChapterDto>>(chapters);
        }

        public async Task<Result> Create(NewChapterDto data)
        {
            var result = new Result();

            if (string.IsNullOrWhiteSpace(data.Title))
            {
                result.IsSuccess = false;
                result.Message = "Chapter title is required";
            }
            else if (string.IsNullOrWhiteSpace(data.Contents))
            {
                result.IsSuccess = false;
                result.Message = "Chapter content is required";
            }
            else if (data.ChapterNumber == 0)
            {
                result.IsSuccess = false;
                result.Message = "Chapter number is required";
            }
            else if (data.NovelId == 0)
            {
                result.IsSuccess = false;
                result.Message = "Novel ID is required";
            }
            else
            {
                int novelId = data.NovelId;
                int chapterNumber = data.ChapterNumber;
                var novel = await _dbContext.Novel.GetSingleFilteredByAsync(m => m.NovelId == novelId);
                var existingChapter = await _dbContext.NovelChapter.GetSingleFilteredByAsync(m => m.ChapterNumber == chapterNumber && m.NovelId == novelId);
                if (existingChapter != null)
                {
                    result.IsSuccess = false;
                    result.Message = $"Chapter number {data.ChapterNumber} already exist in novel";
                }
                else if (novel == null)
                {
                    result.IsSuccess = false;
                    result.Message = $"Novel with ID ${data.NovelId} does not exist";
                }
                else
                {
                    var chapter = _mapper.Map<NovelChapter>(data);
                    result = await _dbContext.NovelChapter.Insert(chapter);
                    if (result.IsSuccess) result.Message = "Chapter added successfully!";
                }
            }

            return result;
        }
    }
}
