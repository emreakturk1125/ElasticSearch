using Elastic.Clients.Elasticsearch;
using Elasticsearch.Net;
using ElasticSearch.API.Extensions;
using ElasticSearch.API.Models;
using ElasticSearch.API.Repositories;
using ElasticSearch.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearch.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Deneme amaçlı aynı görevi gören NEST &  ElasticSearch(yeni)  kütüphenleri eklendi
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        { 
            services.AddControllers(); 
            services.AddScoped<ElasticsearchClient>();                       // ElasticSearch yeni kütüphane 
            services.AddElastic(Configuration);                              // Nest kütüphanesi

            services.AddScoped<IECommerceRepository, ECommerceRepository>();
            services.AddScoped<IProductRepository, ProductRepository>(); 
            services.AddScoped<IProductService, ProductService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1.1" });
            }); 
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1.1"));

            }
             
            app.UseHttpsRedirection();
              
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
