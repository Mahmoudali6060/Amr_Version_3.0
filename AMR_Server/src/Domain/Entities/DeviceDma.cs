using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class DeviceDma
    {
        public DeviceDma()
        {
            InverseParent = new HashSet<DeviceDma>();
            Meter = new HashSet<Meter>();
        }

        public decimal DmaId { get; set; }
        public string DmaName { get; set; }
        public decimal? ParentId { get; set; }
        public string FullPath { get; set; }
        public string FullName { get; set; }
        public bool? IsActive { get; set; }
        public string Comments { get; set; }
        public bool? DeleteStatus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public decimal? CityId { get; set; }

        public virtual City City { get; set; }
        public virtual UserBasicData CreatedUser { get; set; }
        public virtual DeviceDma Parent { get; set; }
        public virtual UserBasicData UpdatedUser { get; set; }
        public virtual ICollection<DeviceDma> InverseParent { get; set; }
        public virtual ICollection<Meter> Meter { get; set; }
    }
}
