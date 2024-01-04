using Teste_Tecnico_Desenvolvedor_.NET.Dto;

namespace Teste_Tecnico_Desenvolvedor_.NET.Servico
{
    public interface IServiceEmprestimo
    {
        List<ErroResponse> InclusaoEmprestimo(Emprestimo financiamento);
    }
}
