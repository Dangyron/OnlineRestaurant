using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.DataAccess.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<OnlineRestaurantDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection"));
    }
);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();