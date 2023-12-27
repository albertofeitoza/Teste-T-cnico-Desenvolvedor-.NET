
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Teste_Tecnico_Desenvolvedor_.NET.StartupExtensions
{
    public static class StartupExtensions
    {
        public static void AddServiceDependency(WebApplicationBuilder builder)
        {
            ImplementcaoInterfaces(builder.Services);
            ImplementcaoRepositorios(builder.Services);
            ImplementacaoBancoDados(builder);
        }

        private static void ImplementacaoBancoDados(WebApplicationBuilder builder)
        {
            builder.Services
                .AddDbContext<Data.TesteTecnicoDbContext>(options => 
                    options.UseSqlServer(builder.Configuration.GetConnectionString("SQL")));
        }

        private static void ImplementcaoRepositorios(IServiceCollection services)
        {

        }

        private static void ImplementcaoInterfaces(IServiceCollection services)
        {



        }
    }
}
