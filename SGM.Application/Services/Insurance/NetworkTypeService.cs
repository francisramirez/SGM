

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGM.Application.Contracts.Repositories.Insurance;
using SGM.Application.Contracts.Services.insuranse;
using SGM.Application.Dtos.insurance.InsuranceProvider;
using SGM.Application.Dtos.insurance.NetworkType;
using SGM.Domain.Base;

namespace SGM.Application.Services.Insurance
{
    public sealed class NetworkTypeService : INetworkTypeService
    {
        private readonly INetworkTypeRepository _networkTypeRepository;
        private readonly ILogger<NetworkTypeService> _logger;
        private readonly IConfiguration _configuration;

        public NetworkTypeService(INetworkTypeRepository networkTypeRepository,
                                  ILogger<NetworkTypeService> logger,
                                  IConfiguration configuration)
        {
            _networkTypeRepository = networkTypeRepository;
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<OperationResult> CreateNetworkTypes(CreateNetworkTypeDto createNetworkTypeDto)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                _logger.LogInformation("Creando el network type", createNetworkTypeDto);

                if (createNetworkTypeDto is null)
                {
                    operationResult = OperationResult.Failure("Objeto create DTO es requerido.");
                    return operationResult;
                }

                operationResult = await _networkTypeRepository.AddAsync(createNetworkTypeDto);

                if (!operationResult.IsSuccess)
                {
                    _logger.LogError($"Ocurrió el error: {operationResult.Message} persistiendo el network type.");
                    return operationResult;
                }

                _logger.LogInformation($"El network type {createNetworkTypeDto.Name} fue creado correctamente.", createNetworkTypeDto);

                return operationResult;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error almancenando los datos del network type {ex.Message}");
                operationResult = OperationResult.Failure($"Error almancenando los datos del network type {ex.Message}");
            }
            return operationResult;
        }

        public Task<OperationResult> DisabledNetworkTypes(DisableNetworkTypeDto disableNetworkTypeDto)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> GetNetworkTypes()
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                operationResult = await _networkTypeRepository.GetAllAsync();

                if (!operationResult.IsSuccess)
                    _logger.LogError($"Ocurrió en la funcinalidad : _networkTypeRepository.GetAllAsync(): {operationResult.Message}");

                return operationResult;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error obteniendo los datos del network type {ex.Message}");
                operationResult = OperationResult.Failure($"Error obteniendo los datos del network type {ex.Message}");
                // envio de notificacion.
            }

            return operationResult;
        }

        public async Task<OperationResult> GetNetworkTypesById(int networkTypeId)
        {
            OperationResult operationResult = new OperationResult();
            operationResult = await _networkTypeRepository.GetByIdAsync(networkTypeId);

            return operationResult;
        }

        public async Task<OperationResult> UpdateNetworkTypes(ModifyNetworkTypeDto modifyNetworkTypeDto)
        {
            OperationResult result = new OperationResult();

            result = await _networkTypeRepository.UpdateAsync(modifyNetworkTypeDto);

            return result;
        }
    }
}
