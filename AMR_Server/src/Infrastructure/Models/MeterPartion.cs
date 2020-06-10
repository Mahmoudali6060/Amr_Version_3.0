using System;
using System.Collections.Generic;

namespace AMR_Server.Infrastructure.Models
{
    public partial class MeterPartion
    {
        public decimal MeterPartionId { get; set; }
        public string PartitionId { get; set; }
        public decimal? MinMeterId { get; set; }
        public decimal? MaxMeterId { get; set; }
    }
}
