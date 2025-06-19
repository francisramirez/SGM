namespace SGM.Application.Dtos.insurance.NetworkType
{
    public record GetNetworkTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
