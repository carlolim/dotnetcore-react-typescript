
using Quality.DataAccess.Entities;

namespace Quality.DataAccess
{
    public interface IDbContext
    {
        IGenericBase<User> User { get; }
        IGenericBase<Genre> Genre { get; }
        IGenericBase<Novel> Novel { get; }
        IGenericBase<NovelChapter> NovelChapter { get; }
        IGenericBase<NovelGenre> NovelGenre { get; }
    }
}