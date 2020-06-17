using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class SimCardList
    {
        public SimCardList()
        {
            Gateway = new HashSet<Gateway>();
            Meter = new HashSet<Meter>();
        }

        public string SimCardNo { get; set; }

        public virtual ICollection<Gateway> Gateway { get; set; }
        public virtual ICollection<Meter> Meter { get; set; }
    }
}
