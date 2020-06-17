using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class EditableColumn
    {
        public decimal EditableColumnId { get; set; }
        public string ColumName { get; set; }
        public string TableName { get; set; }
        public decimal? PageId { get; set; }
        public bool? IsActive { get; set; }
        public bool? DeleteStatus { get; set; }

        public virtual Page Page { get; set; }
    }
}
