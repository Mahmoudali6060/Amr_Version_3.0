using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class MeterBillingConfig
    {
        public string MeterId { get; set; }
        public decimal? BillingCycleId { get; set; }
        public DateTime? BillingCycleDate { get; set; }
        public DateTime? NextBillingCycleStartDate { get; set; }
        public DateTime? NextBillingCycleEndDate { get; set; }
        public DateTime? PreviousReadingDate { get; set; }
        public decimal? PreviousReadingValue { get; set; }
        public decimal MeterBillingConfigId { get; set; }

        public virtual Meter Meter { get; set; }
    }
}
