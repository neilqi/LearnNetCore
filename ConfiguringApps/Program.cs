using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace ConfiguringApps
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            // 在Asp.Net Core中，我们的web application 其实是运行在Kestrel服务上，
            // 它是一个基于libuv开源的跨平台可运行 Asp.Net Core 的web服务器
            return new WebHostBuilder()
                .UseKestrel()   //指定使用Kestrel作为web服务器
                .UseContentRoot(Directory.GetCurrentDirectory())    //指定根节点
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                    if (env.IsDevelopment())
                    {
                        var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                        if (appAssembly != null)
                        {
                            config.AddUserSecrets(appAssembly, optional: true);
                        }
                    }
                    config.AddEnvironmentVariables();
                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                //.ConfigureLogging((hostingContext, logging) =>
                //{
                //    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                //    logging.AddConsole();
                //    logging.AddDebug();
                //})
                .UseIISIntegration()
                //.UseDefaultServiceProvider((context, options) =>
                //{
                //    // 用于配置依赖注入
                //    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                //})
                .UseStartup<Startup>();

            /*
             * 最基本的设置如下即可
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()

                当应用启动时，.net core 创建一个新的startup实例并调用它的ConfigureService方法以便创建service，
                服务是一种对象，给应用的其他部分提供功能。
                服务创建后，.net调用Configure方法。Configure方法的作用是设置请求管道。
                请求管道是一组被称为中间件的组件，用于处理接收的Http请求，生成响应。

             */
        }
    }
}
