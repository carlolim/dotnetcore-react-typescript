using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quality.DataAccess.Entities
{
    [Table("Novel")]
    public class Novel
    {
        [Key]
        public int NovelId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
    }

}
