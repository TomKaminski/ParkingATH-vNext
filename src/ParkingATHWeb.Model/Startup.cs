﻿using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace ParkingATHWeb.Model
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
#if DEBUG
                services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ParkingAthContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:LocalConnectionString"]));
#else
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ParkingAthContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:AzureConnectionString"]));
#endif
        }
        public void Configure(IApplicationBuilder app)
        {
        }
    }
}
