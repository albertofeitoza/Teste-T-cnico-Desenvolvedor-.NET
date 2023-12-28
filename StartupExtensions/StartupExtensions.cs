
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Teste_Tecnico_Desenvolvedor_.NET.Modelo;
using Teste_Tecnico_Desenvolvedor_.NET.Repositorio;
using Teste_Tecnico_Desenvolvedor_.NET.Servico;

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
            services.AddTransient<IRepositorio<Cliente>, Repositorio<Cliente>>();
        }

        private static void ImplementcaoInterfaces(IServiceCollection services)
        {
            services.AddScoped<IServiceCliente, ServiceCliente>();
        }
    }
}
