using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Vậy là mỗi lần tạo ra mộ custom Middleware , Ta sẽ tạo ra một class như này
namespace ASP.NET.basic
{
    public class CustomMiddleware
    {
        // Con trỏ next 
        // Con trỏ next có kiểu RequestDelegate
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // Để kiểu trả về là Task ở đầu thì nghĩa là return có thể là void (Không nhất thiết phải return)
        public async Task InvokeAsync(HttpContext context) // Tên bắt buộc phải để thế này, không thì không chạy được
        {
            await Task.Run(async () => {
                await context.Response.WriteAsync("Scheme : " + context.Request.Scheme + "\n");
                await context.Response.WriteAsync("Host : " + context.Request.Host.ToString() + "\n");
                await context.Response.WriteAsync("Path : " + context.Request.Path + "\n");
                await context.Response.WriteAsync("QueryString : " + context.Request.QueryString.ToString() + "\n");
                await context.Response.WriteAsync("Body : " + context.Request.Body.ToString() + "\n");
            });
            // Call the next delegate/middleware in the pipeline.
            // await _next(context); 
        }
    }

}