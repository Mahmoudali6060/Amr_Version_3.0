using AMR_Server.Application.Common.Interfaces;
using AMR_Server.Domain.Common;
using AMR_Server.Infrastructure.Identity;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AMR_Server.Infrastructure.Persistence
{
    public class IdentityDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private IDbContextTransaction _currentTransaction;

        public IdentityDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUserService currentUserService,
            IDateTime dateTime) : base(options, operationalStoreOptions)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = await base.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync().ConfigureAwait(false);

                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.HasDefaultSchema("AMR");

            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("Relational:DefaultSchema", "AMR");

          
            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("ASPNETROLECLAIMS");

                entity.HasIndex(e => e.Id)
                    .HasName("PK_ASPNETROLECLAIMS")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClaimType).HasColumnName("CLAIMTYPE");

                entity.Property(e => e.ClaimValue).HasColumnName("CLAIMVALUE");

                //entity.Property(e => e.RoleId)
                //    .IsRequired()
                //    .HasColumnName("ROLEID")
                //    .HasMaxLength(450);
            });

            modelBuilder.Entity<IdentityRole<string>>(entity =>
            {
                entity.ToTable("ASPNETROLES");

                entity.HasIndex(e => e.Id)
                    .HasName("PK_ASPNETROLES")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ConcurrencyStamp).HasColumnName("CONCURRENCYSTAMP");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(256);

                entity.Property(e => e.NormalizedName)
                    .HasColumnName("NORMALIZEDNAME")
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("ASPNETUSERCLAIMS");

                entity.HasIndex(e => e.Id)
                    .HasName("PK_ASPNETUSERCLAIMS")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClaimType).HasColumnName("CLAIMTYPE");

                entity.Property(e => e.ClaimValue).HasColumnName("CLAIMVALUE");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("USERID")
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.ToTable("ASPNETUSERLOGINS");

                entity.HasIndex(e => new { e.LoginProvider, e.ProviderKey })
                    .HasName("PK_ASPNETUSERLOGINS")
                    .IsUnique();

                entity.Property(e => e.LoginProvider)
                    .HasColumnName("LOGINPROVIDER")
                    .HasMaxLength(128);

                entity.Property(e => e.ProviderKey)
                    .HasColumnName("PROVIDERKEY")
                    .HasMaxLength(128);

                entity.Property(e => e.ProviderDisplayName).HasColumnName("PROVIDERDISPLAYNAME");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("USERID")
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.ToTable("ASPNETUSERROLES");

                entity.HasIndex(e => new { e.UserId, e.RoleId })
                    .HasName("PK_ASPNETUSERROLES")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("USERID");

                entity.Property(e => e.RoleId).HasColumnName("ROLEID");
            });

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("ASPNETUSERS");

                entity.HasIndex(e => e.Id)
                    .HasName("PK_ASPNETUSERS")
                    .IsUnique();

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("USERNAMEINDEX")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccessFailedCount).HasColumnName("ACCESSFAILEDCOUNT");

                entity.Property(e => e.ConcurrencyStamp).HasColumnName("CONCURRENCYSTAMP");

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(256);

                entity.Property(e => e.EmailConfirmed).HasColumnName("EMAILCONFIRMED");

                entity.Property(e => e.LockoutEnabled).HasColumnName("LOCKOUTENABLED");

                entity.Property(e => e.LockoutEnd)
                    .HasColumnName("LOCKOUTEND")
                    .HasColumnType("TIMESTAMP(6) WITH TIME ZONE");

                entity.Property(e => e.NormalizedEmail)
                    .HasColumnName("NORMALIZEDEMAIL")
                    .HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName)
                    .HasColumnName("NORMALIZEDUSERNAME")
                    .HasMaxLength(256);

                entity.Property(e => e.PasswordHash).HasColumnName("PASSWORDHASH");

                entity.Property(e => e.PhoneNumber).HasColumnName("PHONENUMBER");

                entity.Property(e => e.PhoneNumberConfirmed).HasColumnName("PHONENUMBERCONFIRMED");

                entity.Property(e => e.SecurityStamp).HasColumnName("SECURITYSTAMP");

                entity.Property(e => e.TwoFactorEnabled).HasColumnName("TWOFACTORENABLED");

                entity.Property(e => e.UserName)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.Name, e.LoginProvider });

                entity.ToTable("ASPNETUSERTOKENS");

                entity.HasIndex(e => new { e.UserId, e.LoginProvider, e.Name })
                    .HasName("PK_ASPNETUSERTOKENS")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("USERID");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(128);

                entity.Property(e => e.LoginProvider)
                    .HasColumnName("LOGINPROVIDER")
                    .HasMaxLength(128);

                entity.Property(e => e.Value).HasColumnName("VALUE");
            });


        }

    }
}
