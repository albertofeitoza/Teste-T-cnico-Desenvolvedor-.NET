using Microsoft.AspNetCore.Mvc;
using Teste_Tecnico_Desenvolvedor_.NET.Dto;
using Teste_Tecnico_Desenvolvedor_.NET.Servico;

namespace Teste_Tecnico_Desenvolvedor_.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmprestimoController : ControllerBase
    {
        private readonly ILogger<Emprestimo> logger;
        private readonly IServiceEmprestimo serviceFinanciamento;

        public EmprestimoController(ILogger<Emprestimo> _logger, IServiceEmprestimo _serviceFinanciamento)
        {
            logger = _logger;
            serviceFinanciamento = _serviceFinanciamento;
        }

        [HttpPost]
        public ActionResult<ApiResponse> Post([FromBody] Emprestimo emprestimo)
        {
            try
            {
                logger.LogInformation("Recebendo dados de entrada");

                var response = serviceFinanciamento.ValidacoesEntrada(emprestimo);

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
