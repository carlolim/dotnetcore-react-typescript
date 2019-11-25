using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quality.DataAccess.Entities
{
    [Table("NovelGenre")]
    public class NovelGenre
    {
        public int NovelId { get; set; }
        public int GenreId { get; set; }
    }

}
