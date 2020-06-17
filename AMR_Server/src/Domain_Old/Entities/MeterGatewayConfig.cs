using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class MeterGatewayConfig
    {
        public decimal? LastGatewayId { get; set; }
        public decimal? DrivebyAddrCoordLat { get; set; }
        public decimal? DrivebyAddrCoordLon { get; set; }
        public DateTime? DrivebyAddrCoordDate { get; set; }
        public decimal? LastFixedGatewayId { get; set; }
        public decimal? LastFixedGattewayReadValue { get; set; }
        public DateTime? LastFixedGattewayReadDate { get; set; }
        public decimal? LastDrivebyGatewayId { get; set; }
        public decimal? LastDrivebyGatewayReadValue { get; set; }
        public DateTime? LastDrivebyGatewayReadDate { get; set; }
        public decimal MeterGatewayConfigId { get; set; }
        public short? MeterGatewayId { get; set; }

        public virtual Gateway LastDrivebyGateway { get; set; }
        public virtual Gateway LastFixedGateway { get; set; }
        public virtual Gateway LastGateway { get; set; }
        public virtual Gateway MeterGatewayConfigNavigation { get; set; }
    }
}
