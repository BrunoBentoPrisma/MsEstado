using Microsoft.AspNetCore.Mvc;
using MsEstado.Dtos;
using MsEstado.Rpositorys.Entidades;
using MsEstado.Services.Interfaces;

namespace MsEstado.Controllers
{
    public class EstadoController : Controller
    {
        private readonly IEstadoService _estadoService;
        public EstadoController(IEstadoService estadoService)
        {
            _estadoService = estadoService;
        }

        [HttpPost("/api/AdicionarEstado")]
        public async Task<IActionResult> AdicionarEstado([FromBody] EstadoDto estadoDto)
        {
            try
            {
                var result = await this._estadoService.AddEstado(estadoDto);

                if (!result) return BadRequest("Ocorreu um erro interno ao adicionar o estado.");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("/api/EditarEstado")]
        public async Task<IActionResult> EditarEstado([FromBody] Estado estado)
        {
            try
            {
                var result = await this._estadoService.UpdateEstado(estado);

                if (!result) return BadRequest("Ocorreu um erro interno ao editar o estado!");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/api/RetornaEstadoPorId/{id:int}")]
        public async Task<IActionResult> RetornaEstadoPorId(int id)
        {
            try
            {
                var estado = await this._estadoService.GetById(id);

                return Ok(estado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("/api/ExcluirEstado/{id:int}")]
        public async Task<IActionResult> ExcluirEstado(int id)
        {
            try
            {
                var result = await this._estadoService.DeleteEstado(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/api/ListaEstado")]
        public async Task<IActionResult> ListaEstado()
        {
            try
            {
                var estados = await this._estadoService.ListaEstado();

                return Ok(estados);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/api/ListaPaginacaoEstado/{pagina}/{query?}")]
        public async Task<IActionResult> ListaPaginacao(int pagina, string query = "")
        {
            try
            {
                var paginacao = await this._estadoService.GetPaginacao(pagina, query);

                if (paginacao == null) return BadRequest("Ocorreu um erro interno ao listar os estados.");

                return Ok(paginacao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
