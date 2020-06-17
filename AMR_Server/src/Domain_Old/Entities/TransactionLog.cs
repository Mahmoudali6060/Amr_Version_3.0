using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class TransactionLog
    {
        public decimal TransactionLogId { get; set; }
        public decimal? PageId { get; set; }
        public decimal? PrivilegeId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public short? UserId { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public virtual Page Page { get; set; }
        public virtual Privilege Privilege { get; set; }
        public virtual UserBasicData User { get; set; }
    }
}
