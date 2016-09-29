using Microsoft.EntityFrameworkCore;
using Minor.Dag14.EntityFrameworkNorthWind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Minor.Dag14.EntityFrameworkNorthWind.DAL
{
    public class ProductRepository : IRepository<Products, int>
    {
        public void Insert(Products item)
        {
            using (var context = new NorthwindContext())
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }
                else
                {
                    var category = context.Categories.Where(p => p.CategoryId == item.CategoryId).FirstOrDefault();

                    if (category == null)
                    {
                        context.Categories.Add(item.Category);
                    }

                    context.Products.Add(item);
                    context.SaveChanges();
                }
            }
        }

        public void Update(Products item)
        {
            using (var context = new NorthwindContext())
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }
                else
                {
                    context.Products.Update(item);
                    context.SaveChanges();
                }

            }
        }

        public void Delete(Products item)
        {
            using (var context = new NorthwindContext())
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }
                else
                {
                    context.Products.Remove(item);
                    context.SaveChanges();
                }
            }
        }
        public Products Find(int id)
        {
            using (var context = new NorthwindContext())
            {
                var product = (from item in context.Products.Include(item => item.Category)
                               where item.ProductId == id
                               select item).SingleOrDefault();

                return product;
            }
        }
        public IEnumerable<Products> FindAll()
        {
            using (var context = new NorthwindContext())
            {
                var products = from product in context.Products.Include(item => item.Category)
                               select product;

                return products.ToArray();
            }
        }

        public IEnumerable<Products> FindBy(Expression<Func<Products, bool>> filter)
        {
            using (var context = new NorthwindContext())
            {
                if (filter == null)
                {
                    throw new ArgumentNullException();
                }
                else
                {
                    var products = context.Products.Include(item => item.Category).Where(filter);

                    return products.ToArray();
                }
            }
        }
    }
}
