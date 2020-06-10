using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class Page
    {
        public Page()
        {
            EditableColumn = new HashSet<EditableColumn>();
            ErrorLog = new HashSet<ErrorLog>();
            GlobalFilterPage = new HashSet<GlobalFilterPage>();
            PagePrivilege = new HashSet<PagePrivilege>();
            Permission = new HashSet<Permission>();
            TransactionLog = new HashSet<TransactionLog>();
        }

        public decimal PageId { get; set; }
        public string PageName { get; set; }
        public string PaegDescription { get; set; }
        public string Url { get; set; }
        public decimal? PageOrder { get; set; }
        public bool? IsActive { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual ICollection<EditableColumn> EditableColumn { get; set; }
        public virtual ICollection<ErrorLog> ErrorLog { get; set; }
        public virtual ICollection<GlobalFilterPage> GlobalFilterPage { get; set; }
        public virtual ICollection<PagePrivilege> PagePrivilege { get; set; }
        public virtual ICollection<Permission> Permission { get; set; }
        public virtual ICollection<TransactionLog> TransactionLog { get; set; }
    }
}
