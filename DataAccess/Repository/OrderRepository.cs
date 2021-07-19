using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class OrderRepository : BaseRepository<Order>
    {
        //this class has new context instance for every action
        public OrderRepository() : base(new SaleContext())
        {

        }

        public Order Get(int primaryKey)
        {
            using var Context = DbContext as SaleContext;
            var order = Context.Orders.Find(primaryKey);
            Context.Entry(order).Collection("OrderDetails").Load();
            return order;
        }

        public IList<Order> Search(DateTime starDateTime, DateTime endDateTime, int id)
        {
            using var Context = DbContext as SaleContext;

            if (id==null||id==1)
            {
                return Context.Orders.Include("OrderDetails")
.Where(entity => (entity.OrderDate >= starDateTime && entity.OrderDate <= endDateTime))
.OrderByDescending(entity => entity.OrderDate).ToList();
            }
            else
                return Context.Orders.Include("OrderDetails")
                .Where(entity => (entity.MemberId==id &&entity.OrderDate >= starDateTime && entity.OrderDate <= endDateTime))
                .OrderByDescending(entity => entity.OrderDate).ToList();
           
        }
    }
}
