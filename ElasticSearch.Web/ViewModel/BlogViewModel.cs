﻿using System.Text.Json.Serialization;
using System;

namespace ElasticSearch.Web.ViewModel
{
    public class BlogViewModel
    { 
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Tags { get; set; } 
        public string UserId { get; set; }
        public string Created { get; set; }
    }
}
