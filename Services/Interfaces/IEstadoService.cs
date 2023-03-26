using MsEstado.Dtos;
using MsEstado.Rpositorys.Entidades;

namespace MsEstado.Services.Interfaces
{
    public interface IEstadoService
    {
        Task<bool> AddEstado(EstadoDto estadoDto);
        Task<bool> UpdateEstado(Estado estado);
        Task<string> DeleteEstado(int id);
        Task<Estado> GetById(int id);
        Task<List<Estado>> ListaEstado();
        Task<PaginacaoDto> GetPaginacao(int pagina, string query);
    }
}
