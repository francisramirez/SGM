

using Microsoft.EntityFrameworkCore;
using SGM.Domain.Entities.Insurance;

namespace SGM.Persistence.Context
{
    public class HealtSyncContext : DbContext
    {
        public HealtSyncContext(DbContextOptions<HealtSyncContext> options) : base(options)
        {
        }
        public DbSet<InsuranceProvider> InsuranceProviders { get; set; }
        
    }
    
}
