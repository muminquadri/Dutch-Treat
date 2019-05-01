using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<ProductsController> _logger;
        public OrdersController(IDutchRepository repository, ILogger<ProductsController> logger)
        {
            _repository = repository;
            _logger = logger;

        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAllOrders());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred:Failed to get products: {ex}");
                return BadRequest("Exception occurred:Failed to get products");
            }
        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {

            try
            {
                var order = _repository.GetOrderById(id);

                if (order != null)
                { return Ok(order); }
                else
                { return NotFound(); }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred:Failed to get products: {ex}");
                return BadRequest("Exception occurred:Failed to get products");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]Order model)
        {
            try
            {
                _repository.AddEntity(model);
                if (_repository.SaveAll())
                {
                    return Created($"api/orders/{model.Id}", model);
                }

            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception occurred:Post Call Failed: {ex}");
            }
            return BadRequest("Failed to save new order");
        }
    }
}