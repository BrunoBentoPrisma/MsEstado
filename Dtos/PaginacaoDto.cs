using MsEstado.Rpositorys.Entidades;

namespace MsEstado.Dtos
{
    public class PaginacaoDto
    {
        public int Count { get; set; }
        public List<Estado> Lista { get; set; } =  new List<Estado>();
    }
}
