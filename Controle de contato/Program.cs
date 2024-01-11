using Controle_de_contato.Data;
using Controle_de_contato.Helper;
using Controle_de_contato.Repositorio;
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

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<ISessao, Sessao>();
            builder.Services.AddScoped<IEmail, Email>();

            builder.Services.AddSession(o =>
            {
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential = true;
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

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
