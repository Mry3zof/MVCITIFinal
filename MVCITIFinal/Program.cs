using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectName.BLL.Interfaces;
using ProjectName.BLL.Repositories;
using ProjectName.DAL.Database;
using ProjectName.DAL.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to container
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Clean Enhanced Connection String Injection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Dependency Injection (BLL Repository)
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

var app = builder.Build();

// 1. Custom USE Middleware: Add X-Request-Time to response headers
app.Use(async (context, next) =>
{
    context.Response.Headers["X-Request-Time"] = DateTime.UtcNow.ToString("O");
    await next();
});

// 2. Custom MAP Middleware: Branching pipeline for /admin path
app.Map("/admin", adminApp =>
{
    adminApp.Run(async context =>
    {
        await context.Response.WriteAsync("Admin Dashboard");
    });
});

// 3. Custom RUN Middleware: Terminal endpoint for /hello path
app.Map("/hello", helloApp =>
{
    helloApp.Run(async context =>
    {
        await context.Response.WriteAsync("Hello from Middleware");
    });
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();