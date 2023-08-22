using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPl.ViewModels
{
    public class AllCartItemModel
    {
        public int orderId { get; set; }
        public int productId { get; set; }
        public string productName { get; set; }
        public int unitPrice { get; set; }
        public string? image { get; set; }
        public int quantity { get; set; }
        public int totalBill { get; set; }
    }
}
