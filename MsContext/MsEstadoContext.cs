using Microsoft.EntityFrameworkCore;
using MsEstado.Rpositorys.Entidades;

namespace MsEstado.MsContext
{
    public class MsEstadoContext : DbContext
    {
        public MsEstadoContext(DbContextOptions<MsEstadoContext> options)
           : base(options)
        {
        }

        public DbSet<Estado> Estado { get; set; }
    }
}
