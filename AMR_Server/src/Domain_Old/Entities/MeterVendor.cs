using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class MeterVendor
    {
        public MeterVendor()
        {
            Gateway = new HashSet<Gateway>();
            Meter = new HashSet<Meter>();
            MeterModel = new HashSet<MeterModel>();
        }

        public short VendorId { get; set; }
        public string VendorName { get; set; }
        public bool? IsActive { get; set; }
        public string Comments { get; set; }
        public bool? DeleteStatus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual UserBasicData CreatedUser { get; set; }
        public virtual UserBasicData UpdatedUser { get; set; }
        public virtual ICollection<Gateway> Gateway { get; set; }
        public virtual ICollection<Meter> Meter { get; set; }
        public virtual ICollection<MeterModel> MeterModel { get; set; }
    }
}
