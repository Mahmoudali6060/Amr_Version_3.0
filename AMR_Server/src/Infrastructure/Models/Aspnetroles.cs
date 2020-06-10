using System;
using System.Collections.Generic;

namespace AMR_Server.Infrastructure.Models
{
    public partial class Aspnetroles
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Normalizedname { get; set; }
        public string Concurrencystamp { get; set; }
    }
}
