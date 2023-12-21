using Controle_de_contato.Data;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Controle_de_contato
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            /*builder.Services.AddDbContextPool<BancoContext>(o => o.UseMySql(Configuration.GetConnectionString("conexaoMySQL")));*/


            /*string mySqlConnection =
                          builder.Configuration.GetConnectionString("conexaoMySQL");

            builder.Services.AddDbContextPool<BancoContext>(options =>
            options.UseMySql(mySqlConnection,
            ServerVersion.AutoDetect(mySqlConnection)));*/

            var connectionString = builder.Configuration.GetConnectionString("MySqlConn");

            builder.Services.AddDbContext<BancoContext>(options =>
            {
                options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));
            });

            builder.Services.AddControllersWithViews();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
        }
    }
}
