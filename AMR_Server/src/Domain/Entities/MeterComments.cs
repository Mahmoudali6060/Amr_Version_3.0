using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class MeterComments
    {
        public decimal CommentId { get; set; }
        public string MeterId { get; set; }
        public string Hcn { get; set; }
        public DateTime? CommentTypeId { get; set; }
        public string CommentText { get; set; }
        public DateTime? CommentDate { get; set; }
        public bool? CommentStatus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdateUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public short? ReviewedUserId { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public string ReviewText { get; set; }

        public virtual UserBasicData CreatedUser { get; set; }
        public virtual Meter Meter { get; set; }
        public virtual UserBasicData ReviewedUser { get; set; }
        public virtual UserBasicData UpdateUser { get; set; }
    }
}
