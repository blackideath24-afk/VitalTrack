using Microsoft.EntityFrameworkCore;
using VitalTrack.Models;

namespace VitalTrack.Data
{
    public class VitalTrackContext : DbContext
    {
        public VitalTrackContext(DbContextOptions<VitalTrackContext> options) : base(options) { }

        public DbSet<RegistroSalud> Registros { get; set; }
    }
}