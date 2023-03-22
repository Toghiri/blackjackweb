var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddRazorPages
builder.Services.AddMvc(option => option.EnableEndpointRouting = false);
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseMvc();

app.Run();
