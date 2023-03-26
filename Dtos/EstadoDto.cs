namespace MsEstado.Dtos
{
    public class EstadoDto
    {
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
