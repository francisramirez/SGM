
using Microsoft.EntityFrameworkCore;
using SGM.Application.Contracts.Repositories.Insurance;
using SGM.Domain.Base;
using SGM.Domain.Entities.Insurance;
using SGM.Persistence.Test.Context;
using System.Linq.Expressions;

namespace SGM.Persistence.Test.Repositories
{
    public class InsuranceProviderRepositoryMock : IInsuranceProviderRepository
    {
        private readonly MedicalContext _context;

        public InsuranceProviderRepositoryMock(MedicalContext context)
        {
            _context = context;
        }
        public async Task<OperationResult> AddAsync(InsuranceProvider entity)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                if (entity == null)
                {
                    return OperationResult.Failure("InsuranceProvider entity cannot be null.");
                }

                // Validations for required fields can be added here

                if (string.IsNullOrEmpty(entity.Name))
                {

                    return OperationResult.Failure("The Name is Requeried");
                }

                if (entity.Name.Length > 50)
                {

                    return OperationResult.Failure("The Name Length is invalid");
                }
                await _context.InsuranceProviders.AddAsync(entity);
                await _context.SaveChangesAsync();


                operationResult = OperationResult.Success("InsuranceProvider entity added successfully.", entity);
            }
            catch (Exception ex)
            {
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
                    pResult = OperationResult.Failure("InsuranceProvider entity cannot be null.");
                    return pResult;
                }


                InsuranceProvider existingEntity = await _context.InsuranceProviders.FindAsync(entity.InsuranceProviderID);

                if (existingEntity is null)
                {
                    pResult = OperationResult.Failure("InsuranceProvider entity not found.");
                    return pResult;
                }

                existingEntity.IsActive = false; // Soft delete
                existingEntity.UpdatedAt = DateTime.UtcNow;

                _context.InsuranceProviders.Update(existingEntity);
                await _context.SaveChangesAsync();


                pResult = OperationResult.Success("InsuranceProvider entity deleted successfully.", existingEntity);

            }
            catch (Exception ex)
            {
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

                operationResult.Data = await _context.InsuranceProviders.Where(filter).ToListAsync();

                operationResult = OperationResult.Success("Retrieving InsuranceProvider entities", operationResult.Data);


            }
            catch (Exception e)
            {
                operationResult = OperationResult.Failure("An error occurred while retrieving InsuranceProvider entities.");
            }
            return operationResult;
        }

        public async Task<OperationResult> GetByIdAsync(int id)
        {
            OperationResult operationResult = new OperationResult();

            try
            {

                operationResult.Data = await _context.InsuranceProviders.FindAsync(id);

                operationResult = OperationResult.Success(OperationResult.Success("InsuranceProvider entity retrieved successfully.", operationResult.Data));

            }
            catch (Exception ex)
            {
                operationResult = OperationResult.Failure($"An error occurred while retrieving InsuranceProvider entities. {ex.Message}");

            }

            return operationResult;
        }

        public async Task<OperationResult> UpdateAsync(InsuranceProvider entity)
        {
            OperationResult result = new OperationResult();

            try
            {
              
                if (entity == null)
                {
                    
                    result = OperationResult.Failure("InsuranceProvider entity cannot be null.");
                }


                //validaciones campos //

                InsuranceProvider insurance = await _context.InsuranceProviders.FindAsync(entity.InsuranceProviderID);

                if (insurance is null)
                    return OperationResult.Failure("InsuranceProvider entity not found.");

                insurance.Address = entity.Address;
                insurance.AcceptedRegions = entity.AcceptedRegions;
                insurance.City = entity.City;
                insurance.Country = entity.Country;
                insurance.CoverageDetails = entity.CoverageDetails;
                insurance.CustomerSupportContact = entity.CustomerSupportContact;
                insurance.Email = entity.Email;
                insurance.IsActive = entity.IsActive;
                insurance.IsPreferred = entity.IsPreferred;
                insurance.LogoUrl = entity.LogoUrl;
                insurance.MaxCoverageAmount = entity.MaxCoverageAmount;
                insurance.Name = entity.Name;
                insurance.NetworkTypeId = entity.NetworkTypeId;
                insurance.PhoneNumber = entity.PhoneNumber;
                insurance.State = entity.State;
                insurance.Website = entity.Website;
                insurance.ZipCode = entity.ZipCode;
                insurance.CreatedAt = entity.CreatedAt;


                _context.InsuranceProviders.Update(insurance);
                await _context.SaveChangesAsync();
                     result = OperationResult.Success("InsuranceProvider entity updated successfully.", entity);
            }
            catch (Exception ex)
            {
                 return OperationResult.Failure("An error occurred while updating the InsuranceProvider entity.");
            }


            return result;
        }
    }
}
