using ElasticSearch.Web.Models;
using System.Text.Json.Serialization;
using System;

namespace ElasticSearch.Web.ViewModel
{
    public class ECommerceViewModel
    {
        public string Category { get; set; }
        public string CustomerFirstName { get; set; } = null!;
        public string CustomerFullName { get; set; } = null!;
        public string CustomerLastName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Id { get; set; }
        public string OrderDate { get; set; }
        public int OrderId { get; set; }
        public double TaxfulTotalPrice { get; set; }
    }
}
