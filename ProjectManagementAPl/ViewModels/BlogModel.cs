using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementAPl.ViewModels
{
    public class BlogModel
    {
        [Key]
        public int blogId { get; set; }
        [Required]
        [StringLength(200)]
        public string title_blog { get; set; }
        [Required]
        public string content_blog { get; set; }
        public bool? status { get; set; }

    }
}
