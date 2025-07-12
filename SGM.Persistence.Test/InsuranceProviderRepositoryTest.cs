using SGM.Application.Contracts.Repositories.Insurance;
using SGM.Domain.Base;
using SGM.Domain.Entities.Insurance;
using SGM.Persistence.Test.Repositories;
using System.Buffers;
using System.Threading.Tasks;

namespace SGM.Persistence.Test
{
    public class InsuranceProviderRepositoryTest
    {
        private readonly IInsuranceProviderRepository _insuranceProviderRepository;

        public InsuranceProviderRepositoryTest()
        {
            _insuranceProviderRepository = new InsuranceProviderRepositoryMock(new Context.MedicalContext());
        }
        [Fact]
        public async Task AddAsync_Should_Return_Error_When_Entity_Is_Null()
        {
            //Arrange

            InsuranceProvider? insuranceProvider = null;
            //Act

             var result = await _insuranceProviderRepository.AddAsync(insuranceProvider);
             var message = "InsuranceProvider entity cannot be";

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
            
        }
    }
}