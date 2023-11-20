using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DutchTreat.Data
{
    public class Seeder
    {
        private readonly Context _cntxt;
        private readonly IWebHostEnvironment _env;

        public Seeder(Context cntxt, IWebHostEnvironment env)
        {
            _cntxt = cntxt;
            _env = env;
        }
        public void Seed()
        {
            _cntxt.Database.EnsureCreated();

            if (!_cntxt.Products.Any())
            {
                var filePath = Path.Combine("Data/art.json");
                var json = File.ReadAllText(filePath);
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);
                _cntxt.Products.AddRange(products);

                var order = new Order()
                {
                    OrderDate = DateTime.Today,
                    OrderNumber = "1000",
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                };
                _cntxt.Orders.Add(order);
                _cntxt.SaveChanges();
            }
        }
    }
}
