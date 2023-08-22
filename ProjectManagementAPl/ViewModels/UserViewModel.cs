using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementAPl.ViewModels
{
    public class UserViewModel
    {
        public int customerId { get; set; }
        [StringLength(100)]
        public string? contactName { get; set; }
        [StringLength(200)]
        public string? address { get; set; }
        [StringLength(20)]
        public string? phone { get; set; }
        [StringLength(50)]
        public string username { get; set; }
        [StringLength(50)]
        public string password { get; set; }
        [StringLength(200)]
        public string? image { get; set; }
        public int? roleId { get; set; }
        public int? status { get; set; }

    }
}
