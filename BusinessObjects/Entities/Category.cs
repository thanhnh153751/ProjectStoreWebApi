using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObjects.Entities
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int categoryId { get; set; }
        [Required]
        [StringLength(100)]
        public string categoryName { get; set; }
        [Required]
        [StringLength(200)]
        public string? description { get; set; }
        public bool? status { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
