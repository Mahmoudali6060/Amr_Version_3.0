using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class PagePrivilege
    {
        public decimal PagePrivilegeId { get; set; }
        public decimal? PageId { get; set; }
        public decimal? PrivilegeId { get; set; }
        public bool? DeleteStatus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual UserBasicData CreatedUser { get; set; }
        public virtual Page Page { get; set; }
        public virtual Privilege Privilege { get; set; }
        public virtual UserBasicData UpdatedUser { get; set; }
    }
}
