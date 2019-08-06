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
using SportsStore.Models;
using Microsoft.EntityFrameworkCore;

namespace SportsStore
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                Configuration["Data:SportStoreProduct:ConnectionString"]));

            services.AddTransient<IProductRepository, EFProductRepository>();

            //services.AddTransient<IProductRepository, FakeProductRepository>();
            // 当某个组件，例如控制器，需要实现IProductRepository接口时，它将接受到一个FakeProductRepository对象的实例。
            // AddTransient方法表示每次IProductRepository接口被使用时都要创建一个新的FakeProductRepository对象
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddTransient<ICountryService, CountryService>();
            // 每次请求Cart时，SessionCart.GetCart将被触发。每次Cart类被使用时，都会从session中读取SessionCart对象。
            // 使用lamda的好处，可以通过更灵活的方式创建对象，（比如根据需要组合出这个对象。本例中就是从session中获取，如果session中没有，就创建一个空的）
            // 而不只是简单的映射（映射的作用相当于执行了一个new 方法）
            services.AddScoped(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Transient: 每个请求创建一次对象
            // Scoped：   In ASP.NET Core applications, a scope is created around each server request. 一个客户端创建一个，类似于session
            // Singleton: Specifies that a single instance of the service will be created. 一个应用服务只创建一个，所有客户端来的请求都用这个对象

            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            //app.UseHttpsRedirection();
            //app.UseCookiePolicy();
            app.UseStaticFiles();            
            app.UseStatusCodePages();
            app.UseSession();
            app.UseMvc(routes =>
            {
                //特殊路由必须加到默认路由前面。
                //特殊路由相当于解析出参数，然后按顺序执行到默认路由，完成页面加载

                routes.MapRoute(
                    name:null,
                    template:"{category}/Page{productPage:int}",
                    defaults:new { Controller = "Product", action="List"}
                );

                routes.MapRoute(
                    name:null,
                    template:"Page{productPage:int}",
                    defaults: new { Controller = "Product", action="List", productPage = 1}
                );

                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new
                    {
                        controller = "Product",
                        action = "List",
                        productPage = 1
                    }
                );
                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new
                    {
                        controller = "Product",
                        action = "List",
                        productPage = 1
                    }
                );
                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
            });
            SeedData.EnsurePopulated(app);
        }
    }
}
