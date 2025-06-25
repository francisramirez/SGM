using Microsoft.AspNetCore.Mvc;
using SGM.Application.Contracts.Services.insuranse;
using SGM.Application.Dtos.insurance.InsuranceProvider;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SGM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceProviderController : ControllerBase
    {
        private readonly IInsuranceProviderService _insuranceProviderService;
        private readonly ILogger<InsuranceProviderController> _logger;

        public InsuranceProviderController(IInsuranceProviderService insuranceProviderService, 
                                           ILogger<InsuranceProviderController> logger)
        {
            _insuranceProviderService = insuranceProviderService;
            _logger = logger;
        }
        // GET: api/<InsuranceProviderController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
           var result = await _insuranceProviderService.GetInsuranceProviders();

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                _logger.LogError("Error fetching insurance providers: {ErrorMessage}", result.Message);
                return BadRequest(result);
            }
        }

        // GET api/<InsuranceProviderController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _insuranceProviderService.GetInsuranceProviderById(id);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                _logger.LogError("Error fetching insurance providers: {ErrorMessage}", result.Message);
                return BadRequest(result);
            }
        }

        // POST api/<InsuranceProviderController>
        [HttpPost("CreateInsuranceProvider")]
        public async Task<IActionResult> Post([FromBody] CreateInsuranceProviderDto createInsuranceProviderDto)
        {
            var result = await _insuranceProviderService.CreateInsuranceProvider(createInsuranceProviderDto);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // PUT api/<InsuranceProviderController>/5
        [HttpPost("ModifyInsuranceProvider")]
        public async Task<IActionResult> Put([FromBody] ModifyInsuranceProviderDto modifyInsuranceProviderDto)
        {
            var result = await _insuranceProviderService.UpdateInsuranceProvider(modifyInsuranceProviderDto);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // DELETE api/<InsuranceProviderController>/5
        [HttpPost("DisableInsuranceProvider")]
        public async Task<IActionResult> DisableInsuranceProvider(DisableInsuranceProviderDto disableInsuranceProviderDto)
        {
            var result = await _insuranceProviderService.DisabledInsuranceProvider(disableInsuranceProviderDto);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
