using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject
{
    [Index(nameof(OrderId), nameof(ProductId))]
    public class OrderDetail
    {
        [Required] [ForeignKey("Order")] public int OrderId { set; get; }
        public Order Order { set; get; }

        [Required] [ForeignKey("Product")] public int ProductId { set; get; }
        public Product Product { set; get; }

        [Required] public decimal UnitPrice { set; get; }
        [Required] public int Quantity { set; get; }

        [Required] public float Discount { set; get; }
    }
}