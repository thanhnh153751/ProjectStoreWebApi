using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPl.ViewModels
{
    public class ViewOrderDetailModel
    {
        public int orderId { get; set; }
        public string productName { get; set; }
        public string? image { get; set; }
        public int unitPrice { get; set; }
        public int quantity { get; set; }
    }
}
