using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ConfiguringApps.Infrastructure 
{
    public class ShortCircuitMiddleware 
    {
        private RequestDelegate nextDelegate;

        public ShortCircuitMiddleware(RequestDelegate next) => nextDelegate = next;

        public async Task Invoke(HttpContext httpContext) 
        {
            var testobj = httpContext.Items["EdgeBrowser"];
            var test = testobj as bool? == true;
            if (httpContext.Items["EdgeBrowser"] as bool? == true) 
            {
                httpContext.Response.StatusCode = 403;
            } 
            else
            {
                await nextDelegate.Invoke(httpContext);
            }
        }
    }
}