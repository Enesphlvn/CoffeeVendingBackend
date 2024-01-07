using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("getall")]
        public IActionResult GetAllStatistics() 
        {
            var result = _statisticsService.GetAllStatistics();
            if (result.Success) 
            {
               return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
