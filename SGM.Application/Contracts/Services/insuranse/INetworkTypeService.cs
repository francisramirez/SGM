using SGM.Application.Dtos.insurance.NetworkType;
using SGM.Domain.Base;

namespace SGM.Application.Contracts.Services.insuranse
{
    public interface INetworkTypeService
    {
        Task<OperationResult> GetNetworkTypes();
        Task<OperationResult> GetNetworkTypesById(int networkTypeId);
        Task<OperationResult> UpdateNetworkTypes(ModifyNetworkTypeDto modifyNetworkTypeDto);
        Task<OperationResult> DisabledNetworkTypes(DisableNetworkTypeDto disableNetworkTypeDto);
        Task<OperationResult> CreateNetworkTypes(CreateNetworkTypeDto createNetworkTypeDto);
    }
}
