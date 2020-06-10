using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class Persistedgrants
    {
        public string Key { get; set; }
        public string Type { get; set; }
        public string Subjectid { get; set; }
        public string Clientid { get; set; }
        public DateTime Creationtime { get; set; }
        public DateTime? Expiration { get; set; }
        public string Data { get; set; }
    }
}
