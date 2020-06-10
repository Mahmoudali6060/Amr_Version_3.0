using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class Privilege
    {
        public Privilege()
        {
            ErrorLog = new HashSet<ErrorLog>();
            PagePrivilege = new HashSet<PagePrivilege>();
            Permission = new HashSet<Permission>();
            TransactionLog = new HashSet<TransactionLog>();
        }

        public decimal PrivilegeId { get; set; }
        public string PrivilegeName { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual ICollection<ErrorLog> ErrorLog { get; set; }
        public virtual ICollection<PagePrivilege> PagePrivilege { get; set; }
        public virtual ICollection<Permission> Permission { get; set; }
        public virtual ICollection<TransactionLog> TransactionLog { get; set; }
    }
}
