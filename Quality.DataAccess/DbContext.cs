using System;
using System.Collections.Generic;
using System.Text;
using Quality.DataAccess.Entities;

namespace Quality.DataAccess
{
    public class DbContext : IDbContext
    {
        public DbContext(
            IGenericBase<User> _user,
            IGenericBase<Genre> _genre,
            IGenericBase<Novel> _novel,
            IGenericBase<NovelChapter> _novelChapter,
            IGenericBase<NovelGenre> _novelGenre
            )
        {
            User = _user;
            Genre = _genre;
            Novel = _novel;
            NovelChapter = _novelChapter;
            NovelGenre = _novelGenre;
        }

        public IGenericBase<User> User { get; private set; }

        public IGenericBase<Genre> Genre { get; private set; }

        public IGenericBase<Novel> Novel { get; private set; }

        public IGenericBase<NovelChapter> NovelChapter { get; private set; }

        public IGenericBase<NovelGenre> NovelGenre { get; private set; }
    }
}
