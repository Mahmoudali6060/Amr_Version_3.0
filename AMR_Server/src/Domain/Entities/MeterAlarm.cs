using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class MeterAlarm
    {
        public decimal MeterAlarmId { get; set; }
        public decimal? AlarmCodeId { get; set; }
        public string MeterId { get; set; }
        public string ModuleId { get; set; }
        public bool? ModulePort { get; set; }
        public bool? MeterTypeId { get; set; }
        public DateTime? MeterAlarmDatetime { get; set; }
        public decimal? MeterGroupId { get; set; }
        public string TicketId { get; set; }
        public bool? AlarmInformed { get; set; }
        public bool? DeleteStatus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual AlarmCode AlarmCode { get; set; }
        public virtual UserBasicData CreatedUser { get; set; }
        public virtual Meter Meter { get; set; }
        public virtual DeviceGroup MeterGroup { get; set; }
        public virtual MeterType MeterType { get; set; }
        public virtual UserBasicData UpdatedUser { get; set; }
    }
}
