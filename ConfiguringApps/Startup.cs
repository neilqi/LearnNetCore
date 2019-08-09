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
using ConfiguringApps.Infrastructure;


namespace ConfiguringApps
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

            services.AddSingleton<UptimeService>();
                  
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // IApplicationBuilder: 定义设置应用中间件时必须的功能
            // IHostingEnvrionment: 定义生产环境和开发环境有区别的功能
            // IsDevelopment()   IsStaging()   IsProduction() 分别对应三种默认环境
            // IsEnvironment(env) 是否是指定名称的环境
            if (env.IsDevelopment())
            {
                /*
                 UseDeveloperExceptionPage方法设置了错误处理中间件，用于显示响应中的异常详情，包括异常跟踪。
                 这些信息不应该显示给用户，所以应该仅限于开发环境才调用该方法
                 */
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                //app.UseMiddleware<ContentMiddleware>();
                //app.UseMiddleware<BrowserTypeMiddleware>();
                //app.UseMiddleware<ShortCircuitMiddleware>();
                //app.UseMiddleware<ErrorMiddleware>();
                app.UseBrowserLink();       //作用： 同时刷新多个浏览器， 需要安装 Microsoft.VisualStudio.Web.BrowserLink;
            }
            else
            {
                /*
                 对于预发布或生产环境，应使用UseExceptionHandler方法。用于显示定制的错误信息，而不会揭示应用执行的内部细节。
                 参数中的url为错误跳转页面，在该页中显示错误信息。
                 */
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
        }
    }
}
