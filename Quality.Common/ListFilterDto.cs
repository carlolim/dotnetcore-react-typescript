using System;
using System.Collections.Generic;
using System.Text;

namespace Quality.Common
{
    public class ListFilterDto
    {
        public int CurrentPage { get; set; }
        public int ItemsCountPerPage { get; set; }
        public string SearchString { get; set; }
    }
}
