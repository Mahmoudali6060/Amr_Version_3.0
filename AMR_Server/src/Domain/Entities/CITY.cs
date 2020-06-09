using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMR_Server.Domain.Entities
{
    [Table("CITY", Schema = "AMR")]
    public class CITY 
    {
        [Column("CITY_ID")]
        public Nullable<int> CITY_ID { get; set; }
        [Column("CITY_NAME")]
        public string CITY_NAME { get; set; }
        [Column("IS_ACTIVE")]
        public Nullable<int> IS_ACTIVE { get; set; }
        [Column("COMMENTS")]
        public string COMMENTS { get; set; }
        [Column("DELETE_STATUS")]
        public Nullable<int> DELETE_STATUS { get; set; }
        [Column("CREATED_USER_ID")]
        public Nullable<int> CREATED_USER_ID { get; set; }
        [Column("CREATED_DATE")]
        public Nullable<DateTime> CREATED_DATE { get; set; }
        [Column("UPDATED_USER_ID")]
        public Nullable<int> UPDATED_USER_ID { get; set; }
        [Column("UPDATED_DATE")]
        public Nullable<DateTime> UPDATED_DATE { get; set; }
    }
}
