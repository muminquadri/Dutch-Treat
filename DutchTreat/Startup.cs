﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using NLog.Web;

namespace DutchTreat
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DutchContext>(cfg =>
            {
                cfg.UseSqlServer(_config.GetConnectionString("DutchConnectionString"));
            }
                );
            services.AddTransient<IMailService, NullMailService>();
            services.AddTransient<DutchSeeder>();
            services.AddScoped<IDutchRepository, DutchRepository>();
            services.AddMvc()
                .AddJsonOptions(o=>o.SerializerSettings.ReferenceLoopHandling=ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
          //  app.UseDefaultFiles(); this opens index.html, we dont need that.
            app.UseStaticFiles();
            env.ConfigureNLog("nlog.config");
            loggerFactory.AddNLog();
          //  we need projection.projection transforms a source to a destination beyond
             //flattening the object model, so we must specify this through custom member mapping.
             //to do that we call into ForMember and give (destination property name, how to calculate the value)
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<OrderViewModel, Order>()
                .ForMember(o=>o.Id, src=>src.MapFrom(o=>o.OrderId))
                .ReverseMap();
                cfg.CreateMap<OrderItem, OrderItemViewModel>()
                .ReverseMap();
            });
            app.UseMvc(cfg =>
            {
                cfg.MapRoute("Default",
                    "{controller}/{Action}/{id?}", new { controller = "App", Action = "Index" });
            });
            if (env.IsDevelopment())
            {
                using(var scope = app.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetService<DutchSeeder>();
                    seeder.Seed();
                }
            }
        }
    }
}
