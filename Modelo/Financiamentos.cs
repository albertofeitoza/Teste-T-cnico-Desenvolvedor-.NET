using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Teste_Tecnico_Desenvolvedor_.NET.Enum;

namespace Teste_Tecnico_Desenvolvedor_.NET.Modelo
{
    public class Financiamentos
    {
        [Key]
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public long CPF { get; set; } = 0;
        public TipoFinanciamento TipoFinanciamento { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataUltimoVencimento { get; set; }
        
        
        [JsonIgnore]
        public virtual List<Parcelas> Parcelas { get; set; }

        [JsonIgnore]
        public virtual Cliente Cliente { get; set; }


    }
}
