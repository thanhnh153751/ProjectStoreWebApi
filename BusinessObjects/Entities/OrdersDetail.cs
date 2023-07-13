using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObjects.Entities
{
    public class OrdersDetail
    {
        [Key]
        public int orderId { get; set; }
        [Key]
        public int productId { get; set; }
        public int quantity { get; set; }
        public int unitPrice { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
