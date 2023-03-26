using MsEstado.Dtos;
using MsEstado.Rpositorys.Entidades;

namespace MsEstado.Rpositorys.Interfaces
{
    public interface IRepositoryEstado
    {
        Task<bool> AddEstado(Estado estado);
        Task<bool> UpdateEstado(Estado estado);
        Task<string> DeleteEstado(Estado estado);
        Task<Estado> GetById(int id);
        Task<List<Estado>> GetAll();
        Task<PaginacaoDto> GetPaginacao(int pagina, string query);
    }
}
