using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;

namespace BookStore.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ApplicationDbContext _context { get; }
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product obj)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == obj.Id);
            if (product != null)
            {
                if (obj.ImageUrl != null)
                {
                    product.ImageUrl = obj.ImageUrl;
                }
                product.Title = obj.Title;
                product.Description = obj.Description;
                product.ISBN = obj.ISBN;
                product.Author = obj.Author;
                product.ListPrice = obj.ListPrice;
                product.Price = obj.Price;
                product.Price50 = obj.Price50;
                product.Price100 = obj.Price100;
                product.CategoryId = obj.CategoryId;
                product.CoverTypeId = obj.CoverTypeId;
            }
        }
    }
}
