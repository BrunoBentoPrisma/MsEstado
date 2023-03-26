using MsEstado.Dtos;
using MsEstado.Rpositorys.Entidades;
using MsEstado.Rpositorys.Interfaces;
using MsEstado.Services.Interfaces;

namespace MsEstado.Services.Service
{
    public class EstadoService : IEstadoService
    {
        private readonly IRepositoryEstado _repositoryEstado;
        public EstadoService(IRepositoryEstado repositoryEstado)
        {
            _repositoryEstado = repositoryEstado;
        }
        public async Task<bool> AddEstado(EstadoDto estadoDto)
        {
            try
            {
                if (estadoDto == null) throw new Exception("Dados do estado enviados incorretamente.");
                if (string.IsNullOrEmpty(estadoDto.Nome.Trim())) throw new Exception("Nome do estado é obrigatório");
                if (string.IsNullOrEmpty(estadoDto.Sigla.Trim())) throw new Exception("Sigla do estado é obrigatório");
                if (estadoDto.PaisId <= 0) throw new Exception("Id do país inválido.");

                var estado = new Estado()
                {
                    Id = 0,
                    PaisId = estadoDto.PaisId,
                    Nome = estadoDto.Nome,
                    AliquotaFcpEstado = estadoDto.AliquotaFcpEstado,
                    AliquotaIcmsEstado = estadoDto.AliquotaIcmsEstado,
                    ChecagemContribuinteIsento = estadoDto.ChecagemContribuinteIsento,
                    DifalComCalculoDeIsento = estadoDto.DifalComCalculoDeIsento,
                    DifalComCalculoPorDentro = estadoDto.DifalComCalculoPorDentro,
                    Sigla = estadoDto.Sigla
                };

                await this._repositoryEstado.AddEstado(estado);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeleteEstado(int id)
        {
            try
            {
                if (id <= 0) throw new Exception("Id inválido.");
                
                var estado = await this._repositoryEstado.GetById(id);

                if (estado == null) throw new Exception("Não foi possível localizar o estado.");

                var result = await this._repositoryEstado.DeleteEstado(estado);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Estado> GetById(int id)
        {
            try
            {
                if (id <= 0) throw new Exception("Id inválido.");

                var estado = await _repositoryEstado.GetById(id);

                if (estado == null) throw new Exception("Não foi possível localizar o estado.");

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PaginacaoDto> GetPaginacao(int pagina, string query)
        {
            try
            {
                var paginacao = await this._repositoryEstado.GetPaginacao(pagina, query);

                if (paginacao == null) throw new Exception("Não foi possível listar os estado.");

                return paginacao;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Estado>> ListaEstado()
        {
            try
            {
                var estados = await this._repositoryEstado.GetAll();

                if (estados == null) throw new Exception("Ocorreu um erro interno ao listar os estados, tente novamente mais tarde.");

                return estados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateEstado(Estado estado)
        {
            try
            {
                if (estado == null) throw new Exception("Dados do estado enviados incorretamente.");
                if (string.IsNullOrEmpty(estado.Nome.Trim())) throw new Exception("Nome do estado é obrigatório");
                if (string.IsNullOrEmpty(estado.Sigla.Trim())) throw new Exception("Sigla do estado é obrigatório");
                if (estado.PaisId <= 0) throw new Exception("Id do país inválido.");

                await this._repositoryEstado.UpdateEstado(estado);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
