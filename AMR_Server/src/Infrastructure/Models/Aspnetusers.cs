using System;
using System.Collections.Generic;

namespace AMR_Server.Infrastructure.Models
{
    public partial class Aspnetusers
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Normalizedusername { get; set; }
        public string Email { get; set; }
        public string Normalizedemail { get; set; }
        public bool Emailconfirmed { get; set; }
        public string Passwordhash { get; set; }
        public string Securitystamp { get; set; }
        public string Concurrencystamp { get; set; }
        public string Phonenumber { get; set; }
        public bool Phonenumberconfirmed { get; set; }
        public bool Twofactorenabled { get; set; }
        public DateTimeOffset? Lockoutend { get; set; }
        public bool Lockoutenabled { get; set; }
        public int Accessfailedcount { get; set; }
    }
}
