using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _ctx;
        private readonly IHostingEnvironment _hosting;
        public DutchSeeder(DutchContext ctx, IHostingEnvironment hosting)
        {
            _ctx = ctx;
            _hosting = hosting;
        }
        public void Seed()
        {
            _ctx.Database.EnsureCreated();
            if (!_ctx.Products.Any())
            {
                //Seed the data
                //_hosting.ContentRootPath gives root directory
                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _ctx.Products.AddRange(products);

                var order = new Order()
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = "12121",
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.FirstOrDefault(),
                            Quantity = 6,
                            UnitPrice = products.FirstOrDefault().Price
                        }
                    }

                };
                _ctx.Orders.AddRange(order);
                _ctx.SaveChanges();

            }
        }
    }
}
