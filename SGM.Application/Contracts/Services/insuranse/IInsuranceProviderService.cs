

using SGM.Application.Dtos.insurance.InsuranceProvider;
using SGM.Application.Dtos.insurance.NetworkType;
using SGM.Domain.Base;

namespace SGM.Application.Contracts.Services.insuranse
{
    public interface IInsuranceProviderService
    {

        Task<OperationResult> GetInsuranceProviders();
        Task<OperationResult> GetInsuranceProviderById(int insuranceProviderId);
        Task<OperationResult> UpdateInsuranceProvider(ModifyInsuranceProviderDto modifyNetworkTypeDto);
        Task<OperationResult> DisabledInsuranceProvider(DisableInsuranceProviderDto disableNetworkTypeDto);
        Task<OperationResult> CreateInsuranceProvider(CreateInsuranceProviderDto createNetworkTypeDto);


    }
}
