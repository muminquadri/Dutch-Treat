﻿using DutchTreat.Services;
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
        private readonly IMailService _mailService;
        public AppController(IMailService mailService)
        {
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
    }
}
