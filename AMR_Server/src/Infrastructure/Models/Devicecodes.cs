using System;
using System.Collections.Generic;

namespace AMR_Server.Infrastructure.Models
{
    public partial class Devicecodes
    {
        public string Usercode { get; set; }
        public string Devicecode { get; set; }
        public string Subjectid { get; set; }
        public string Clientid { get; set; }
        public DateTime Creationtime { get; set; }
        public DateTime Expiration { get; set; }
        public string Data { get; set; }
    }
}
