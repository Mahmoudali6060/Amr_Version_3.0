using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class MeterProfile
    {
        public decimal MeterProfileId { get; set; }
        public string MeterId { get; set; }
        public decimal? ProfileTypeId { get; set; }
        public string Value { get; set; }
        public DateTime? TransactionDate { get; set; }

        public virtual Meter Meter { get; set; }
        public virtual ProfileType ProfileType { get; set; }
    }
}
