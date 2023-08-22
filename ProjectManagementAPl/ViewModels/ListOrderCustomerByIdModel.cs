using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPl.ViewModels
{
    public class ListOrderCustomerByIdModel
    {
        public int orderId { get; set; }
        public DateTime? requiredDate { get; set; }
        public int? totalmoney { get; set; }
        public string? phone { get; set; }
        public string? address { get; set; }
        public string? status { get; set; }
        

    }
}
