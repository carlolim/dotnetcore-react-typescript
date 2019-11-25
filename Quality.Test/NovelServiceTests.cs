using AutoMapper;
using Quality.Service;
using Quality.Test.DataAccess;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using Quality.DataAccess.Entities;
using Quality.Common.Dto.Novel;

namespace Quality.Test
{
    [TestFixture]
    public class NovelServiceTests
    {
        private readonly MockDbContext _dbContext;
        private readonly INovelService _novelService;

        public NovelServiceTests()
        {
            _dbContext = new MockDbContext();
            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);
            _novelService = new NovelService(_dbContext.DbContext, mapper);
        }



        [Test]
        public async Task GetAllNovels_ShouldReturnListOfNovels()
        {
            var novels = await _novelService.All();
            Assert.True(novels.ToList().Any());
        }

        [Test]
        public async Task CreateNewNovel_ShouldReturnTrue()
        {
            var novel = new NewNovelDto
            {
                Author = "CreateNewNovel_ShouldInsertToMockData",
                Description = "CreateNewNovel_ShouldInsertToMockData",
                Title = "CreateNewNovel_ShouldInsertToMockData",
            };

            var result = await _novelService.Create(novel);

            var all = await _novelService.All();
            var novelFromMockDb = all.FirstOrDefault(m => m.Title == "CreateNewNovel_ShouldInsertToMockData");
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(novelFromMockDb);
        }

        [Test]
        public async Task CreateNewNovel_MissingRequiredData_ShouldReturnFalse()
        {
            var novel = new NewNovelDto
            {
                Author = "",
                Description = "",
                Title = "CreateNewNovel_WrongInput_ShouldReturnFalse",
            };

            var result = await _novelService.Create(novel);
            Assert.IsFalse(result.IsSuccess);
        }
    }
}
