using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPl.ViewModels
{
    public class CategoryModelApi
    {
        public int categoryId { get; set; }
        [Required]
        [StringLength(100)]
        public string categoryName { get; set; }
        [Required]
        [StringLength(200)]
        public string? description { get; set; }
        public bool? status { get; set; }

    }
}
