using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace ElasticSearch.Web.ViewModel
{
    public class BlogCreateViewModel
    {  
        public string Title { get; set; } = null!; 
        public string Content { get; set; } = null!; 
        public List<string> Tags { get; set; } = new();  
    }
}
