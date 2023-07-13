using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace BusinessObjects.Entities
{
    public class ViewProduct
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int viewId { get; set; }
        public int productId { get; set; }
        public int? viewNumber { get; set; }
        public DateTime? viewdate { get; set; }

        public Product Product { get; set; }
    }
}
