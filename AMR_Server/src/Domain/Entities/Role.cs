using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class Role
    {
        public Role()
        {
            Permission = new HashSet<Permission>();
            UserRole = new HashSet<UserRole>();
        }

        public decimal RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual ICollection<Permission> Permission { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
