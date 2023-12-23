using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using MVCProject.Exceptions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog yap�land�rmas�
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog(); // Serilog'u kullanmak i�in bu sat�r� ekledim

//Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBMAY9C3t2VlhiQlVPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSHxSf0VkWXxbcHFTRWU=");

// Identity services
builder.Services.AddDbContext<ContextDal>();
builder.Services.AddIdentity<User, UserRole>(options =>
{
    //Identity �ifre politikalar�n� burada yapt�m
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6; // Minimum �ifre uzunlu�u
}).AddEntityFrameworkStores<ContextDal>().AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

// Session etkinle�tirme
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
//Excepiton yaklamak i�in
app.UseMiddleware<ExceptionHandlerMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404/");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

app.UseSession(); // Session kullan�m� i�in ekledik

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();
