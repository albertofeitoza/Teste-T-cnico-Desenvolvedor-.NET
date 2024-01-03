using System.ComponentModel;

namespace Teste_Tecnico_Desenvolvedor_.NET.Enum
{
    public enum TipoCredito
    {
        [Description("Crédito Direto")]
        CreditoDireto = 1,

        [Description("Crédito Consignado")]
        CreditoConsignado = 2,

        [Description("Crédito Pessoa Jurídica")]
        CreditoPessoaJuridica = 3,

        [Description("Crédito Pessoa Física")]
        CreditoPessoaFisica = 4,

        [Description("Crédito Imobiliário")]
        CreditoImobiliario = 5
    }
}
