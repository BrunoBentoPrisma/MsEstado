using Microsoft.EntityFrameworkCore;
using MsEstado.Dtos;
using MsEstado.MsContext;
using MsEstado.Rpositorys.Entidades;
using MsEstado.Rpositorys.Interfaces;

namespace MsEstado.Rpositorys.Repository
{
    public class RepositoryEstado : IRepositoryEstado
    {
        protected MsEstadoContext _dbContext;
        public RepositoryEstado(MsEstadoContext msEstadoContext)
        {
            _dbContext = msEstadoContext;
        }
        public async Task<bool> AddEstado(Estado estado)
        {
            try
            {
                _dbContext.Set<Estado>().Add(estado);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> DeleteEstado(Estado estado)
        {
            var message = string.Empty;
            try
            {
                _dbContext.Set<Estado>().Remove(estado);
                await _dbContext.SaveChangesAsync();
                message = "Registro excluído com sucesso !";

            }
            catch (DbUpdateException exDb)
            {
                var innerException = exDb.InnerException;
                while (innerException != null)
                {

                    if (innerException is Npgsql.PostgresException pgEx && pgEx.SqlState == "23503")
                    {
                        throw new Exception($"Não é possível excluir o registro porque há registros relacionados na tabela : {pgEx.TableName}", pgEx);
                    }
                    else
                    {
                        throw new Exception($"Ocorreu um erro interno : {innerException.Message}", innerException);
                    }

                }

            }

            return message;
        }

        public async Task<List<Estado>> GetAll()
        {
            return await _dbContext.Set<Estado>().ToListAsync();
        }

        public async Task<Estado> GetById(int id)
        {
            var estado = new Estado();

            try
            {
                estado = await _dbContext.Set<Estado>().FindAsync(id);
            }
            catch (Exception)
            {
                estado = null;
            }

            return estado;
        }

        public async Task<PaginacaoDto> GetPaginacao(int pagina, string query)
        {
            var paginacao = new PaginacaoDto();

            try
            {

                IQueryable<Estado> queryable = _dbContext.Estado;

                if (!string.IsNullOrEmpty(query.Trim()))
                {
                    queryable = queryable.Where(x => EF.Functions.ILike(
                        EF.Functions.Unaccent(x.Nome),$"%{query}%") ||
                        EF.Functions.ILike(
                        EF.Functions.Unaccent(x.Sigla), $"%{query}%")
                        )
                        .OrderBy(x => EF.Functions.ILike(
                        EF.Functions.Unaccent(x.Nome),
                        $"{query}%") ? 0 : 1)
                        .ThenBy(x => x.Nome);
                }
                else
                {
                    queryable = queryable.OrderByDescending(x => x.Id);
                }

                var total = await queryable.CountAsync();
                var totalPages = (int)Math.Ceiling(total / 10.0);
                pagina = Math.Min(Math.Max(1, pagina), totalPages);

                paginacao.Lista = await queryable.Skip((pagina - 1) * 10)
                                                 .Take(10)
                                                 .ToListAsync();

                paginacao.Count = totalPages;

            }
            catch (Exception)
            {
                paginacao = null;
            }

            return paginacao;
        }

        public async Task<bool> UpdateEstado(Estado estado)
        {
            try
            {
                _dbContext.Estado.Update(estado);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
