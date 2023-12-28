using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Teste_Tecnico_Desenvolvedor_.NET.Dto;
using Teste_Tecnico_Desenvolvedor_.NET.Modelo;
using Teste_Tecnico_Desenvolvedor_.NET.Repositorio;

namespace Teste_Tecnico_Desenvolvedor_.NET.Servico
{
    public class ServiceCliente : IServiceCliente
    {
    
        private readonly IRepositorio<Cliente> repositorioCliente;
        public ServiceCliente(IRepositorio<Cliente> _repositorioCliente)
        {
            repositorioCliente = _repositorioCliente;
        }

        public ApiResponse BuscarClientes()
        {
            try
            {
                List<Cliente> response = repositorioCliente.BuscarTodos();
                    
                        
                                    
                // calculo dos juros será usado no futuro
                //double valor, percentual, valor_calculado;
                //valor = 178.00; // valor original
                //percentual = Constantes.Constantes.CreditoDireto / 100.0;
                //valor_calculado = valor + (percentual * valor);

                if (response.Count > 0)
                {
                   
                    //incluir a busca dos financiamentos

                    
                    return new ApiResponse { Code = 200, Mensagem = "", data = response };
                    
                }
                else
                    return new ApiResponse { Code = 404, Mensagem = "Não foi localizado clientes", data = null };

            }
            catch (Exception ex)
            {
                return new ApiResponse { Code = 500, Mensagem = $"Erro ao buscar clientes{ex.Message}" };
            }
        }

        public ApiResponse CadastrarCliente(Cliente cliente)
        {
            try
            {
                repositorioCliente.Adicionar(cliente);

                return new ApiResponse { Code = 200, Mensagem = "", data = cliente };
            }
            catch (Exception ex)
            {
                return new ApiResponse { Code = 500, Mensagem = $"Erro ao buscar clientes{ex.Message}" };
            }
        }

    }
}
