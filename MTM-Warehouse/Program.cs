using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
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

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false; 
    options.Password.RequiredUniqueChars = 0;
}).AddEntityFrameworkStores<AllDbContext>().AddDefaultTokenProviders();

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {       
//        options.AccessDeniedPath = "/Login/AccessDenied"; 
//    });
//options.LoginPath = "/Login/LoginPage"; 

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


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=LoginPage}/{id?}");

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    await AllDbContext.CreateAdminUser(scope.ServiceProvider);
}

app.Run();
