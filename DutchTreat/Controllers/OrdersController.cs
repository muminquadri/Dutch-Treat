using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
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
        public IActionResult Get(bool includeItems = true)
        {
            try
            {
                var results = _repository.GetAllOrders(includeItems);
                return Ok(Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(results));
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
               // var results = Mapper.Map<Order, OrderViewModel>(order);
                if (order != null)
                { return Ok(Mapper.Map<Order, OrderViewModel>(order)); }
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
        public IActionResult Post([FromBody]OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = Mapper.Map<OrderViewModel, Order>(model);
                    if(newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }
                    _repository.AddEntity(newOrder);
                    if (_repository.SaveAll())
                    {
                        return Created($"api/orders/{newOrder.Id}", Mapper.Map<Order, OrderViewModel>(newOrder));
                    }
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