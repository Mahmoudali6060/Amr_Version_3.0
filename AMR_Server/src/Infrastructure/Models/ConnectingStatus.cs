﻿using System;
using System.Collections.Generic;

namespace AMR_Server.Infrastructure.Models
{
    public partial class ConnectingStatus
    {
        public decimal ConnectingStatus1 { get; set; }
        public string ConnectingStatusName { get; set; }
        public bool? IsActive { get; set; }
        public bool? DeleteStatus { get; set; }
    }
}
