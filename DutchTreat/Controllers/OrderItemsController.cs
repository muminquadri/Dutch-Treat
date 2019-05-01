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
    [Route("api/orders/{orderid}/items")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<OrderItemsController> _logger;
        public OrderItemsController(IDutchRepository repository, ILogger<OrderItemsController> logger)
        {
            _repository = repository;
            _logger = logger;

        }
        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = _repository.GetOrderById(orderId);
            if(order != null)
            {
                return Ok(Mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
            }
            return NotFound();
        }
        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            var order = _repository.GetOrderById(orderId);
            if(order != null)
            {
                var item = order.Items.Where(t => t.Id == id).FirstOrDefault();
                if(item != null)
                {
                    return Ok(Mapper.Map<OrderItem,OrderItemViewModel>(item));
                }
                return NotFound();
            }
            return NotFound();

        }
    }
}