using Business.Abstract;
using Entities.DTOs.ProductContent;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductContentsController : ControllerBase
    {
        IProductContentService _productContentService;

        public ProductContentsController(IProductContentService productContentService)
        {
            _productContentService = productContentService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _productContentService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("getproductcontentdetails")]
        public IActionResult GetProductContentDetails()
        {
            var result = _productContentService.GetProductContentDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int productContentId)
        {
            var result = _productContentService.GetById(productContentId);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("add")]
        public IActionResult Add(CreateProductContentDto productContentDto)
        {
            var result = _productContentService.Add(productContentDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(UpdateProductContentDto productContentDto)
        {
            var result = _productContentService.Update(productContentDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("delete")]
        public IActionResult Delete(int productContentId)
        {
            var result = _productContentService.Delete(productContentId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("harddelete")]
        public IActionResult HardDelete(int productContentId)
        {
            var result = _productContentService.HardDelete(productContentId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
