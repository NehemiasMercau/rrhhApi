using Microsoft.EntityFrameworkCore;

namespace rrhhApi.Models;

public class RRHHContext : DbContext {
    public RRHHContext(DbContextOptions<RRHHContext> options) : base(options) {

    }

    public DbSet<Candidato> Candidatos { get; set;} = null!;
    public DbSet<Empleo> Empleos { get; set;} = null!;
}