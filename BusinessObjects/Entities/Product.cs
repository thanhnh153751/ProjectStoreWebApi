﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


#nullable disable

namespace BusinessObjects.Entities
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productId { get; set; }
        [Required]
        [StringLength(100)]
        public string productName { get; set; }
        [Required]
        public int unitPrice { get; set; }
        public int? unitsInStock { get; set; }
        public int? unitsOnOrder { get; set; }
        public int categoryId { get; set; }
        [StringLength(200)]
        public string? image { get; set; }
        [StringLength(300)]
        public string? description { get; set; }
        public bool? status { get; set; }

        public Category Category { get; set; }
        public ICollection<ViewProduct> ViewProducts { get; set; }
        public ICollection<OrdersDetail> OrdersDetails { get; set; }
    }
}
