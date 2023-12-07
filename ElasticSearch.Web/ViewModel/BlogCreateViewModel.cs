using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElasticSearch.Web.ViewModel
{
    public class BlogCreateViewModel
    {
        [Required]
        [Display(Name = "Blog Title")]
        public string Title { get; set; } = null!;

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; } = null!; 
        public string Tags { get; set; }
    }
}
