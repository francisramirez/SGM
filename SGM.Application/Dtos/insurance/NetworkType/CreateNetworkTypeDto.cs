﻿

namespace SGM.Application.Dtos.insurance.NetworkType
{
    public record CreateNetworkTypeDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
