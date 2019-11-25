using Quality.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quality.Test.DataAccess
{
    public class TestData
    {
        public static IEnumerable<Novel> Novels()
        {
            for(int i = 0; i <= 50; i++)
            {
                yield return new Novel
                {
                    Author = $"Author { i }",
                    Description = $"Description {i}",
                    NovelId = i,
                    Title = $"Novel title {i}"
                };
            }
        }
        public static IEnumerable<Genre> Genres()
        {
            for (int i = 0; i <= 20; i++)
            {
                yield return new Genre
                {
                    GenreId = i,
                    Name = $"Genre { i }"
                };
            }
        }

        /// <summary>
        /// Creates 5 sample chapters for novels with id 1 to 10
        /// chapters 1, 2, and 3 are published, chapters 4 and 5 are not
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<NovelChapter> Chapters()
        {
            for (int novelId = 1; novelId <= 10; novelId++)
            {
                for(int chapterId = 1; chapterId <= 5; chapterId++)
                {
                    yield return new NovelChapter
                    {
                        ChapterNumber = chapterId,
                        Contents = $"Contents",
                        IsPublished = chapterId >= 4,
                        NovelChapterId = chapterId,
                        NovelId = novelId,
                        Title = $"Novel title {novelId} - Chapter ${chapterId}"
                    };
                }
            }
        }
    }
}
