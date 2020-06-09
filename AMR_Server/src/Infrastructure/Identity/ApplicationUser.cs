
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMR_Server.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {

        [Column("SECURITYSTAMP")]
        public override string SecurityStamp { get; set; }

        [PersonalData]
        [Column("PHONENUMBERCONFIRMED")]
        public override bool PhoneNumberConfirmed { get; set; }

        [ProtectedPersonalData]
        [Column("PHONENUMBER")]
        public override string PhoneNumber { get; set; }

        [Column("PASSWORDHASH")]
        public override string PasswordHash { get; set; }

        [Column("NORMALIZEDUSERNAME")]
        public override string NormalizedUserName { get; set; }

        [Column("NORMALIZEDEMAIL")]
        public override string NormalizedEmail { get; set; }
        [Column("LOCKOUTEND")]
        public override DateTimeOffset? LockoutEnd { get; set; }
        [Column("LOCKOUTENABLED")]
        public override bool LockoutEnabled { get; set; }
        [Column("ID")]
        [PersonalData]
        public override string Id { get; set; }
        [Column("EMAILCONFIRMED")]
        [PersonalData]
        public override bool EmailConfirmed { get; set; }
        [Column("EMAIL")]
        [ProtectedPersonalData]
        public override string Email { get; set; }
        [Column("CONCURRENCYSTAMP")]
        public override string ConcurrencyStamp { get; set; }
        [Column("ACCESSFAILEDCOUNT")]
        public override int AccessFailedCount { get; set; }
        [Column("TWOFACTORENABLED")]
        [PersonalData]
        public override bool TwoFactorEnabled { get; set; }
        [Column("USERNAME")]
        [ProtectedPersonalData]
        public override string UserName { get; set; }
    }
}
