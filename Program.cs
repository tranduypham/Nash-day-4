using ASP.NET.basic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// app.Map("/mapping", MyMapMiddleware);
// app.Use(async (context, next) => {
//     await context.Response.WriteAsync("This is USE middleware speaking");
//     await next();
// });
// app.Map("/logging", LoggingMiddleware);
// app.Use(MyRunMiddleware);
// app.Use(MyUseMiddleware);


app.UseMiddleware<CustomMiddleware>(); //Sử dụng middleware bằng lệnh UseMiddleware<Tên class middleware>
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Task MyRunMiddleware(HttpContext context){
//     return context.Response.WriteAsync("This is run middleware speaking");
// }
// Task MyMapMiddleware(HttpContext context){
//     return context.Response.WriteAsync("This is MAP middleware speaking");
// }
// Task MyUseMiddleware(HttpContext context, Func<Task> next){
//     context.Response.WriteAsync("This is USE middleware speaking");
//     return next();
// }
// Task LoggingMiddleware(HttpContext context){
//     var responses = new String[]{
//         "Scheme : " + context.Request.Scheme,
//         "Host : " + context.Request.Host.ToString(),
//         "Path : " + context.Request.Path,
//         "QueryString : " + context.Request.QueryString.ToString(),
//         "Body : " + context.Request.Body.ToString()
//     };
//     String responseString = "";
//     foreach(String response in responses){
//         responseString += "\n" + response + "\n";
//     }
//     return context.Response.WriteAsync(responseString);
// }