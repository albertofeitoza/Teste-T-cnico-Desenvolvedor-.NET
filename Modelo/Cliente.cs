using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Teste_Tecnico_Desenvolvedor_.NET.Modelo
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public long CPF { get; set; } = 0;
        public string UF { get; set; } = "";
        public long Celular { get; set; } = 0;
        
        [JsonIgnore]        
        public virtual List<Financiamentos>? Financiamentos { get; set; }
    }
}
