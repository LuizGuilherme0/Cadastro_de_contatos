



using Controle_de_contato.Models;

namespace Controle_de_contato.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {

        }

        public DbSet<ContatoModel> Contatos {  get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }  
}
