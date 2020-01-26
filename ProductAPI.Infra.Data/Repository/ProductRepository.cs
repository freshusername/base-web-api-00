using System;
using System.Collections.Generic;
using System.Text;
using ProductAPI.Domain.Entities;
using ProductAPI.Domain.Interfaces;
using ProductAPI.Infra.Data.Context;
using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Infra.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Insert(Product product)
        {
            _context.Set<Product>().Add(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Set<Product>().Remove(Select(id));
            _context.SaveChanges();
        }

        public Product Select(int id)
        {
            return _context.Set<Product>().Find(id);
        }

        public IQueryable<Object> SelectProducts()
        {
            return from p in _context.Set<Product>()
                   join b in _context.Set<Brand>() on p.Brand_Id equals b.Id
                   where p.Active == true
                   select new
                   {
                       id = p.Id,
                       name = p.Name,
                       unit = p.Unit,
                       quantity = p.Quantity,
                       price = p.Price,
                       active = p.Active,
                       brand_Id = p.Brand_Id,
                       brand_name = b.Name
                   };
        }

        public void Update(Product product)
        {
            _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public int CountActiveProducts()
        {
            return _context.Product.Count(p => p.Active == true);
        }
    }
}
