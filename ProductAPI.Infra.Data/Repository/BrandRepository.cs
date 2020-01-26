using System;
using System.Collections.Generic;
using System.Text;
using ProductAPI.Domain.Entities;
using ProductAPI.Domain.Interfaces;
using ProductAPI.Infra.Data.Context;
using System.Linq;
using System.Xml.Linq;

namespace ProductAPI.Infra.Data.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private ApplicationDbContext _context;

        public BrandRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Insert(Brand brand)
        {
            _context.Set<Brand>().Add(brand);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Set<Brand>().Remove(Select(id));
            _context.SaveChanges();
        }

        public Brand Select(int id)
        {
            return _context.Set<Brand>().Find(id);
        }

        public IList<Brand> SelectBrands()
        {
            return _context.Set<Brand>().ToList();
        }

        public void Update(Brand obj)
        {
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public List<BrandElement> SelectBrandsWithTotalProducts()
        {
            return (from b in _context.Set<Brand>()
                    select new BrandElement
                    {
                        Id = b.Id,
                        Name = b.Name,
                        TotalProducts = _context.Product.Where(x => x.Brand_Id == b.Id).Count()
                    }).ToList();
        }
    }
}
