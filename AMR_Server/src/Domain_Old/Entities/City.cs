using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class City
    {
        public City()
        {
            DeviceDma = new HashSet<DeviceDma>();
            DeviceGroup = new HashSet<DeviceGroup>();
        }

        public decimal CityId { get; set; }
        public string CityName { get; set; }
        public bool? IsActive { get; set; }
        public string Comments { get; set; }
        public bool? DeleteStatus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<DeviceDma> DeviceDma { get; set; }
        public virtual ICollection<DeviceGroup> DeviceGroup { get; set; }
    }
}
