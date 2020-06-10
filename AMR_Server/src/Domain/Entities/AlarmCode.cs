using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class AlarmCode
    {
        public AlarmCode()
        {
            MeterAlarm = new HashSet<MeterAlarm>();
            MeterAlarmRf = new HashSet<MeterAlarmRf>();
        }

        public decimal AlarmCodeId { get; set; }
        public string AlarmCodeName { get; set; }
        public string AlarmCodeDescription { get; set; }
        public short? AlarmCategoryId { get; set; }
        public short? AlarmLevelId { get; set; }
        public string Comments { get; set; }
        public bool? DeleteStatus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual AlarmCategory AlarmCategory { get; set; }
        public virtual AlarmLevel AlarmLevel { get; set; }
        public virtual ICollection<MeterAlarm> MeterAlarm { get; set; }
        public virtual ICollection<MeterAlarmRf> MeterAlarmRf { get; set; }
    }
}
