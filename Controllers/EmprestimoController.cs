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
        public ActionResult Post([FromBody] Emprestimo emprestimo)
        {
            try
            {
                logger.LogInformation("Recebendo dados de empréstimo.");

                var response = serviceFinanciamento.InclusaoEmprestimo(emprestimo);

                if (response[0].DadosRetorno == null)
                    return BadRequest(response);

                return Ok(response[0].DadosRetorno);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
