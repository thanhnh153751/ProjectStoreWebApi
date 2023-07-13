using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPl.ViewModels
{
    public class PublisherViewModel
    {
        public int pub_id { get; set; }
        [Required]
        public string publisher_name { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }

    }
}
