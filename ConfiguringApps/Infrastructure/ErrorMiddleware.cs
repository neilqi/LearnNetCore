using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ConfiguringApps.Infrastructure
{
    public class ErrorMiddleware
    {
        private RequestDelegate nextDelegate;

        public ErrorMiddleware(RequestDelegate next)
        {
            nextDelegate = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await nextDelegate.Invoke(httpContext);
            switch (httpContext.Response.StatusCode)
            {
                case 403:
                    await httpContext.Response.WriteAsync("IE not supported", Encoding.UTF8);
                    break;
                case 404:
                    await httpContext.Response.WriteAsync("No content middleware response", Encoding.UTF8);
                    break;
            }
        }
    }
}
