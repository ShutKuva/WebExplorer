using BLL;
using DAL;
using Microsoft.EntityFrameworkCore;
using WebExplorer.Extensions;
using WebExplorer.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ExplorerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ExplorerString")));
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<CatalogProfile>();
});

builder.Services.AddScoped<DBCatalogService>();

var app = builder.Build();

app.MigrateDatabase();

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
