using AutoMapper;
using Quality.Common;
using Quality.Common.Dto.Novel;
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
    public class NovelService : INovelService
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public NovelService(IDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<NovelDto>> All()
        {
            var novels = (await _dbContext.Novel.All()).ToList();
            return _mapper.Map<List<Novel>, List<NovelDto>>(novels);
        }

        public async Task<NovelDto> ById(int id)
        {
            var novel = await _dbContext.Novel.GetSingleFilteredByAsync(m => m.NovelId == id);
            return _mapper.Map<Novel, NovelDto>(novel);
        }

        public async Task<Result> Create(NewNovelDto data)
        {
            var result = new Result();

            // validate required fields
            if (string.IsNullOrWhiteSpace(data.Author))
                result.Message = "Author is required";
            else if (string.IsNullOrWhiteSpace(data.Description))
                result.Message = "Description is required";
            else if (string.IsNullOrWhiteSpace(data.Title))
                result.Message = "Title is required";
            else
            {
                var novel = _mapper.Map<Novel>(data);
                result = await _dbContext.Novel.Insert(novel);
                if (result.IsSuccess) result.Message = $"Novel '{data.Title}' added!";
            }
            return result;
        }
    }
}
