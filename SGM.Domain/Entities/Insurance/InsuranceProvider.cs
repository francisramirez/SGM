﻿
using SGM.Domain.Base;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;


namespace SGM.Domain.Entities.Insurance
{

    [Table("InsuranceProviders", Schema = "Insurance")]
    public sealed class InsuranceProvider : AuditEntiy
    {


        public int InsuranceProviderID { get; set; }
        public string? Name { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? Website { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }

        public string? ZipCode { get; set; }

        public string? CoverageDetails { get; set; }

        public string? LogoUrl { get; set; }

        public bool IsPreferred { get; set; }

        public int NetworkTypeId { get; set; }

        public string? CustomerSupportContact { get; set; }

        public string? AcceptedRegions { get; set; }

        public decimal? MaxCoverageAmount { get; set; }


    }
}
