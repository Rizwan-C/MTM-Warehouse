using Microsoft.EntityFrameworkCore;
using MTM_Warehouse.Entities;
using MTM_Warehouse.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IWarehouseService, WarehouseService>();

builder.Services.AddDistributedMemoryCache(); // Needed to store session in memory
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(720); // 12 hours timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DB context service:
string connStr = builder.Configuration.GetConnectionString("DbConnString");
builder.Services.AddDbContext<AllDbContext>(options => options.UseSqlServer(connStr));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=LoginPage}/{id?}");

app.Run();
