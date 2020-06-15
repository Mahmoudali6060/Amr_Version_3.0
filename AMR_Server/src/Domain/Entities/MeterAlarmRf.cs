using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class MeterAlarmRf
    {
        public decimal? AlarmId { get; set; }
        public string AlarmCodeId { get; set; }
        public string MeterId { get; set; }
        public decimal? ModuleId { get; set; }
        public bool? ModulePort { get; set; }
        public bool? MeterTypeId { get; set; }
        public DateTime? MeterAlarmDatetime { get; set; }
        public decimal? MeterGroupId { get; set; }
        public string TicketId { get; set; }
        public bool? DeleteStatus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public decimal MeterAlarmId { get; set; }

        public virtual AlarmCode Alarm { get; set; }
        public virtual UserBasicData CreatedUser { get; set; }
        public virtual Meter Meter { get; set; }
        public virtual MeterType MeterType { get; set; }
        public virtual UserBasicData UpdatedUser { get; set; }
    }
}
