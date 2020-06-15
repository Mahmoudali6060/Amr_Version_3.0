using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class Unit
    {
        public Unit()
        {
            MeterReading = new HashSet<MeterReading>();
        }

        public decimal UnitId { get; set; }
        public string Column1 { get; set; }
        public string UnitLabel { get; set; }
        public string UnitDescription { get; set; }
        public string MeasurementType { get; set; }
        public bool? IsActive { get; set; }
        public string Comments { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public decimal? ConversionFactor { get; set; }

        public virtual UserBasicData CreatedUser { get; set; }
        public virtual UserBasicData UpdatedUser { get; set; }
        public virtual ICollection<MeterReading> MeterReading { get; set; }
    }
}
