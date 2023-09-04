using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObjects.Entities
{
    public class Blog
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int blogId { get; set; }
        [Required]
        [StringLength(200)]
        public string title { get; set; }
        [Required]
        public string content { get; set; }
        public bool? status { get; set; }

    }
}
