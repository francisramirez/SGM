
using SGM.Domain.Base;

namespace SGM.Domain.Entities.Insurance
{
    public sealed class InsuranceProvider : AuditEntiy
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ContactInfo { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        // Additional properties can be added as needed
    }
}
