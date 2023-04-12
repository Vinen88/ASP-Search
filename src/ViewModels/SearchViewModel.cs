using System;
using System.Collections.Generic;
using ASPSearch.Models;

namespace ASPSearch.ViewModels
{
    public class SearchViewModel
    {
        public string Term { get; set; }
        public List<Cars> Results { get; set; }
        public int Page { get; set; }
    }
}
