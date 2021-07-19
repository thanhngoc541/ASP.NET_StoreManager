using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : BaseRepository<OrderDetail>
    {
        
        public OrderDetailRepository() : base(new SaleContext())
        {

        }

    }
}
