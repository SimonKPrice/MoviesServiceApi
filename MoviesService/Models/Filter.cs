using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesService.Models
{
    public class Filter
    {
        public string Title { get; set; }
        public int? YearOfRelease { get; set; }
        public List<string> Genres { get; set; }
    }
}