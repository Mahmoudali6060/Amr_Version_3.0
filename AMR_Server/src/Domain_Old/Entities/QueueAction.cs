using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class QueueAction
    {
        public QueueAction()
        {
            DeviceQueueAction = new HashSet<DeviceQueueAction>();
        }

        public decimal QueueActionId { get; set; }
        public string QueueActionName { get; set; }
        public string QueueActionComment { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedUserDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedUserDate { get; set; }
        public bool? DeleteStatus { get; set; }
        public bool? IsAutomatic { get; set; }
        public bool? IsSingle { get; set; }

        public virtual UserBasicData CreatedUser { get; set; }
        public virtual UserBasicData UpdatedUser { get; set; }
        public virtual ICollection<DeviceQueueAction> DeviceQueueAction { get; set; }
    }
}
