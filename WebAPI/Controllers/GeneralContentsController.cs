using Business.Abstract;
using Entities.DTOs.GeneralContent;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralContentsController : ControllerBase
    {
        IGeneralContentService _generalContentService;

        public GeneralContentsController(IGeneralContentService generalContentService)
        {
            _generalContentService = generalContentService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _generalContentService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int generalContentId)
        {
            var result = _generalContentService.GetById(generalContentId);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(CreateGeneralContentDto generalContentDto)
        {
            var result = _generalContentService.Add(generalContentDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(UpdateGeneralContentDto generalContentDto)
        {
            var result = _generalContentService.Update(generalContentDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("delete")]
        public IActionResult Delete(int generalContentId)
        {
            var result = _generalContentService.Delete(generalContentId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("harddelete")]
        public IActionResult HardDelete(int generalContentId)
        {
            var result = _generalContentService.HardDelete(generalContentId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}