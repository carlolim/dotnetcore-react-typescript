using AutoMapper;
using NUnit.Framework;
using Quality.Common.Dto.NovelChapter;
using Quality.Service;
using Quality.Test.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quality.Test
{
    [TestFixture]
    public class ChapterServiceTests
    {
        private readonly MockDbContext _dbContext;
        private readonly IChapterService _chapterService;

        public ChapterServiceTests()
        {
            _dbContext = new MockDbContext();
            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);
            _chapterService = new ChapterService(_dbContext.DbContext, mapper);
        }

        [Test]
        public async Task GetAllChaptersOfNovel_ShouldPass()
        {
            var novelId1Chapters = await _chapterService.ByNovelId(1);
            var novelId20Chapters = await _chapterService.ByNovelId(20);
            Assert.IsTrue(novelId1Chapters.Any());
            Assert.IsFalse(novelId20Chapters.Any());
        }

        [Test]
        public async Task CreateNew_ValidData_ShouldReturnTrue()
        {
            var chapter = new NewChapterDto
            {
                ChapterNumber = 6,
                Contents = "CreateNew_ValidData_ShouldReturnTrue",
                IsPublished = false,
                NovelId = 1,
                Title = "CreateNew_ValidData_ShouldReturnTrue"
            };

            var result = await _chapterService.Create(chapter);
            var novelChapters = await _chapterService.ByNovelId(1);
            var chapterFromDb = novelChapters.FirstOrDefault(m => m.Title == "CreateNew_ValidData_ShouldReturnTrue");

            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(chapterFromDb);
        }

        [Test]
        public async Task CreateNew_InvalidData_ShouldReturnFalse()
        {
            var chapter = new NewChapterDto
            {
                ChapterNumber = 6,
                Contents = "",
                IsPublished = false,
                NovelId = 1,
                Title = ""
            };

            var result = await _chapterService.Create(chapter);
            Assert.IsFalse(result.IsSuccess);
        }
    }
}
