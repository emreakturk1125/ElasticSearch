﻿using ElasticSearch.API.Enum;

namespace ElasticSearch.API.Models.ModelsForNestLibrary
{
    public class ProductFeature
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public EColor Color { get; set; }
    }
}
