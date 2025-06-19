
using SGM.Application.Dtos.insurance.NetworkType;
using SGM.Domain.Base;
using SGM.Domain.Entities.Insurance;
using System.Linq.Expressions;

namespace SGM.Application.Contracts.Repositories.Insurance
{
    public interface INetworkTypeRepository
    {
        Task<OperationResult> GetByIdAsync(int id);
        Task<OperationResult> AddAsync(CreateNetworkTypeDto createNetworkTypeDto);
        Task<OperationResult> UpdateAsync(ModifyNetworkTypeDto modifyNetworkTypeDto);
        Task<OperationResult> DeleteAsync(DisableNetworkTypeDto disableNetworkTypeDto);
        Task<OperationResult> GetAllAsync();
       
    }
}
