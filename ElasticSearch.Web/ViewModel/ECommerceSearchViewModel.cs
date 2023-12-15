using System;
using System.ComponentModel.DataAnnotations;

namespace ElasticSearch.Web.ViewModel
{
    public class ECommerceSearchViewModel
    {
        [Display(Name = "Category")]
        #nullable enable
        public string? Category { get; set; }

        [Display(Name = "Gender")]
        #nullable enable
        public string? Gender { get; set; }

        [Display(Name = "Order Date (Start)")]
        [DataType(DataType.Date)]
        public DateTime? OrderDateStart { get; set; }

        [Display(Name = "Order Date (End)")]

        [DataType(DataType.Date)] 
        public DateTime? OrderDateEnd { get; set; }

        [Display(Name = "Customer Full Name")]
        #nullable enable
        public string? CustomerFullName { get; set; }
    }
}
