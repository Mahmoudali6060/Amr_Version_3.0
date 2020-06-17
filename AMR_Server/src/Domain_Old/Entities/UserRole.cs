using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class UserRole
    {
        public decimal UserRoleId { get; set; }
        public short? UserId { get; set; }
        public decimal? RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual UserBasicData User { get; set; }
    }
}
