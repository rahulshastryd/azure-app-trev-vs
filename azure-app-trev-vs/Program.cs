using azure_app_trev_vs.Data;
using Microsoft.EntityFrameworkCore;

namespace azure_app_trev_vs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("AzureSqlConnection");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString,sqlOptions => sqlOptions.EnableRetryOnFailure()));

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddApplicationInsightsTelemetry();

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

            app.Run();
        }
    }
}