using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class ProfileType
    {
        public ProfileType()
        {
            GatewayProfile = new HashSet<GatewayProfile>();
            MeterProfile = new HashSet<MeterProfile>();
        }

        public decimal ProfileTypeId { get; set; }
        public string ProfileTypeName { get; set; }

        public virtual ICollection<GatewayProfile> GatewayProfile { get; set; }
        public virtual ICollection<MeterProfile> MeterProfile { get; set; }
    }
}
