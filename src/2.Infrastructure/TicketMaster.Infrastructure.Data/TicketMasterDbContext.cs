using Microsoft.EntityFrameworkCore;

namespace TicketMaster.Infrastructure.Data;

public class TicketMasterDbContext : DbContext
{
    public TicketMasterDbContext(DbContextOptions<TicketMasterDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}