using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
   public class EfProductDal:EfEntityRepositoryBase<Product,NorthwindContext>,IProductDal
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            using (NorthwindContext context=new NorthwindContext())
            {
                var deger = from x in context.Products
                    join c in context.Categories on x.CategoryId equals c.CategoryId
                    select new ProductDetailDto
                    {
                        ProductId = x.ProductId, ProductName = x.ProductName, CategoryName = c.CategoryName,
                        UnitsInStock = x.UnitsInStock
                    };

                return deger.ToList();
            }
        }
    }
}
