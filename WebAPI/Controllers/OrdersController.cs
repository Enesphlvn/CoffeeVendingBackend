using Business.Abstract;
using Entities.DTOs.Order;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _orderService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("getorderdetails")]
        public IActionResult GetOrderDetails()
        {
            var result = _orderService.GetOrderDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }


        [HttpGet("getbyid")]
        public IActionResult GetById(int orderId)
        {
            var result = _orderService.GetById(orderId);

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
        public IActionResult Add(CreateOrderDto orderDto)
        {
            var result = _orderService.Add(orderDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(UpdateOrderDto orderDto)
        {
            var result = _orderService.Update(orderDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("delete")]
        public IActionResult Delete(int orderId)
        {
            var result = _orderService.Delete(orderId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("harddelete")]
        public IActionResult HardDelete(int orderId)
        {
            var result = _orderService.HardDelete(orderId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
