using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class GlobalFilter
    {
        public GlobalFilter()
        {
            GlobalFilterPage = new HashSet<GlobalFilterPage>();
        }

        public decimal GlobalFilterId { get; set; }
        public string FilterName { get; set; }
        public string FilterDescription { get; set; }
        public bool? IsActive { get; set; }
        public bool? DeleteStatus { get; set; }
        public string LookupTableQuery { get; set; }

        public virtual ICollection<GlobalFilterPage> GlobalFilterPage { get; set; }
    }
}
