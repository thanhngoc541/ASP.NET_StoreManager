using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject
{
    [Index(nameof(MemberId))]
    public class Order
    {
        [Key] public int OrderId { set; get; }

        [Required] [ForeignKey("Member")] public int MemberId { set; get; }
        public Member Member { set; get; }

        [Required] public DateTime OrderDate { set; get; }
        public DateTime RequiredDate { set; get; }
        public DateTime ShippedDate { set; get; }
        public decimal Freight { set; get; }
        public ICollection<OrderDetail> OrderDetails { set; get; }
    }
}