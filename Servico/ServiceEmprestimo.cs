using Teste_Tecnico_Desenvolvedor_.NET.Dto;
using Teste_Tecnico_Desenvolvedor_.NET.Enum;

namespace Teste_Tecnico_Desenvolvedor_.NET.Servico
{
    public class ServiceEmprestimo : IServiceEmprestimo
    {
        public List<ErroResponse> InclusaoEmprestimo(Emprestimo emprestimo)
        {
            var dadosEntrada = Validacoes(emprestimo);

            if (dadosEntrada.Count > 0)
                return dadosEntrada;

            var dadosEmprestimo = CalculoJuros(emprestimo);

            dadosEntrada.Add(new ErroResponse
            {
                status = "success",
                MensagemErro = string.Empty,
                DadosRetorno = dadosEmprestimo
            });

            return dadosEntrada;
        }

        private List<ErroResponse> Validacoes(Emprestimo emprestimo)
        {
            var erros = new List<ErroResponse>();

            if (emprestimo.ValorCredito <= 0 || emprestimo.ValorCredito > double.Parse("1.000.000,00"))
            {
                erros.Add(
                    new ErroResponse
                    {
                        status = "Recusado",
                        MensagemErro = "O valor máximo a ser liberado para qualquer tipo de empréstimo dev ser maior que R$ 0,00 até R$ 1.000.000,00"
                    });
            }

            if (emprestimo.QuantidadeParcelas < 5 || emprestimo.QuantidadeParcelas > 72)
            {

                erros.Add(
                   new ErroResponse
                   {
                       status = "Recusado",
                       MensagemErro = "A quantidade mínima de parcelas é de 5x e máxima de 72x"
                   });
            }

            if (emprestimo.TipoCredito == TipoCredito.CreditoPessoaJuridica && emprestimo.ValorCredito < double.Parse("15.000,00"))
            {
                erros.Add(
                    new ErroResponse
                    {
                        status = "Recusado",
                        MensagemErro = "Para o crédito de pessoa jurídica, o valor mínimo a ser liberado é de R$ 15.000,00"
                    });
            }

            if (emprestimo.DataPrimeiroVencimento.Date < DateTime.Now.Date.AddDays(15) || emprestimo.DataPrimeiroVencimento.Date > DateTime.Now.Date.AddDays(40))
            {
                erros.Add(
                    new ErroResponse
                    {
                        status = "Recusado",
                        MensagemErro = "A data do primeiro vencimento sempre será no mínimo 15 dias e no máximo 40 dias a partir da data atual."
                    });
            }
            return erros;
        }

        private ApiResponse CalculoJuros(Emprestimo financiamento)
        {

            switch (financiamento.TipoCredito)
            {
                case TipoCredito.CreditoDireto:
                    return new ApiResponse
                    {
                        Status = "Aprovado",
                        ValorTotalComJuros = (decimal)(financiamento.ValorCredito + (Constantes.Constantes.CreditoDireto / 100.0 * financiamento.ValorCredito)),
                        ValorJuros = ((decimal)(Constantes.Constantes.CreditoDireto / 100.0 * financiamento.ValorCredito))
                    };
                    break;
                case TipoCredito.CreditoConsignado:
                    return new ApiResponse
                    {
                        Status = "Aprovado",
                        ValorTotalComJuros = (decimal)(financiamento.ValorCredito + (Constantes.Constantes.CreditoConsignado) / 100.0 * financiamento.ValorCredito),
                        ValorJuros = ((decimal)(Constantes.Constantes.CreditoConsignado / 100.0 * financiamento.ValorCredito))
                    };
                    break;
                case TipoCredito.CreditoPessoaJuridica:
                    return new ApiResponse
                    {
                        Status = "Aprovado",
                        ValorTotalComJuros = (decimal)(financiamento.ValorCredito + (Constantes.Constantes.CreditoPessoaJuridica / 100.0 * financiamento.ValorCredito)),
                        ValorJuros = ((decimal)(Constantes.Constantes.CreditoPessoaJuridica / 100.0 * financiamento.ValorCredito))
                    };
                    break;
                case TipoCredito.CreditoPessoaFisica:
                    return new ApiResponse
                    {
                        Status = "Aprovado",
                        ValorTotalComJuros = (decimal)(financiamento.ValorCredito + (Constantes.Constantes.CreditoPessoaFisica / 100.0 * financiamento.ValorCredito)),
                        ValorJuros = ((decimal)(Constantes.Constantes.CreditoPessoaFisica / 100.0 * financiamento.ValorCredito))
                    };
                    break;
                case TipoCredito.CreditoImobiliario:
                    return new ApiResponse
                    {
                        Status = "Aprovado",
                        ValorTotalComJuros = (decimal)(financiamento.ValorCredito + (Constantes.Constantes.CreditoImobiliario / 100.0 * financiamento.ValorCredito)),
                        ValorJuros = ((decimal)(Constantes.Constantes.CreditoImobiliario / 100.0 * financiamento.ValorCredito))
                    };
                    break;
                default:
                    return new ApiResponse();
                    break;
            }
        }
    }
}