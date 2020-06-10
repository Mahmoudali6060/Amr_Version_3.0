using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class GlobalFilterPage
    {
        public decimal GlobalFilterPageId { get; set; }
        public decimal? PageId { get; set; }
        public decimal? GlobalFilterId { get; set; }
        public string FilterQuery { get; set; }

        public virtual GlobalFilter GlobalFilter { get; set; }
        public virtual Page Page { get; set; }
    }
}
