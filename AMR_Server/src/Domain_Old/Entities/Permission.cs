using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class Permission
    {
        public decimal PermissionId { get; set; }
        public decimal? RoleId { get; set; }
        public decimal? PrivilegeId { get; set; }
        public decimal? PageId { get; set; }

        public virtual Page Page { get; set; }
        public virtual Privilege Privilege { get; set; }
        public virtual Role Role { get; set; }
    }
}
