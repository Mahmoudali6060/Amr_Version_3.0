using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class GatewayProfile
    {
        public decimal GatewayProfile1 { get; set; }
        public decimal? GatewayId { get; set; }
        public decimal? ProfileTypeId { get; set; }
        public string Value { get; set; }
        public DateTime? TransactionDate { get; set; }

        public virtual ProfileType ProfileType { get; set; }
    }
}
