using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomEntityFoundation.WebStarter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            EntityDbContext.Assembles = new String[] { "CustomEntityFoundation" };
            var options = new DatabaseOptions
            {
                ContentRootPath = Directory.GetCurrentDirectory() + "\\App_Data",
            };

            // Sqlite
            options.Database = "Sqlite";
            options.ConnectionString = "Data Source=|DataDirectory|\\Content.db";
            options.ConnectionString = options.ConnectionString.Replace("|DataDirectory|", options.ContentRootPath);
            EntityDbContext.Options = options;
        }
    }
}
