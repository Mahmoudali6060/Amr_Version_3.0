using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class ObisData
    {
        public ObisData()
        {
            MeterReading = new HashSet<MeterReading>();
        }

        public decimal ObisId { get; set; }
        public string ObisCode { get; set; }
        public string ObisDescription { get; set; }
        public string ObisLable { get; set; }
        public short? ObisTypeId { get; set; }
        public short? ObisMediaId { get; set; }
        public short? MeasurementChannelId { get; set; }
        public short? MeasurandId { get; set; }
        public short? MeasurmentParameter { get; set; }
        public short? ReportingType { get; set; }
        public bool? IsActive { get; set; }
        public string Comments { get; set; }
        public bool? DeleteStatus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual UserBasicData CreatedUser { get; set; }
        public virtual UserBasicData UpdatedUser { get; set; }
        public virtual ICollection<MeterReading> MeterReading { get; set; }
    }
}
