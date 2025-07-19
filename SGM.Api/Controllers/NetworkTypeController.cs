using Microsoft.AspNetCore.Mvc;
using SGM.Application.Contracts.Services.insuranse;
using SGM.Application.Dtos.insurance.NetworkType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SGM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkTypeController : ControllerBase
    {
        private readonly INetworkTypeService _networkTypeService;

        public NetworkTypeController(INetworkTypeService networkTypeService)
        {
            _networkTypeService = networkTypeService;
        }

        [HttpGet("GetNetworkTypes")]
        public async Task<IActionResult> Get()
        {
            var result = await _networkTypeService.GetNetworkTypes();

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);

        }


        [HttpGet("GetNetworkTypeById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _networkTypeService.GetNetworkTypesById(id);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }


        [HttpPost("CreateNetworkType")]
        public async Task<IActionResult> Post([FromBody] CreateNetworkTypeDto createNetworkTypeDto)
        {
            var result = await _networkTypeService.CreateNetworkTypes(createNetworkTypeDto);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }


        [HttpPost("ModifyNetworkType")]
        public async Task<IActionResult> Put([FromBody] ModifyNetworkTypeDto modifyNetworkTypeDto)
        {
            var result = await _networkTypeService.UpdateNetworkTypes(modifyNetworkTypeDto);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }


        [HttpPost("DisabledNetworkType")]
        public async Task<IActionResult> Put([FromBody] DisableNetworkTypeDto disableNetworkTypeDto)
        {
            var result = await _networkTypeService.DisabledNetworkTypes(disableNetworkTypeDto);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
