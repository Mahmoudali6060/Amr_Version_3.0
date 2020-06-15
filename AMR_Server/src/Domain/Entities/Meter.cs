using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class Meter
    {
        public Meter()
        {
            DeviceQueueAction = new HashSet<DeviceQueueAction>();
            MeterAlarm = new HashSet<MeterAlarm>();
            MeterAlarmConfig = new HashSet<MeterAlarmConfig>();
            MeterAlarmRf = new HashSet<MeterAlarmRf>();
            MeterBillingConfig = new HashSet<MeterBillingConfig>();
            MeterComments = new HashSet<MeterComments>();
            MeterProfile = new HashSet<MeterProfile>();
            MeterReading = new HashSet<MeterReading>();
            MeterReads = new HashSet<MeterReads>();
        }

        public string MeterId { get; set; }
        public string BadgeNo { get; set; }
        public string Hcn { get; set; }
        public decimal? GroupId { get; set; }
        public short? VendorId { get; set; }
        public short? ModelId { get; set; }
        public decimal? InventoryStaus { get; set; }
        public decimal? MeterStatusId { get; set; }
        public string SerialNo { get; set; }
        public DateTime? InstalationDate { get; set; }
        public DateTime? LastDatasetDate { get; set; }
        public DateTime? LastOnlineDate { get; set; }
        public bool? IsActive { get; set; }
        public string Comments { get; set; }
        public bool? DeleteStaus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDriveby { get; set; }
        public string OnlineStatusLastChangeDate { get; set; }
        public string SimCardNo { get; set; }
        public string CoordinatesType { get; set; }
        public bool? InStore { get; set; }
        public bool? IsSmartInCcb { get; set; }
        public bool? IsInControlRoom { get; set; }
        public bool? IsBulk { get; set; }
        public short? ModuleId { get; set; }
        public bool? ModulePort { get; set; }
        public short? ModuleVendorId { get; set; }
        public short? ModuleModelId { get; set; }
        public short? AreaId { get; set; }
        public string StreetName { get; set; }
        public string FloorNo { get; set; }
        public string PostalCode { get; set; }
        public decimal? CoordLat { get; set; }
        public decimal? CoordLon { get; set; }
        public decimal? DmaId { get; set; }
        public bool? MeterTypeId { get; set; }
        public decimal? ConnectingStatus { get; set; }
        public string Column2 { get; set; }

        public virtual UserBasicData CreatedUser { get; set; }
        public virtual DeviceDma Dma { get; set; }
        public virtual DeviceGroup Group { get; set; }
        public virtual MeterStatus MeterStatus { get; set; }
        public virtual MeterType MeterType { get; set; }
        public virtual MeterModel Model { get; set; }
        public virtual SimCardList SimCardNoNavigation { get; set; }
        public virtual UserBasicData UpdatedUser { get; set; }
        public virtual MeterVendor Vendor { get; set; }
        public virtual ICollection<DeviceQueueAction> DeviceQueueAction { get; set; }
        public virtual ICollection<MeterAlarm> MeterAlarm { get; set; }
        public virtual ICollection<MeterAlarmConfig> MeterAlarmConfig { get; set; }
        public virtual ICollection<MeterAlarmRf> MeterAlarmRf { get; set; }
        public virtual ICollection<MeterBillingConfig> MeterBillingConfig { get; set; }
        public virtual ICollection<MeterComments> MeterComments { get; set; }
        public virtual ICollection<MeterProfile> MeterProfile { get; set; }
        public virtual ICollection<MeterReading> MeterReading { get; set; }
        public virtual ICollection<MeterReads> MeterReads { get; set; }
    }
}
