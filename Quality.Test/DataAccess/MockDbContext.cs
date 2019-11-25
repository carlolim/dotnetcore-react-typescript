using Moq;
using Quality.DataAccess;
using Quality.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quality.Test.DataAccess
{
    public class MockDbContext
    {
        public readonly IDbContext DbContext;
        private readonly Mock<IDbContext> _moqDbContext = new Mock<IDbContext>();

        public MockDbContext()
        {
            var novel = new MockGenericDataAccess<Novel>(TestData.Novels());
            var genre = new MockGenericDataAccess<Genre>(TestData.Genres());
            var chapter = new MockGenericDataAccess<NovelChapter>(TestData.Chapters());
            _moqDbContext.Setup(m => m.Novel).Returns(novel.DataAccess);
            _moqDbContext.Setup(m => m.Genre).Returns(genre.DataAccess);
            _moqDbContext.Setup(m => m.NovelChapter).Returns(chapter.DataAccess);
            DbContext = _moqDbContext.Object;
        }
    }
}
