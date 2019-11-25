using AutoMapper;
using Quality.Common.Dto.Genre;
using Quality.Common.Dto.Novel;
using Quality.Common.Dto.NovelChapter;
using Quality.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quality
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Novel, NovelDto>().ReverseMap();
            CreateMap<Novel, NewNovelDto>().ReverseMap();
            CreateMap<NovelChapter, NovelChapterDto>().ReverseMap();
            CreateMap<NovelChapter, NewChapterDto>().ReverseMap();
            CreateMap<Genre, NewGenreDto>().ReverseMap();
        }
    }
}
