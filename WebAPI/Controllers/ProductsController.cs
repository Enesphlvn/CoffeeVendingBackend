﻿using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs.Product;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int productId)
        {
            var result = _productService.GetById(productId);

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
        public IActionResult Add(CreateProductDto productDto)
        {
            var result = _productService.Add(productDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPut("update")]
        public IActionResult Update(UpdateProductDto productDto)
        {
            var result = _productService.Update(productDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPut("delete")]
        public IActionResult Delete(int productId)
        {
            var result = _productService.Delete(productId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpDelete("harddelete")]
        public IActionResult HardDelete(int productId)
        {
            var result = _productService.HardDelete(productId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
