using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.DataAccess.Data;
using OnlineRestaurant.DataAccess.Repository;
using OnlineRestaurant.DataAccess.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<OnlineRestaurantDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection"));
    }
);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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