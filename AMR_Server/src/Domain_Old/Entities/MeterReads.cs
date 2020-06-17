using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class MeterReads
    {
        public decimal MeterReadId { get; set; }
        public DateTime? LastReadingDate { get; set; }
        public decimal? LastReadingValue { get; set; }
        public DateTime? LastReadingInsertDate { get; set; }
        public DateTime? FirstReadingDate { get; set; }
        public decimal? FirstReadingValue { get; set; }
        public DateTime? MaxReadingDate { get; set; }
        public string MeterId { get; set; }

        public virtual Meter Meter { get; set; }
    }
}
