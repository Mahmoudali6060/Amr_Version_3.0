using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class GatewayConnection
    {
        public decimal ConnectionId { get; set; }
        public decimal? GatewayId { get; set; }
        public DateTime? ConnectionDatetime { get; set; }
        public decimal? ReadingsCount { get; set; }
        public string FileName { get; set; }
        public decimal? FileSize { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual UserBasicData CreatedUser { get; set; }
        public virtual Gateway Gateway { get; set; }
        public virtual UserBasicData UpdatedUser { get; set; }
    }
}
