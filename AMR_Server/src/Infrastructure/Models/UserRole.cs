using System;
using System.Collections.Generic;

namespace AMR_Server.Infrastructure.Models
{
    public partial class UserRole
    {
        public decimal UserRoleId { get; set; }
        public short? UserId { get; set; }
        public decimal? RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Users User { get; set; }
    }
}
