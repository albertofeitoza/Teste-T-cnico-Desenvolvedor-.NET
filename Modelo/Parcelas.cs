using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Teste_Tecnico_Desenvolvedor_.NET.Modelo
{
    public class Parcelas
    {
        [Key]
        public int Id { get; set; }
        public int FinanciamentoId { get; set; }
        public int NumeroParcela { get; set; }
        public decimal ValorParcela { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        
        [JsonIgnore]
        public virtual Financiamentos Financiamento { get; set; }
    }
}
