using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quality.DataAccess.Entities
{
    [Table("NovelChapter")]
    public class NovelChapter
    {
        [Key]
        public int NovelChapterId { get; set; }
        public int NovelId { get; set; }
        public string Title { get; set; }
        public bool IsPublished { get; set; }
        public string Contents { get; set; }
        public int ChapterNumber { get; set; }
    }

}
