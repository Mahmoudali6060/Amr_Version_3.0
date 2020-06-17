using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class Area
    {
        public short? AreaId { get; set; }
        public string AreaName { get; set; }
        public short? CityId { get; set; }
        public bool? IsActive { get; set; }
        public string Comments { get; set; }
        public bool? DeleteStatus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
