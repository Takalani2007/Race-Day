using Race_Day.Services;

var builder = WebApplication.CreateBuilder(args);

// Register MVC and ignore JSON cycles
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });


// Get the API URL from Docker environment, or fallback to localhost for local run
var apiUrl = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7011/";

builder.Services.AddHttpClient<RaceEventApiService>(client =>
{
    client.BaseAddress = new Uri(apiUrl);
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    // This ignores the SSL certificate error when running inside Docker
    return new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
    };
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();