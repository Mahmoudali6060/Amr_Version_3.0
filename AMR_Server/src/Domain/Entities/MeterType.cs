using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class MeterType
    {
        public MeterType()
        {
            Meter = new HashSet<Meter>();
            MeterAlarm = new HashSet<MeterAlarm>();
            MeterAlarmRf = new HashSet<MeterAlarmRf>();
            MeterModel = new HashSet<MeterModel>();
        }

        public bool MeterTypeId { get; set; }
        public string MeterTypeCode { get; set; }
        public string MeterTypeName { get; set; }
        public string MeterTypeDescription { get; set; }
        public bool? IsActive { get; set; }
        public string Comments { get; set; }
        public bool? DeleteStatus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual UserBasicData CreatedUser { get; set; }
        public virtual UserBasicData UpdatedUser { get; set; }
        public virtual ICollection<Meter> Meter { get; set; }
        public virtual ICollection<MeterAlarm> MeterAlarm { get; set; }
        public virtual ICollection<MeterAlarmRf> MeterAlarmRf { get; set; }
        public virtual ICollection<MeterModel> MeterModel { get; set; }
    }
}
