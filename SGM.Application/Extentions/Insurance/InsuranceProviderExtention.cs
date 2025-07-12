

using SGM.Application.Dtos.insurance.InsuranceProvider;
using SGM.Domain.Entities.Insurance;

namespace SGM.Application.Extentions.Insurance
{
    public static class InsuranceProviderExtention
    {
        public static InsuranceProvider ToInsuranceProviderDto(this CreateInsuranceProviderDto createInsuranceProviderDto) 
        {
            return new InsuranceProvider() 
            {
                Name = createInsuranceProviderDto.Name,
                PhoneNumber = createInsuranceProviderDto.PhoneNumber,
                Email = createInsuranceProviderDto.Email,
                Website = createInsuranceProviderDto.Website,
                Address = createInsuranceProviderDto.Address,
                City = createInsuranceProviderDto.City,
                State = createInsuranceProviderDto.State,
                Country = createInsuranceProviderDto.Country,
                ZipCode = createInsuranceProviderDto.ZipCode,
                CoverageDetails = createInsuranceProviderDto.CoverageDetails,
                LogoUrl = createInsuranceProviderDto.LogoUrl,
                IsPreferred = createInsuranceProviderDto.IsPreferred,
                NetworkTypeId = createInsuranceProviderDto.NetworkTypeId,
                CustomerSupportContact = createInsuranceProviderDto.CustomerSupportContact,
                AcceptedRegions = createInsuranceProviderDto.AcceptedRegions,
                MaxCoverageAmount = createInsuranceProviderDto.MaxCoverageAmount
            };
        }
    }
}
