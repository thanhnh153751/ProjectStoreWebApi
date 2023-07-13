using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPl.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public int productId { get; set; }
        [Required]
        [StringLength(100)]
        public string productName { get; set; }
        [Required]
        public int unitPrice { get; set; }
        public int? unitsInStock { get; set; }
        public int? unitsOnOrder { get; set; }
        public string categoryName { get; set; }
        [StringLength(200)]
        public string? image { get; set; }
        [StringLength(300)]
        public string? description { get; set; }
        public bool? status { get; set; }

    }
}
