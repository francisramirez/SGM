
using SGM.Domain.Base;
namespace SGM.Domain.Entities.Insurance
{
    public sealed class NetworkType : AuditEntiy
    {
        public int NetworkTypeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
