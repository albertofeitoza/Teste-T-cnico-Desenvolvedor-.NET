
using Microsoft.EntityFrameworkCore;
using Teste_Tecnico_Desenvolvedor_.NET.Servico;

namespace Teste_Tecnico_Desenvolvedor_.NET.StartupExtensions
{
    public static class StartupExtensions
    {
        public static void AddServiceDependency(WebApplicationBuilder builder)
        {
            ImplementcaoInterfaces(builder.Services);
        }

        private static void ImplementcaoInterfaces(IServiceCollection services)
        {
            services.AddScoped<IServiceEmprestimo, ServiceEmprestimo>();
        }
    }
}
