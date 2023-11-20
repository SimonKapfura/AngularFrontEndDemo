using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DutchTreat.Data
{
    public class Repository : IRepository
    {
        private readonly Context _ctx;
        private readonly ILogger _logger;

        public Repository(Context ctx, ILogger<Repository> logger)
        {
            _ctx = ctx;
            _logger = logger;   
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            try
            {
                return _ctx.Orders
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to retrieve orders: {ex}");
                return null;
            }
        }
        public Order GetOrderByID(int id)
        {
            try
            {
                return _ctx.Orders
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .Where(o => o.Id == id)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to retrieve orders: {ex}");
                return null;
            }
        }
        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                return _ctx.Products
                .OrderBy(p => p.Title)
                .ToList();
            }
            catch(Exception ex) 
            {
                _logger.LogError($"Failed to retrieve products: {ex}");
                return null;
            }
        }

        

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            try
            {
                return _ctx.Products
                .Where(p => p.Category != category)
                .ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get Products by category: {ex}");
                return null;
            }
        }
        public bool SaveChanges()
        {
            return _ctx.SaveChanges() > 0;
        }

    }
}
