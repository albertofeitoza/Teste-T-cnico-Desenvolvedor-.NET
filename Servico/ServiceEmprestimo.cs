using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Numerics;
using Teste_Tecnico_Desenvolvedor_.NET.Dto;
using Teste_Tecnico_Desenvolvedor_.NET.Enum;

namespace Teste_Tecnico_Desenvolvedor_.NET.Servico
{
    public class ServiceEmprestimo : IServiceEmprestimo
    {
        public ApiResponse ValidacoesEntrada(Emprestimo emprestimo)
        {
            var dadosEntrada = ValidacaoEntrada(emprestimo);

            if (dadosEntrada != null)
                return dadosEntrada;

            var response = CalculoJuros(emprestimo);

            return response;
        }

        private ApiResponse ValidacaoEntrada(Emprestimo emprestimo)
        {
            if (emprestimo.ValorCredito <= 0 || emprestimo.ValorCredito > double.Parse("1.000.000,00"))
            {
                return new ApiResponse
                {
                    Status = "Recusado, O valor máximo a ser liberado para qualquer tipo de empréstimo dev ser maior que R$ 0,00 até R$ 1.000.000,00",
                    ValorJuros = 0,
                    ValorTotalComJuros = 0
                };
            }


            if (emprestimo.QuantidadeParcelas < 5 || emprestimo.QuantidadeParcelas > 72)
            {
                return new ApiResponse
                {
                    Status = "Recusado, A quantidade mínima de parcelas é de 5x e máxima de 72x",
                    ValorJuros = 0,
                    ValorTotalComJuros = 0
                };
            }

            if (emprestimo.TipoCredito == TipoCredito.CreditoPessoaJuridica && emprestimo.ValorCredito < double.Parse("15.000,00"))
            {
                return new ApiResponse
                {
                    Status = "Recusado, Para o crédito de pessoa jurídica, o valor mínimo a ser liberado é de R$ 15.000,00",
                    ValorJuros = 0,
                    ValorTotalComJuros = 0
                };
            }

            if (emprestimo.DataPrimeiroVencimento.Date < DateTime.Now.Date.AddDays(15) || emprestimo.DataPrimeiroVencimento.Date > DateTime.Now.Date.AddDays(40))
            {
                return new ApiResponse
                {
                    Status = "Recusado, A data do primeiro vencimento sempre será no mínimo 15 dias e no máximo 40 dias a partir da data atual. ",
                    ValorJuros = 0,
                    ValorTotalComJuros = 0
                };
            }
            return null;
        }

        private ApiResponse CalculoJuros(Emprestimo financiamento)
        {
            if (financiamento.TipoCredito == TipoCredito.CreditoDireto)
            {
                return new ApiResponse
                {
                    Status = "Aprovado",
                    ValorTotalComJuros = (decimal)(financiamento.ValorCredito + (Constantes.Constantes.CreditoDireto / 100.0 * financiamento.ValorCredito)),
                    ValorJuros = ((decimal)(Constantes.Constantes.CreditoDireto / 100.0 * financiamento.ValorCredito))
                };
            }

            if (financiamento.TipoCredito == TipoCredito.CreditoConsignado)
            {
                return new ApiResponse
                {
                    Status = "Aprovado",
                    ValorTotalComJuros = (decimal)(financiamento.ValorCredito + (Constantes.Constantes.CreditoConsignado) / 100.0 * financiamento.ValorCredito),
                    ValorJuros = ((decimal)(Constantes.Constantes.CreditoConsignado / 100.0 * financiamento.ValorCredito))
                };
            }

            if (financiamento.TipoCredito == TipoCredito.CreditoPessoaJuridica)
            {
                return new ApiResponse
                {
                    Status = "Aprovado",
                    ValorTotalComJuros = (decimal)(financiamento.ValorCredito + (Constantes.Constantes.CreditoPessoaJuridica / 100.0 * financiamento.ValorCredito)),
                    ValorJuros = ((decimal)(Constantes.Constantes.CreditoPessoaJuridica / 100.0 * financiamento.ValorCredito))
                };
            }

            if (financiamento.TipoCredito == TipoCredito.CreditoPessoaFisica)
            {
                return new ApiResponse
                {
                    Status = "Aprovado",
                    ValorTotalComJuros = (decimal)(financiamento.ValorCredito + (Constantes.Constantes.CreditoPessoaFisica / 100.0 * financiamento.ValorCredito)),
                    ValorJuros = ((decimal)(Constantes.Constantes.CreditoPessoaFisica / 100.0 * financiamento.ValorCredito))
                };
            }

            if (financiamento.TipoCredito == TipoCredito.CreditoImobiliario)
            {
                return new ApiResponse
                {
                    Status = "Aprovado",
                    ValorTotalComJuros = (decimal)(financiamento.ValorCredito + (Constantes.Constantes.CreditoImobiliario / 100.0 * financiamento.ValorCredito)),
                    ValorJuros = ((decimal)(Constantes.Constantes.CreditoImobiliario / 100.0 * financiamento.ValorCredito))
                };
            }
            return new ApiResponse();
        }
    }
}