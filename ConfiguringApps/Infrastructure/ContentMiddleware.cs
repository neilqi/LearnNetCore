using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ConfiguringApps.Infrastructure
{
    public class ContentMiddleware
    {
        // RequestDelegate对象代表队列中的下一个中间件对象
        // asp.net接收到Http请求时将调用Invoke方法
        // 进入本中间件 https://localhost:5001/middleware
        private RequestDelegate nextDelegate;
        private UptimeService uptimeService;

        public ContentMiddleware(RequestDelegate next, UptimeService up)
        {
            nextDelegate = next;
            uptimeService = up;
        }

        public async Task Invoke(HttpContext httpcontext)
        {
            if(httpcontext.Request.Path.ToString().ToLower() == "/middleware")
            {
                await httpcontext.Response.WriteAsync($"This is from te content middleware. uptime: {uptimeService.Uptime} ms", Encoding.UTF8);
            }
            else
            {
                await nextDelegate.Invoke(httpcontext);
            }
        }
    }
}