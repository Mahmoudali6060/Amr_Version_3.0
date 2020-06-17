using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class Aspnetuserlogins
    {
        public string Loginprovider { get; set; }
        public string Providerkey { get; set; }
        public string Providerdisplayname { get; set; }
        public string Userid { get; set; }
    }
}
