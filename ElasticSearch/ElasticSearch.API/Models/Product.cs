﻿using System.Text.Json.Serialization;

namespace ElasticSearch.API.Models
{
    public class Product
    {
        [JsonPropertyName("product_id")]
        public long ProductId { get; set; }

        [JsonPropertyName("product_name")]
        public string ProductName { get; set; } 
    }
}
