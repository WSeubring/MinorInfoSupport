using Minor.Dag14.EntityFrameworkNorthWind.DAL;
using Minor.Dag14.EntityFrameworkNorthWind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.Dag14.EntityFrameworkNorthWind
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ProductRepository productRepository = new ProductRepository();
            var newProduct = new Products
            {
                ProductName = "SuchNameWow2",
                QuantityPerUnit = "0",
                Category = new Categories()
                {
                    CategoryName = "Kaas"
                }
            };
            productRepository.Insert(newProduct);
            var products = productRepository.FindBy(p => p.Category.CategoryName == newProduct.Category.CategoryName);

            foreach (var product in products)
            {
                Console.WriteLine($"{product?.ProductId} | {product?.ProductName} | {product?.QuantityPerUnit} | {product?.Category?.CategoryName} ");
            }



            //using (var context = new NorthwindContext())
            //{
            //    var productWithPriceOver100Query = from product in context.Products
            //                                       where product.UnitPrice > 100
            //                                       select product;
            //    foreach (var product in productWithPriceOver100Query)
            //    {
            //       Console.WriteLine($"{product.ProductName} - {product.UnitPrice}");
            //    }

            //    var BeverageCategoryQuery = from category in context.Categories
            //                                where category.CategoryName == "Beverages"
            //                                select category;

            //    var BeverageCatergory = BeverageCategoryQuery.First();

            //    var BeveragesQuery = from product in context.Products
            //                         where product.Category == BeverageCatergory
            //                         select product;

            //    foreach (var beverage in BeveragesQuery)
            //    {
            //        Console.WriteLine($"{beverage.ProductName} - {beverage.QuantityPerUnit}");
            //    }
            //}

        }
    }
}
