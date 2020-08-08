using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookStore.Data;
using Microsoft.EntityFrameworkCore;
using BookStore.Data.Interface;
using BookStore.Data.Mocks;
using BookStore.Data.Repositories;
using BookStore.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Identity;

namespace BookStore
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
            //Database Configuration
            services.AddDbContext<BookStoreContext>(op => op.UseSqlServer(Configuration.GetConnectionString("BookStoreConString")));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //Identity Configuration
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<BookStoreContext>();

            //Repository Configuration
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IGuestMessage, GuestMessageRepo>();

            //For Shoping Cart
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => ShopingCart.GetShopingCart(sp));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Temporary Service for Shoping Cart using session
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(120);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseAuthentication();
            /*
             * Can use instead 
             * app.UseIdentity();
             * app.UseCookieAuthentication();
             */

            app.UseMvc(routes =>
            {                
                routes.MapRoute(
                    name: "GalleryView",
                    template: "Book/{action}/{category?}",
                    defaults: new { Controller = "Book", Action = "Gallery"});
                routes.MapRoute(
                      name: "MyArea",
                      template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
