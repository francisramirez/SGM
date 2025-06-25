
using SGM.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
namespace SGM.Domain.Entities.Insurance
{
    [Table("NetworkType", Schema = "Insurance")]
    public sealed class NetworkType : AuditEntiy
    {
        public int NetworkTypeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
