using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class MeterModel
    {
        public MeterModel()
        {
            Gateway = new HashSet<Gateway>();
            Meter = new HashSet<Meter>();
        }

        public short ModelId { get; set; }
        public string ModelName { get; set; }
        public short? VendorId { get; set; }
        public bool? DeviceTypeId { get; set; }
        public bool? IsActive { get; set; }
        public string Comments { get; set; }
        public bool? DeleteStatus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public decimal? MaxAcceptedReading { get; set; }
        public string ConnectionType { get; set; }

        public virtual UserBasicData CreatedUser { get; set; }
        public virtual MeterType DeviceType { get; set; }
        public virtual UserBasicData UpdatedUser { get; set; }
        public virtual MeterVendor Vendor { get; set; }
        public virtual ICollection<Gateway> Gateway { get; set; }
        public virtual ICollection<Meter> Meter { get; set; }
    }
}
