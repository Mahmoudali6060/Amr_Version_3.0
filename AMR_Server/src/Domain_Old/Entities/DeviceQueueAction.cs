using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class DeviceQueueAction
    {
        public decimal DeviceQueueActionId { get; set; }
        public string MeterId { get; set; }
        public decimal? QueueActionId { get; set; }
        public string Source { get; set; }
        public string RequesterId { get; set; }
        public byte? ExecuteStatus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? DeleteStatus { get; set; }
        public decimal? GatewayId { get; set; }
        public bool? IsManual { get; set; }

        public virtual UserBasicData CreatedUser { get; set; }
        public virtual Gateway Gateway { get; set; }
        public virtual Meter Meter { get; set; }
        public virtual QueueAction QueueAction { get; set; }
        public virtual UserBasicData UpdatedUser { get; set; }
    }
}
