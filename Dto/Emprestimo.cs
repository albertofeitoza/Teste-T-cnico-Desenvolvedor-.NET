using System.ComponentModel.DataAnnotations;
using Teste_Tecnico_Desenvolvedor_.NET.Enum;

namespace Teste_Tecnico_Desenvolvedor_.NET.Dto
{
    public class Emprestimo
    {
        [Required()]
        public double ValorCredito { get; set; }
        
        [Required]
        public TipoCredito TipoCredito { get; set; }
        
        [Required]
        public int QuantidadeParcelas { get; set; }

        [Required]
        public DateTime DataPrimeiroVencimento { get; set; } = DateTime.Now.Date;

    }
}
