﻿using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;

namespace ElasticSearch.Web.ViewModel
{
    public class SearchPageViewModel
    {
        public long TotalCount { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public long PageLinkCount { get; set; }
        public List<ECommerceViewModel> List { get; set; }
        public ECommerceSearchViewModel SearchViewModel { get; set; }
        public string CreatePageUrl(HttpRequest request, int page, int pageSize)
        {
            var currentUrl = new Uri($"{request.Scheme}://{request.Path}{request.QueryString}").AbsoluteUri;
            if (currentUrl.Contains("page",StringComparison.OrdinalIgnoreCase))
            {
                currentUrl = currentUrl.Replace($"Page={Page}", $"Page={page}", StringComparison.OrdinalIgnoreCase);
                currentUrl = currentUrl.Replace($"PageSize={PageSize}", $"PageSize={pageSize}", StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                currentUrl = $"{currentUrl}?Page={page}";
                currentUrl = $"{currentUrl}?PageSize={pageSize}";
            }
            return currentUrl;
        }
    }
}