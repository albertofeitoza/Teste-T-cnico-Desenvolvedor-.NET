using System.ComponentModel.DataAnnotations;

namespace Teste_Tecnico_Desenvolvedor_.NET.Modelo
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public int CPF { get; set; } = 0;
        public string UF { get; set; } = "";
        public int Celular { get; set; } = 0;
        public virtual List<Financiamentos>? Financiamentos { get; set; }
    }
}
