using CoursePlatform.Common;
using CoursePlatform.Common.Entities;
using CoursePlatform.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddPlatform();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            builder.Services.AddDbContext<CoursePlatformContext>();

            var app = builder.Build();
          
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            using(var scope = app.Services.CreateScope()) 
            {
                var provider = scope.ServiceProvider;
                using (var context = new CoursePlatformContext(provider))
                {
                    if (context.Database.GetPendingMigrations().Any())
                        context.Database.Migrate();
                }

            }

            app.Run();
        }
    }
}
