

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGM.Application.Contracts.Repositories.Insurance;
using SGM.Application.Contracts.Services.insuranse;
using SGM.Application.Dtos.insurance.InsuranceProvider;
using SGM.Domain.Base;
using SGM.Domain.Entities.Insurance;

namespace SGM.Application.Services.Insurance
{
    public sealed class InsuranceProviderService : IInsuranceProviderService
    {
        private readonly IInsuranceProviderRepository _providerRepository;
        private readonly ILogger<InsuranceProviderService> _logger;
        private readonly IConfiguration _configuration;

        public InsuranceProviderService(IInsuranceProviderRepository providerRepository,
                                        ILogger<InsuranceProviderService> logger,
                                        IConfiguration configuration
                                        )
        {
            _providerRepository = providerRepository;
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<OperationResult> GetInsuranceProviders()
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                _logger.LogInformation("Fetching all insurance providers.");

                operationResult = await _providerRepository.GetAllAsync(ip => !ip.IsActive);

                operationResult.IsSuccess = true;
                operationResult.Message = "Insurance providers retrieved successfully.";

                _logger.LogInformation("Successfully fetched insurance providers.");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while getting insurance providers.");
                operationResult.IsSuccess = false;
                operationResult.Message = "An error occurred while getting insurance providers.";
            }
            return operationResult;
        }
        public async Task<OperationResult> CreateInsuranceProvider(CreateInsuranceProviderDto createInsuranceProviderDto)
        {
            OperationResult operationResult = new OperationResult();
            try
            {

                /// Validate the DTO before proceeding //
                /// 
                InsuranceProvider insuranceProvider = new InsuranceProvider
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

                operationResult = await _providerRepository.AddAsync(insuranceProvider);

                _logger.LogInformation("Creating insurance provider: {InsuranceProviderName}", createInsuranceProviderDto.Name);
            }
            catch (Exception)
            {

                _logger.LogError("An error occurred while creating insurance provider.");
                operationResult.IsSuccess = false;
                operationResult.Message = "An error occurred while creating insurance provider.";
            }
            return operationResult;
        }

        public async Task<OperationResult> DisabledInsuranceProvider(DisableInsuranceProviderDto disableInsuranceProviderDto)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                _logger.LogInformation("Disabling insurance provider with ID {InsuranceProviderId}.", disableInsuranceProviderDto.Id);

                // Validate the DTO before proceeding //

                InsuranceProvider insuranceProvider = new InsuranceProvider
                {
                    InsuranceProviderID = disableInsuranceProviderDto.Id,
                    UpdatedAt = disableInsuranceProviderDto.UpdateAt ?? DateTime.UtcNow
                };

                await _providerRepository.DeleteAsync(insuranceProvider);

                _logger.LogInformation("Successfully disabled insurance provider with ID {InsuranceProviderId}.", disableInsuranceProviderDto.Id);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while disabling insurance provider with ID {InsuranceProviderId}.", disableInsuranceProviderDto.Id);
                operationResult = OperationResult.Failure("An error occurred while disabling the insurance provider.");
            }
            return operationResult;
        }

        public async Task<OperationResult> GetInsuranceProviderById(int insuranceProviderId)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                operationResult = await _providerRepository.GetByIdAsync(insuranceProviderId);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while getting insurance provider by ID {InsuranceProviderId}.", insuranceProviderId);
                operationResult.IsSuccess = false;
                operationResult.Message = "An error occurred while getting the insurance provider by ID.";
            }

            return operationResult;
        }

        public async Task<OperationResult> UpdateInsuranceProvider(ModifyInsuranceProviderDto modifyNetworkTypeDto)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                // Validate the DTO before proceeding //

                _logger.LogInformation("Updating insurance provider with ID {InsuranceProviderId}.", modifyNetworkTypeDto.Id);

                InsuranceProvider insuranceProvider = new InsuranceProvider
                {
                    InsuranceProviderID = modifyNetworkTypeDto.Id,
                    Name = modifyNetworkTypeDto.Name,
                    PhoneNumber = modifyNetworkTypeDto.PhoneNumber,
                    Email = modifyNetworkTypeDto.Email,
                    Website = modifyNetworkTypeDto.Website,
                    Address = modifyNetworkTypeDto.Address,
                    City = modifyNetworkTypeDto.City,
                    State = modifyNetworkTypeDto.State,
                    Country = modifyNetworkTypeDto.Country,
                    ZipCode = modifyNetworkTypeDto.ZipCode,
                    CoverageDetails = modifyNetworkTypeDto.CoverageDetails,
                    LogoUrl = modifyNetworkTypeDto.LogoUrl,
                    IsPreferred = modifyNetworkTypeDto.IsPreferred,
                    NetworkTypeId = modifyNetworkTypeDto.NetworkTypeId,
                    CustomerSupportContact = modifyNetworkTypeDto.CustomerSupportContact,
                    AcceptedRegions = modifyNetworkTypeDto.AcceptedRegions,
                    MaxCoverageAmount = modifyNetworkTypeDto.MaxCoverageAmount
                };

                operationResult = await _providerRepository.UpdateAsync(insuranceProvider);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while updating insurance provider with ID {InsuranceProviderId}.", modifyNetworkTypeDto.Id);
                operationResult.IsSuccess = false;
                operationResult.Message = "An error occurred while updating the insurance provider.";
            }

            return operationResult;
        }
    }
}
