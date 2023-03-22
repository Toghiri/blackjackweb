var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

// app.UseAuthorization();

// app.MapRazorPages();

app.Use(async (HttpContext context, Func<Task> next) =>
{
    // The execution reach this point only if no one was able to serve the request
    // This means that we are in one of the following cases: 
    //  1) The user has request a non existing file/api
    //  2) The user has requested a Vue sub route
    //  3) An external provider wants to start the web app from url different from /
    //
    // If we are in the first case we return a 404, in the other two we redirect to /index.htm
    // preserving the query string

    var path = context.Request.Path.Value;

    var redirects = new string[]
    {
        "/"
    };

    if (redirects.Contains(path))
    {
        // Redirect to index.html and keep the query string
        var queryParams = context.Request.Query.ToDictionary(x => x.Key, x => x.Value.ToString());
        var redirectUrl = Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString("/index.html", queryParams);
        //context.Request.Path = redirectUrl;

        context.Response.Redirect(redirectUrl);
    }
    else
    {
        context.Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound;
    }
});

app.Run();
