﻿using System;
using System.Collections.Generic;

namespace AMR_Server.Infrastructure.Models
{
    public partial class MeterStatus
    {
        public MeterStatus()
        {
            Meter = new HashSet<Meter>();
        }

        public decimal StatusId { get; set; }
        public string StatusName { get; set; }
        public bool? IsActive { get; set; }
        public string Comments { get; set; }
        public bool? DeleteStatus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Users CreatedUser { get; set; }
        public virtual Users UpdatedUser { get; set; }
        public virtual ICollection<Meter> Meter { get; set; }
    }
}
