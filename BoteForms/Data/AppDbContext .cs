using System.Data.Entity;
using BoteForms.modelo;

namespace BoteForms.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=CalculadoraBoteDB")
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
