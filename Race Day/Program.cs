using Race_Day.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// 2. Register the API Service
builder.Services.AddHttpClient<RaceEventApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7011/");
});

var app = builder.Build();

// 3. Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // For CSS, JS, and images

app.UseRouting();

app.UseAuthorization();

// 4. Set up the default MVC route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Events}/{action=Index}/{id?}");

app.Run();
