using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using BookStore.Data;
using BookStore.Data.Caching;
using BookStore.Data.Services.DataServices;
using BookStore.Data.Services.DataServices.Interfaces;
using BookStore.Web.Mapping;
using BookStore.Web.Models.Authors;
using BookStore.Web.Validation.Authors;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookStore.Web
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
            services.AddControllersWithViews()
                .AddFluentValidation();

            // DbContext
            services.AddDbContext<BookStoreDatabase>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("BookStoreContext")), ServiceLifetime.Singleton);

            // Repository
            services.AddSingleton(typeof(IRepository<>), typeof(EfRepository<>));

            // Memory cache manager
            services.AddSingleton(typeof(MemoryCacheManager));

            // data services
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<IBookService, BookService>();

            // automapper
            services.AddAutoMapper(cfg =>
            {
                cfg.AddCollectionMappers();
                cfg.UseEntityFrameworkCoreModel<BookStoreDatabase>(services);
            },
            Assembly.GetAssembly(typeof(BookStoreMappingProfile)));

            // validators
            services.AddTransient<IValidator<AuthorModel>, AuthorModelValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
