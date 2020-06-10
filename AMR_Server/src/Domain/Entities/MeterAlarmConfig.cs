using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class MeterAlarmConfig
    {
        public string MeterId { get; set; }
        public bool? LowBattery { get; set; }
        public bool? Leak { get; set; }
        public bool? MagneticTamper { get; set; }
        public bool? MeterError { get; set; }
        public bool? BackFlow { get; set; }
        public bool BrokenPipe { get; set; }
        public string EmptyPipe { get; set; }
        public bool? SpecificError { get; set; }
        public short MeterAlarmConfigId { get; set; }

        public virtual Meter Meter { get; set; }
    }
}
