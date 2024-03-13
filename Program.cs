using Web_Scraper_UI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<ScrapService>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("MongoDBConnection");
    var databaseName = configuration["DatabaseName"];
    var collectionName = configuration["CollectionName"];
    return new ScrapService(connectionString, databaseName, collectionName);
});
builder.Services.AddSingleton<DatabaseService>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("MongoDBConnection");
    var databaseName = configuration["DatabaseName"];
    var collectionName = configuration["CollectionName"];
    return new DatabaseService(connectionString, databaseName, collectionName);
});
builder.Services.AddSingleton<FilteringService>();
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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
