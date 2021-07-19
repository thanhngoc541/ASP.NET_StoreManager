using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class ProductRepository : BaseRepository<Product>
    {
        //each class has their own Database Context (for design reason)
        private static readonly DbContext context = new SaleContext();

        public ProductRepository() : base(context)
        {

        }

        public IList<Product> Search(string name)
        {
           // using var Context = DbContext as SaleContext;


            return GetAll().Where(entity => (entity.ProductName.Contains(name))).ToList();
//.OrderByDescending(entity => entity.ProductName.OrderBy(c=>c)).ToList();
            

        }
        public void Update(Product product)
        {
            var myProduct=Get(product.ProductId);
            myProduct.ProductName = product.ProductName;
            myProduct.UnitInStock = product.UnitInStock;
            myProduct.UnitPrice = product.UnitPrice;
            myProduct.Weight = product.Weight;
            myProduct.CategoryId = product.CategoryId;
            context.Entry<Product>(myProduct).State = EntityState.Modified;

        }
        public void SubstractStock(int id, int unit)
        {
            var product = Get(id);
            product.UnitInStock -= unit;
            context.SaveChanges();
        }
    }
}
