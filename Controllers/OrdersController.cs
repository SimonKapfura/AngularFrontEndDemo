using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    public class OrdersController : Controller
    {
        private readonly IRepository _repository;
        private readonly ILogger _logger;
        public OrdersController(IRepository repository, ILogger<OrdersController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGet]
        public ActionResult GetOrders()
        {
            try
            {
                return Ok(_repository.GetAllOrders());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }
        [HttpGet("{id:int}")]
        public ActionResult GetOrder(int id)
        {
            try
            {
               var order = _repository.GetOrderByID(id);
                if(order != null)
                {
                    return Ok(order);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get order: {ex}");
                return BadRequest("Failed to get order");
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody]Order model)
        {
            try
            {
                 _repository.AddEntity(model);
                if(_repository.SaveChanges())
                {
                    return Created($"/api/orders/{model.Id}", model);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex}");
            }
            return BadRequest("Failed to create order");
        }
    }
}
