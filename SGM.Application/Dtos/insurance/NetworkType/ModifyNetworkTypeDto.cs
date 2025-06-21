
namespace SGM.Application.Dtos.insurance.NetworkType
{
    public record ModifyNetworkTypeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? UpdateAt { get; set; } = DateTime.UtcNow;
    }
}
