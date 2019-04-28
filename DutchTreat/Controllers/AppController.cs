using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly IMailService _mailService;
        public AppController(IMailService mailService, IDutchRepository repository)
        {
            _repository = repository;
            _mailService = mailService;
    }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("contact")]
        public IActionResult Contact()
        {           
            //ViewBag.Title = "Contact Us";
           // throw new InvalidOperationException("Exception here to check for production error page");
            return View();
        }
        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if(ModelState.IsValid)
            {
                _mailService.SendMail("mumin.quadri@gmail.com", model.Subject,$"From: {model.Name} - { model.Email }, Message: {model.Message}");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }
            return View();
        }
        public IActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }
        public IActionResult Shop()
        {
            //var results = _context.Products.OrderBy(p => p.Category).ToList();
            //var results = from t in _context.Products
            //              orderby t.Category
            //              select t;
            var results = _repository.GetAllProducts();
            return View(results);
        }
    }
}
