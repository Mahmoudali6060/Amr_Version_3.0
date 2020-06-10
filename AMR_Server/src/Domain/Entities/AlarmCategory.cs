using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class AlarmCategory
    {
        public AlarmCategory()
        {
            AlarmCode = new HashSet<AlarmCode>();
        }

        public short AlarmCategoryId { get; set; }
        public string AlarmCategoryName { get; set; }
        public string AlarmCategoryDescription { get; set; }
        public string Comments { get; set; }
        public bool? DeleteStatus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<AlarmCode> AlarmCode { get; set; }
    }
}
