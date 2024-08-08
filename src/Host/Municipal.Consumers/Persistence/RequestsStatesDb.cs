using Microsoft.EntityFrameworkCore;
using Municipal.Consumers.Saga;

namespace Municipal.Consumers.Persistence;

public class RequestsStatesDb: DbContext
{
    
    public RequestsStatesDb(DbContextOptions<RequestsStatesDb> options): 
        base(options){  }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ServicesSagaData>().HasKey(s => s.CorrelationId);

    }
    
    public DbSet<ServicesSagaData> SagaData { get; set; }
    
}