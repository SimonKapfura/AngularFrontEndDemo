using DutchTreat.Controllers;
using DutchTreat.Data.Entities;
using System.Collections.Generic;

namespace DutchTreat.Data
{
    public interface IRepository
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrderByID(int id);
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        void AddEntity(object model);
        bool SaveChanges();
        
    }
}