using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class AlarmLevel
    {
        public AlarmLevel()
        {
            AlarmCode = new HashSet<AlarmCode>();
        }

        public short AlarmLevelId { get; set; }
        public string AlarmLevelName { get; set; }
        public string AlarmLevelDescription { get; set; }
        public string Comments { get; set; }
        public bool? DeleteStatus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public decimal? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<AlarmCode> AlarmCode { get; set; }
    }
}
