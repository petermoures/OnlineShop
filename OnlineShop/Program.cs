using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            string connectionString = builder.Configuration.GetConnectionString("BS");
            builder.Services.AddDbContext<OnlineShopeEntity>(OptionsBuilder => OptionsBuilder.UseSqlServer(connectionString));
            builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<OnlineShopeEntity>();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User/SignIn";
            });
                
                var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}/{userId?}");

            app.Run();
        }
    }
}