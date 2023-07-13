using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObjects.Entities
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderId { get; set; }
        public int customerId { get; set; }
        public DateTime? orderDate { get; set; }
        public DateTime? requiredDate { get; set; }
        public DateTime? shippedDate { get; set; }
        public int? totalmoney { get; set; }
        [StringLength(20)]
        public string? status { get; set; }

        public Customer Customer { get; set; }
        public ICollection<OrdersDetail> OrdersDetails { get; set; }
    }
}
