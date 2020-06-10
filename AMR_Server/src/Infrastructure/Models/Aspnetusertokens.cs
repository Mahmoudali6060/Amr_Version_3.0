using System;
using System.Collections.Generic;

namespace AMR_Server.Infrastructure.Models
{
    public partial class Aspnetusertokens
    {
        public string Userid { get; set; }
        public string Loginprovider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
