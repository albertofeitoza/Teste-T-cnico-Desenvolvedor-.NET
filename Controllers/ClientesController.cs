using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Teste_Tecnico_Desenvolvedor_.NET.Dto;
using Teste_Tecnico_Desenvolvedor_.NET.Modelo;
using Teste_Tecnico_Desenvolvedor_.NET.Servico;

namespace Teste_Tecnico_Desenvolvedor_.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ILogger<Cliente> logger;
        private readonly IServiceCliente serviceCliente;

        public ClientesController(ILogger<Cliente> _logger, IServiceCliente _serviceCliente)
        {
            logger = _logger;
            serviceCliente = _serviceCliente;
        }


        [HttpGet]
        public ActionResult<ApiResponse> Get()
        {
            try
            {
                logger.LogInformation("Buscando todos os clientes");

                var response = serviceCliente.BuscarClientes();

                if (response.Code == 200)
                    return Ok(response);
                else
                    return StatusCode(response.Code, response);
            }
            catch (Exception e)
            {
                return new ApiResponse { Code = 500, Mensagem = "Servidor Indisponível.", data = null };
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ApiResponse> GetId(int id)
        {
            try
            {


                return new ApiResponse();
            }
            catch (Exception e)
            {

                return new ApiResponse();
            }
        }

        [HttpPost]
        public ActionResult<ApiResponse> Post([FromBody] Cliente cliente)
        {
            try
            {
                logger.LogInformation("Cadastro de cliente");

                var response = serviceCliente.CadastrarCliente(cliente);
                
                if (response.Code == 200)
                    return Ok(response);
                else
                    return StatusCode(response.Code, response);
            }
            catch (Exception e)
            {
                return new ApiResponse { Code = 500, Mensagem = "Servidor Indisponível.", data = null };
            }

        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
