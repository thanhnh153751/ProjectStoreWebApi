using System.ComponentModel.DataAnnotations;

namespace DataAccess.ModelViewOdata
{
    public class ProductViewCount
    {
        public int ProductId { get; set; }
        public int TotalViewNumber { get; set; }
    }
}
