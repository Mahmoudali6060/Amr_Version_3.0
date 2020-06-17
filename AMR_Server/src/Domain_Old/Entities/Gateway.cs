using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class Gateway
    {
        public Gateway()
        {
            DeviceQueueAction = new HashSet<DeviceQueueAction>();
            GatewayConnection = new HashSet<GatewayConnection>();
            MeterGatewayConfigLastDrivebyGateway = new HashSet<MeterGatewayConfig>();
            MeterGatewayConfigLastFixedGateway = new HashSet<MeterGatewayConfig>();
            MeterGatewayConfigLastGateway = new HashSet<MeterGatewayConfig>();
            MeterReading = new HashSet<MeterReading>();
        }

        public decimal GatewayId { get; set; }
        public string GatewayCode { get; set; }
        public decimal? GroupId { get; set; }
        public short? VendorId { get; set; }
        public short? ModelId { get; set; }
        public byte? InventoryStatus { get; set; }
        public bool? OnlineStatus { get; set; }
        public string StreetName { get; set; }
        public string HouseNo { get; set; }
        public string FloorNo { get; set; }
        public string PostalCode { get; set; }
        public decimal? CoordLat { get; set; }
        public decimal? CoordLon { get; set; }
        public DateTime? LastDatasetDate { get; set; }
        public DateTime? LastOnlineDate { get; set; }
        public bool? IsActive { get; set; }
        public string Comments { get; set; }
        public bool? DeleteStatus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdateUserId { get; set; }
        public string UpdatedDate { get; set; }
        public string SimCardNo { get; set; }
        public bool? IsFixed { get; set; }
        public string LocationName { get; set; }
        public decimal? LocationTypeId { get; set; }
        public bool? IsApproved { get; set; }
        public short? ApprovedUserId { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public decimal? ContractId { get; set; }
        public string FtpHostName { get; set; }
        public decimal? FtpPort { get; set; }
        public string FtpUsername { get; set; }
        public string FtpPassword { get; set; }
        public string FtpDirectory { get; set; }
        public string FtpGatewayNameSuffix { get; set; }
        public string FtpGatewayFolderName { get; set; }
        public bool? FtpIsFolderCreated { get; set; }

        public virtual UserBasicData ApprovedUser { get; set; }
        public virtual UserBasicData CreatedUser { get; set; }
        public virtual DeviceGroup Group { get; set; }
        public virtual MeterModel Model { get; set; }
        public virtual SimCardList SimCardNoNavigation { get; set; }
        public virtual UserBasicData UpdateUser { get; set; }
        public virtual MeterVendor Vendor { get; set; }
        public virtual MeterGatewayConfig MeterGatewayConfigMeterGatewayConfigNavigation { get; set; }
        public virtual ICollection<DeviceQueueAction> DeviceQueueAction { get; set; }
        public virtual ICollection<GatewayConnection> GatewayConnection { get; set; }
        public virtual ICollection<MeterGatewayConfig> MeterGatewayConfigLastDrivebyGateway { get; set; }
        public virtual ICollection<MeterGatewayConfig> MeterGatewayConfigLastFixedGateway { get; set; }
        public virtual ICollection<MeterGatewayConfig> MeterGatewayConfigLastGateway { get; set; }
        public virtual ICollection<MeterReading> MeterReading { get; set; }
    }
}
