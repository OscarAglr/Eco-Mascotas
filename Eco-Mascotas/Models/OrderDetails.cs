using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Eco_Mascotas.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        [Display(Name = "Order")]
        public int OrderId { get; set; }
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
