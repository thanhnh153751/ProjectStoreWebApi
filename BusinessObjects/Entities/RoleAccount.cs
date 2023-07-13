using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObjects.Entities
{
    public class RoleAccount
    {
        [Key]
        public int roleId { get; set; }
        [StringLength(100)]
        public string roleName { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
