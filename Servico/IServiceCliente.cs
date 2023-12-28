using Teste_Tecnico_Desenvolvedor_.NET.Dto;
using Teste_Tecnico_Desenvolvedor_.NET.Modelo;

namespace Teste_Tecnico_Desenvolvedor_.NET.Servico
{
    public interface IServiceCliente
    {
        ApiResponse BuscarClientes();
        ApiResponse CadastrarCliente(Cliente cliente);
    }
}
