

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SGM.Application.Contracts.Repositories.Insurance;
using SGM.Domain.Base;
using SGM.Domain.Entities.Insurance;
using SGM.Persistence.Context;
using System.Linq.Expressions;

namespace SGM.Persistence.Repositories
{
    public class InsuranceProviderRepository : IInsuranceProviderRepository
    {
        private readonly HealtSyncContext _context;
        private readonly ILogger<InsuranceProviderRepository> _logger;

        public InsuranceProviderRepository(HealtSyncContext context,
                                          ILogger<InsuranceProviderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<OperationResult> AddAsync(InsuranceProvider entity)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                _logger.LogInformation("Adding InsuranceProvider entity: {@Entity}", entity);

                if (entity == null)
                {
                    _logger.LogError("Attempted to add a null InsuranceProvider entity.");
                    return OperationResult.Failure("InsuranceProvider entity cannot be null.");
                }

                // Validations for required fields can be added here

                await _context.InsuranceProviders.AddAsync(entity);
                await _context.SaveChangesAsync();

                _logger.LogInformation("InsuranceProvider entity added successfully: {@Entity}", entity);

                operationResult = OperationResult.Success("InsuranceProvider entity added successfully.", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding InsuranceProvider entity");
                operationResult = OperationResult.Failure("An error occurred while adding the InsuranceProvider entity.");
            }
            return operationResult;
        }

        public async Task<OperationResult> DeleteAsync(InsuranceProvider entity)
        {
            OperationResult pResult = new OperationResult();
            try
            {
                if (entity == null)
                {
                    _logger.LogError("Attempted to delete a null InsuranceProvider entity.");
                    pResult = OperationResult.Failure("InsuranceProvider entity cannot be null.");
                    return pResult;
                }

                _logger.LogInformation("Deleting InsuranceProvider entity: {@Entity}", entity);

                InsuranceProvider existingEntity = await _context.InsuranceProviders.FindAsync(entity.Id);

                if (existingEntity is null)
                {
                    pResult = OperationResult.Failure("InsuranceProvider entity not found.");
                    return pResult;
                }

                existingEntity.IsDeleted = true; // Soft delete
                existingEntity.DeletedAt = DateTime.UtcNow;
                existingEntity.DeletedBy = Environment.UserName;
                _context.InsuranceProviders.Update(existingEntity);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting InsuranceProvider entity");
                return OperationResult.Failure("An error occurred while deleting the InsuranceProvider entity.");
            }
            return pResult;
        }

        public async Task<bool> ExistsAsync(Expression<Func<InsuranceProvider, bool>> filter)
        {
            return await _context.InsuranceProviders.AnyAsync(filter);
        }

        public async Task<OperationResult> GetAllAsync(Expression<Func<InsuranceProvider, bool>> filter)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                _logger.LogInformation("Retrieving InsuranceProvider entities with filter: {@Filter}", filter);

                operationResult.Data = await _context.InsuranceProviders.Where(filter).ToListAsync();

                operationResult = OperationResult.Success("Retrieving InsuranceProvider entities", operationResult.Data);


                _logger.LogInformation("Retrieved InsuranceProvider");

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error retrieving InsuranceProvider entities");
                operationResult = OperationResult.Failure("An error occurred while retrieving InsuranceProvider entities.");
            }
            return operationResult;
        }

        public async Task<OperationResult> GetByIdAsync(int id)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                _logger.LogInformation("Retrieving InsuranceProvider entity with ID: {Id}", id);
                operationResult.Data = await _context.InsuranceProviders.FindAsync(id);

                operationResult = OperationResult.Success(OperationResult.Success("InsuranceProvider entity retrieved successfully.", operationResult.Data));

                _logger.LogInformation("Retrieved InsuranceProvider");
            }
            catch (Exception ex)
            {
                _logger.LogError("");
            }

            return operationResult;
        }

        public async Task<OperationResult> UpdateAsync(InsuranceProvider entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Updating InsuranceProvider entity: {@Entity}", entity);
                if (entity == null)
                {
                    _logger.LogError("Attempted to update a null InsuranceProvider entity.");
                    return OperationResult.Failure("InsuranceProvider entity cannot be null.");
                }


                //validaciones campos //

                InsuranceProvider insurance = await _context.InsuranceProviders.FindAsync(entity.Id);

                if (insurance is null)
                    return OperationResult.Failure("InsuranceProvider entity not found.");

                insurance.Address = entity.Address;
                insurance.ContactInfo = entity.ContactInfo;
                insurance.Description = entity.Description;


                _context.InsuranceProviders.Update(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("InsuranceProvider entity updated successfully: {@Entity}", entity);
                return OperationResult.Success("InsuranceProvider entity updated successfully.", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating InsuranceProvider entity");
                return OperationResult.Failure("An error occurred while updating the InsuranceProvider entity.");
            }


            return result;
        }
    }
}
