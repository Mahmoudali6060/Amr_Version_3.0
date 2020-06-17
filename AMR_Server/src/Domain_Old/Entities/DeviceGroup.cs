using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class DeviceGroup
    {
        public DeviceGroup()
        {
            Gateway = new HashSet<Gateway>();
            InverseParent = new HashSet<DeviceGroup>();
            Meter = new HashSet<Meter>();
            MeterAlarm = new HashSet<MeterAlarm>();
            UserGroup = new HashSet<UserGroup>();
        }

        public decimal GroupId { get; set; }
        public string GroupName { get; set; }
        public decimal? ParentId { get; set; }
        public string FullGroupPath { get; set; }
        public string FullGroupName { get; set; }
        public bool? IsActive { get; set; }
        public string Comments { get; set; }
        public bool? DeleteStatus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CcbDivision { get; set; }
        public string CcbAreaCode { get; set; }
        public bool? IsVirtual { get; set; }
        public decimal? CityId { get; set; }

        public virtual City City { get; set; }
        public virtual UserBasicData CreatedUser { get; set; }
        public virtual DeviceGroup Parent { get; set; }
        public virtual UserBasicData UpdatedUser { get; set; }
        public virtual ICollection<Gateway> Gateway { get; set; }
        public virtual ICollection<DeviceGroup> InverseParent { get; set; }
        public virtual ICollection<Meter> Meter { get; set; }
        public virtual ICollection<MeterAlarm> MeterAlarm { get; set; }
        public virtual ICollection<UserGroup> UserGroup { get; set; }
    }
}
