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
MyMiddleware myMiddleware = new MyMiddleware();
app.Map("/mapping", myMiddleware.MyMapMiddleware);
// app.Use(async (context, next) => {
//     await context.Response.WriteAsync("This is USE middleware speaking");
//     await next();
// });
app.Map("/logging", myMiddleware.LoggingMiddleware);
// app.Use(myMiddleware.MyUseMiddleware);
// app.Run(myMiddleware.MyRunMiddleware);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

class MyMiddleware{
    public Task MyRunMiddleware(HttpContext context){
        return context.Response.WriteAsync("This is run middleware speaking");
    }
    public Task MyMapMiddleware(HttpContext context){
        return context.Response.WriteAsync("This is MAP middleware speaking");
    }
    public Task MyUseMiddleware(HttpContext context, Func<Task> next){
        context.Response.WriteAsync("This is USE middleware speaking");
        return next();
    }
    public Task LoggingMiddleware(HttpContext context){
        var responses = new String[]{
            "Scheme : " + context.Request.Scheme,
            "Host : " + context.Request.Host.ToString(),
            "Path : " + context.Request.Path,
            "QueryString : " + context.Request.QueryString.ToString(),
            "Body : " + context.Request.Body.ToString()
        };
        String responseString = "";
        foreach(String response in responses){
            responseString += "\n" + response + "\n";
        }
        return context.Response.WriteAsync(responseString);
    }
}