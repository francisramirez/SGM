


using Microsoft.EntityFrameworkCore;
using SGM.Domain.Entities.Insurance;
namespace SGM.Persistence.Test.Context
{
    public class MedicalContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("MedicalDb");
        }
        public DbSet<InsuranceProvider> InsuranceProviders { get; set; }
    }
}
