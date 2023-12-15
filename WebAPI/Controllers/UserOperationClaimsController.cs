using Business.Abstract;
using Entities.DTOs.UserOperationClaim;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : ControllerBase
    {
        IUserOperationClaimService _userOperationClaimService;

        public UserOperationClaimsController(IUserOperationClaimService userOperationClaimService)
        {
            _userOperationClaimService = userOperationClaimService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _userOperationClaimService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getuseroperationclaimdetail")]
        public IActionResult GetUserOperationClaimDetails()
        {
            var result = _userOperationClaimService.GetUserOperationClaimDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(CreateUserOperationClaimDto userOperationClaimDto)
        {
            var result = _userOperationClaimService.Add(userOperationClaimDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(UpdateUserOperationClaimDto userOperationClaimDto)
        {
            var result = _userOperationClaimService.Update(userOperationClaimDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("delete")]
        public IActionResult Delete(int userOperationClaimId)
        {
            var result = _userOperationClaimService.Delete(userOperationClaimId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("harddelete")]
        public IActionResult HardDelete(int userOperationClaimId)
        {
            var result = _userOperationClaimService.HardDelete(userOperationClaimId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
