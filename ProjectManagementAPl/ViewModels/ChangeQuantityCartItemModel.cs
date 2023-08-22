using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPl.ViewModels
{
    public class ChangeQuantityCartItemModel
    {       
        public int customerId { get; set; }
        public int productId { get; set; }
        public bool sign { get; set; }

    }
}
