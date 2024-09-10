using DATN_LiBrary.Models;
using DATN_LiBrary.Responsive;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("DoanContext");
builder.Services.AddDbContext<DoanContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddScoped<ITheLoaiResponsive, TheLoaiResponsive>();
builder.Services.AddSession();
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
app.UseSession();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapControllerRoute(
//        name: "login",
//        pattern: "Home/Login",
//        defaults: new { controller = "Home", action = "Login" });
//app.MapControllerRoute(
//                name: "areas",
//                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{

    //endpoints.MapControllerRoute(
    //    name: "areas",
    //    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    //    );
    endpoints.MapControllerRoute(
        name: "login",
        pattern: "{controller=Home}/{action=Login}"
        );
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
        );
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=HomeAdmin}/{action=Edit}/{id?}"
        );
});

app.Run();
