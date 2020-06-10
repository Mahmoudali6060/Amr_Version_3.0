using System;
using System.Collections.Generic;

namespace AMR_Server.Infrastructure.Models
{
    public partial class ErrorInfo
    {
        public ErrorInfo()
        {
            ErrorLog = new HashSet<ErrorLog>();
        }

        public decimal ErrorInfoId { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ObjectName { get; set; }

        public virtual ICollection<ErrorLog> ErrorLog { get; set; }
    }
}
