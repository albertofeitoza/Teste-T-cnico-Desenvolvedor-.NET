using Microsoft.EntityFrameworkCore;

namespace Teste_Tecnico_Desenvolvedor_.NET.Data
{
    public class TesteTecnicoDbContext : DbContext
    {
        public TesteTecnicoDbContext(DbContextOptions<TesteTecnicoDbContext> options)
           : base(options) { }

        
    }
}
