using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class Aspnetroleclaims
    {
        public int Id { get; set; }
        public string Roleid { get; set; }
        public string Claimtype { get; set; }
        public string Claimvalue { get; set; }
    }
}
