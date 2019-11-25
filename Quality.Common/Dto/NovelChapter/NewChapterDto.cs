using System;
using System.Collections.Generic;
using System.Text;

namespace Quality.Common.Dto.NovelChapter
{
    public class NewChapterDto
    {
        public int NovelChapterId { get; set; }
        public int NovelId { get; set; }
        public string Title { get; set; }
        public int ChapterNumber { get; set; }
        public string Contents { get; set; }
        public bool IsPublished { get; set; }
    }
}
