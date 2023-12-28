using Microsoft.EntityFrameworkCore;
using Teste_Tecnico_Desenvolvedor_.NET.Modelo;

namespace Teste_Tecnico_Desenvolvedor_.NET.Data
{
    public class TesteTecnicoDbContext : DbContext
    {
        public TesteTecnicoDbContext(DbContextOptions<TesteTecnicoDbContext> options)
           : base(options) { }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Financiamentos> Financiamentos { get; set; }
        public DbSet<Parcelas> Parcelas { get; set; }
        

    }
}
