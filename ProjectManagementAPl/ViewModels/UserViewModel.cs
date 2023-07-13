using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPl.ViewModels
{
    public class UserViewModel
    {
        public int user_id { get; set; }
        public string email_address { get; set; }
        public string password { get; set; }
        public string source { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public int role_id { get; set; }
        public int? pub_id { get; set; }
        public DateTime? hire_date { get; set; }

    }
}
