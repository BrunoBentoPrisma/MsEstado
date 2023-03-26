using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MsEstado.Rpositorys.Entidades
{
    public class Estado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public decimal? AliquotaIcmsEstado { get; set; }
        public decimal? AliquotaFcpEstado { get; set; }
        public bool DifalComCalculoPorDentro { get; set; }
        public bool DifalComCalculoDeIsento { get; set; }
        public bool ChecagemContribuinteIsento { get; set; }
        public int? PaisId { get; set; }
    }
}
