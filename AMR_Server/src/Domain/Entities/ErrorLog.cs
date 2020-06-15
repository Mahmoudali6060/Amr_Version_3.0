using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class ErrorLog
    {
        public decimal ErrorLogId { get; set; }
        public decimal? PageId { get; set; }
        public decimal? PrivilegeId { get; set; }
        public decimal? ErrorInfoId { get; set; }
        public string ExceptionMessage { get; set; }
        public DateTime? ErrorDate { get; set; }
        public short? UserId { get; set; }

        public virtual ErrorInfo ErrorInfo { get; set; }
        public virtual Page Page { get; set; }
        public virtual Privilege Privilege { get; set; }
        public virtual UserBasicData User { get; set; }
    }
}
