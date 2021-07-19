using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace BusinessObject
{
    public class Product
    {
        [Key] public int ProductId { set; get; }
        [Required] public int CategoryId { set; get; }
        [Required] [StringLength(40)] public string ProductName { set; get; }
        [Required] [StringLength(20)] public string Weight { set; get; }
        [Required] public decimal UnitPrice { set; get; }
        [Required] public int UnitInStock { set; get; }
    }
}