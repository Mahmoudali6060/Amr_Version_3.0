using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class UserGroup
    {
        public decimal UserGroupId { get; set; }
        public short? UserId { get; set; }
        public bool? IsActive { get; set; }
        public bool? DeleteStatus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserD { get; set; }
        public decimal? UpdatedDate { get; set; }
        public decimal? DeviceGroupId { get; set; }

        public virtual UserBasicData CreatedUser { get; set; }
        public virtual DeviceGroup DeviceGroup { get; set; }
        public virtual UserBasicData UpdatedUserDNavigation { get; set; }
        public virtual UserBasicData User { get; set; }
    }
}
