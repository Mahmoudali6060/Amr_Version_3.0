using AMR_Server.Application.Common.Interfaces;
using AMR_Server.Domain.Common;
using AMR_Server.Domain.Entities;
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
    public class AmrDbContext : ApiAuthorizationDbContext<ApplicationUser>, IAmrDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private IDbContextTransaction _currentTransaction;

        public AmrDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUserService currentUserService,
            IDateTime dateTime) : base(options, operationalStoreOptions)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }


       
        //public DbSet<User> Users { get; set; }

        public virtual DbSet<AlarmCategory> AlarmCategory { get; set; }
        public virtual DbSet<AlarmCode> AlarmCode { get; set; }
        public virtual DbSet<AlarmLevel> AlarmLevel { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        //public virtual DbSet<Aspnetroles> Aspnetroles { get; set; }
        //public virtual DbSet<Aspnetuserclaims> Aspnetuserclaims { get; set; }
        //public virtual DbSet<Aspnetuserlogins> Aspnetuserlogins { get; set; }
        //public virtual DbSet<Aspnetuserroles> Aspnetuserroles { get; set; }
        //public virtual DbSet<Aspnetusers> Aspnetusers { get; set; }
        //public virtual DbSet<Aspnetusertokens> Aspnetusertokens { get; set; }
        public virtual DbSet<CcbMeters> CcbMeters { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<ConnectingStatus> ConnectingStatus { get; set; }
        public virtual DbSet<DeviceDma> DeviceDma { get; set; }
        public virtual DbSet<DeviceGroup> DeviceGroup { get; set; }
        public virtual DbSet<DeviceQueueAction> DeviceQueueAction { get; set; }
        public virtual DbSet<DeviceCodes> DeviceCodes { get; set; }
        public virtual DbSet<EditableColumn> EditableColumn { get; set; }
        public virtual DbSet<ErrorInfo> ErrorInfo { get; set; }
        public virtual DbSet<ErrorLog> ErrorLog { get; set; }
        public virtual DbSet<Gateway> Gateway { get; set; }
        public virtual DbSet<GatewayConnection> GatewayConnection { get; set; }
        public virtual DbSet<GatewayProfile> GatewayProfile { get; set; }
        public virtual DbSet<GlobalFilter> GlobalFilter { get; set; }
        public virtual DbSet<GlobalFilterPage> GlobalFilterPage { get; set; }
        public virtual DbSet<Meter> Meter { get; set; }
        public virtual DbSet<MeterAlarm> MeterAlarm { get; set; }
        public virtual DbSet<MeterAlarmConfig> MeterAlarmConfig { get; set; }
        public virtual DbSet<MeterAlarmRf> MeterAlarmRf { get; set; }
        public virtual DbSet<MeterBillingConfig> MeterBillingConfig { get; set; }
        public virtual DbSet<MeterComments> MeterComments { get; set; }
        public virtual DbSet<MeterGatewayConfig> MeterGatewayConfig { get; set; }
        public virtual DbSet<MeterModel> MeterModel { get; set; }
        public virtual DbSet<MeterPartion> MeterPartion { get; set; }
        public virtual DbSet<MeterProfile> MeterProfile { get; set; }
        public virtual DbSet<MeterReading> MeterReading { get; set; }
        public virtual DbSet<MeterReads> MeterReads { get; set; }
        public virtual DbSet<MeterStatus> MeterStatus { get; set; }
        public virtual DbSet<MeterType> MeterType { get; set; }
        public virtual DbSet<MeterVendor> MeterVendor { get; set; }
        public virtual DbSet<ObisData> ObisData { get; set; }
        public virtual DbSet<Page> Page { get; set; }
        public virtual DbSet<PagePrivilege> PagePrivilege { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Persistedgrants> Persistedgrants { get; set; }
        public virtual DbSet<Privilege> Privilege { get; set; }
        public virtual DbSet<ProfileType> ProfileType { get; set; }
        public virtual DbSet<QueueAction> QueueAction { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<SimCardList> SimCardList { get; set; }
        public virtual DbSet<TransactionLog> TransactionLog { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }
        public virtual DbSet<UserGroup> UserGroup { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserBasicData> UserBasicData { get; set; }

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

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //    builder.HasDefaultSchema("AMR");
        //    builder.Entity<CITY>()
        //      .HasKey(p => new { p.CITY_ID });

        //    base.OnModelCreating(builder);
        //    builder.Entity<ApplicationUser>().ToTable("ASPNETUSERS");
        //    builder.Entity<IdentityRole>().ToTable("ASPNETROLES");
        //    builder.Entity<IdentityUserRole<string>>().ToTable("ASPNETUSERROLES");
        //    builder.Entity<IdentityUserClaim<string>>().ToTable("ASPNETUSERCLAIMS");
        //    builder.Entity<IdentityUserLogin<string>>().ToTable("ASPNETUSERLOGINS");
        //    builder.Entity<IdentityUserToken<string>>().ToTable("ASPNETUSERTOKENS");
        //    builder.Entity<IdentityRoleClaim<string>>().ToTable("ASPNETROLECLAIMS");



        //}



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.HasDefaultSchema("AMR");


            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<ApplicationUser>().ToTable("ASPNETUSERS");
            //modelBuilder.Entity<IdentityRole>().ToTable("ASPNETROLES");
            //modelBuilder.Entity<IdentityUserRole<string>>().ToTable("ASPNETUSERROLES");
            //modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("ASPNETUSERCLAIMS");
            //modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("ASPNETUSERLOGINS");
            //modelBuilder.Entity<IdentityUserToken<string>>().ToTable("ASPNETUSERTOKENS");
            //modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("ASPNETROLECLAIMS");

            modelBuilder.HasAnnotation("Relational:DefaultSchema", "AMR");

            modelBuilder.Entity<AlarmCategory>(entity =>
            {
                entity.ToTable("ALARM_CATEGORY");

                entity.HasIndex(e => e.AlarmCategoryId)
                    .HasName("ALARM_CATEGORY_PK")
                    .IsUnique();

                entity.Property(e => e.AlarmCategoryId)
                    .HasColumnName("ALARM_CATEGORY_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AlarmCategoryDescription)
                    .HasColumnName("ALARM_CATEGORY_DESCRIPTION")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.AlarmCategoryName)
                    .HasColumnName("ALARM_CATEGORY_NAME")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.Comments)
                    .HasColumnName("COMMENTS")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedUserId).HasColumnName("UPDATED_USER_ID");
            });

            modelBuilder.Entity<AlarmCode>(entity =>
            {
                entity.ToTable("ALARM_CODE");

                entity.HasIndex(e => e.AlarmCodeId)
                    .HasName("ALARM_CODE_PK")
                    .IsUnique();

                entity.Property(e => e.AlarmCodeId)
                    .HasColumnName("ALARM_CODE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.AlarmCategoryId).HasColumnName("ALARM_CATEGORY_ID");

                entity.Property(e => e.AlarmCodeDescription)
                    .HasColumnName("ALARM_CODE_DESCRIPTION")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.AlarmCodeName)
                    .HasColumnName("ALARM_CODE_NAME")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.AlarmLevelId).HasColumnName("ALARM_LEVEL_ID");

                entity.Property(e => e.Comments)
                    .HasColumnName("COMMENTS")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedUserId).HasColumnName("UPDATED_USER_ID");

                entity.HasOne(d => d.AlarmCategory)
                    .WithMany(p => p.AlarmCode)
                    .HasForeignKey(d => d.AlarmCategoryId)
                    .HasConstraintName("ALARM_CODE_FK2");

                entity.HasOne(d => d.AlarmLevel)
                    .WithMany(p => p.AlarmCode)
                    .HasForeignKey(d => d.AlarmLevelId)
                    .HasConstraintName("ALARM_CODE_FK1");
            });

            modelBuilder.Entity<AlarmLevel>(entity =>
            {
                entity.ToTable("ALARM_LEVEL");

                entity.HasIndex(e => e.AlarmLevelId)
                    .HasName("ALARM_LEVEL_PK")
                    .IsUnique();

                entity.Property(e => e.AlarmLevelId)
                    .HasColumnName("ALARM_LEVEL_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AlarmLevelDescription)
                    .HasColumnName("ALARM_LEVEL_DESCRIPTION")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.AlarmLevelName)
                    .HasColumnName("ALARM_LEVEL_NAME")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.Comments)
                    .HasColumnName("COMMENTS")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedUserId)
                    .HasColumnName("UPDATED_USER_ID")
                    .HasColumnType("NUMBER");
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AREA");

                entity.Property(e => e.AreaId).HasColumnName("AREA_ID");

                entity.Property(e => e.AreaName)
                    .HasColumnName("AREA_NAME")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.CityId).HasColumnName("CITY_ID");

                entity.Property(e => e.Comments)
                    .HasColumnName("COMMENTS")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedUserId).HasColumnName("UPDATED_USER_ID");
            });

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

            modelBuilder.Entity<CcbMeters>(entity =>
            {
                entity.ToTable("CCB_METERS");

                entity.HasIndex(e => e.CcbMetersId)
                    .HasName("CCB_METERS_PK")
                    .IsUnique();

                entity.Property(e => e.CcbMetersId)
                    .HasColumnName("CCB_METERS_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.BadgeNo)
                    .HasColumnName("BADGE_NO")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.Hcn)
                    .HasColumnName("HCN")
                    .HasColumnType("VARCHAR2(50)");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("CITY");

                entity.HasIndex(e => e.CityId)
                    .HasName("CITY_PK")
                    .IsUnique();

                entity.Property(e => e.CityId)
                    .HasColumnName("CITY_ID")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.CityName)
                    .HasColumnName("CITY_NAME")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.Comments)
                    .HasColumnName("COMMENTS")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedUserId).HasColumnName("UPDATED_USER_ID");
            });

            modelBuilder.Entity<ConnectingStatus>(entity =>
            {
                entity.HasKey(e => e.ConnectingStatus1)
                    .HasName("CONNECTING_STATUS_PK");

                entity.ToTable("CONNECTING_STATUS");

                entity.HasIndex(e => e.ConnectingStatus1)
                    .HasName("CONNECTING_STATUS_PK")
                    .IsUnique();

                entity.Property(e => e.ConnectingStatus1)
                    .HasColumnName("CONNECTING_STATUS")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.ConnectingStatusName)
                    .HasColumnName("CONNECTING_STATUS_NAME")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            });

            modelBuilder.Entity<DeviceDma>(entity =>
            {
                entity.HasKey(e => e.DmaId)
                    .HasName("METER_DMA_PK");

                entity.ToTable("DEVICE_DMA");

                entity.HasIndex(e => e.DmaId)
                    .HasName("METER_DMA_PK")
                    .IsUnique();

                entity.Property(e => e.DmaId)
                    .HasColumnName("DMA_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.CityId)
                    .HasColumnName("CITY_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Comments)
                    .HasColumnName("COMMENTS")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.DmaName)
                    .HasColumnName("DMA_NAME")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.FullName)
                    .HasColumnName("FULL_NAME")
                    .HasColumnType("VARCHAR2(1000)");

                entity.Property(e => e.FullPath)
                    .HasColumnName("FULL_PATH")
                    .HasColumnType("VARCHAR2(200)");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.ParentId)
                    .HasColumnName("PARENT_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedUserId).HasColumnName("UPDATED_USER_ID");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.DeviceDma)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("DEVICE_DMA_FK1");

                entity.HasOne(d => d.CreatedUser)
                    .WithMany(p => p.DeviceDmaCreatedUser)
                    .HasForeignKey(d => d.CreatedUserId)
                    .HasConstraintName("METER_DMA_FK1");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("METER_DMA_FK3");

                entity.HasOne(d => d.UpdatedUser)
                    .WithMany(p => p.DeviceDmaUpdatedUser)
                    .HasForeignKey(d => d.UpdatedUserId)
                    .HasConstraintName("METER_DMA_FK2");
            });

            modelBuilder.Entity<DeviceGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                    .HasName("DEVICE_GROUP_PK");

                entity.ToTable("DEVICE_GROUP");

                entity.HasIndex(e => e.GroupId)
                    .HasName("DEVICE_GROUP_PK")
                    .IsUnique();

                entity.Property(e => e.GroupId)
                    .HasColumnName("GROUP_ID")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.CcbAreaCode)
                    .HasColumnName("CCB_AREA_CODE")
                    .HasColumnType("VARCHAR2(10)");

                entity.Property(e => e.CcbDivision)
                    .HasColumnName("CCB_DIVISION")
                    .HasColumnType("VARCHAR2(10)");

                entity.Property(e => e.CityId)
                    .HasColumnName("CITY_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Comments)
                    .HasColumnName("COMMENTS")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.FullGroupName)
                    .HasColumnName("FULL_GROUP_NAME")
                    .HasColumnType("VARCHAR2(1000)");

                entity.Property(e => e.FullGroupPath)
                    .HasColumnName("FULL_GROUP_PATH")
                    .HasColumnType("VARCHAR2(200)");

                entity.Property(e => e.GroupName)
                    .HasColumnName("GROUP_NAME")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.IsVirtual).HasColumnName("IS_VIRTUAL");

                entity.Property(e => e.ParentId)
                    .HasColumnName("PARENT_ID")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedUserId).HasColumnName("UPDATED_USER_ID");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.DeviceGroup)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("DEVICE_GROUP_FK1");

                entity.HasOne(d => d.CreatedUser)
                    .WithMany(p => p.DeviceGroupCreatedUser)
                    .HasForeignKey(d => d.CreatedUserId)
                    .HasConstraintName("METER_GROUP_FK1");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("METER_GROUP_FK3");

                entity.HasOne(d => d.UpdatedUser)
                    .WithMany(p => p.DeviceGroupUpdatedUser)
                    .HasForeignKey(d => d.UpdatedUserId)
                    .HasConstraintName("METER_GROUP_FK2");
            });

            modelBuilder.Entity<DeviceQueueAction>(entity =>
            {
                entity.ToTable("DEVICE_QUEUE_ACTION");

                entity.HasIndex(e => e.DeviceQueueActionId)
                    .HasName("DEVICE_QUEUE_ACTION_PK")
                    .IsUnique();

                entity.Property(e => e.DeviceQueueActionId)
                    .HasColumnName("DEVICE_QUEUE_ACTION_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.ExecuteStatus)
                    .HasColumnName("EXECUTE_STATUS")
                    .HasColumnType("NUMBER(2)");

                entity.Property(e => e.GatewayId)
                    .HasColumnName("GATEWAY_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.IsManual).HasColumnName("IS_MANUAL");

                entity.Property(e => e.MeterId)
                    .HasColumnName("METER_ID")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.QueueActionId)
                    .HasColumnName("QUEUE_ACTION_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.RequesterId)
                    .HasColumnName("REQUESTER_ID")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.Source)
                    .HasColumnName("SOURCE")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedUserId).HasColumnName("UPDATED_USER_ID");

                entity.HasOne(d => d.CreatedUser)
                    .WithMany(p => p.DeviceQueueActionCreatedUser)
                    .HasForeignKey(d => d.CreatedUserId)
                    .HasConstraintName("METER_QUEUE_ACTION_FK2");

                entity.HasOne(d => d.Gateway)
                    .WithMany(p => p.DeviceQueueAction)
                    .HasForeignKey(d => d.GatewayId)
                    .HasConstraintName("DEVICE_QUEUE_ACTION_FK1");

                entity.HasOne(d => d.Meter)
                    .WithMany(p => p.DeviceQueueAction)
                    .HasForeignKey(d => d.MeterId)
                    .HasConstraintName("DEVICE_QUEUE_ACTION_FK2");

                entity.HasOne(d => d.QueueAction)
                    .WithMany(p => p.DeviceQueueAction)
                    .HasForeignKey(d => d.QueueActionId)
                    .HasConstraintName("METER_QUEUE_ACTION_FK1");

                entity.HasOne(d => d.UpdatedUser)
                    .WithMany(p => p.DeviceQueueActionUpdatedUser)
                    .HasForeignKey(d => d.UpdatedUserId)
                    .HasConstraintName("METER_QUEUE_ACTION_FK3");
            });

            modelBuilder.Entity<DeviceCodes>(entity =>
            {
                entity.HasKey(e => e.Usercode);

                entity.ToTable("DEVICECODES");

                entity.HasIndex(e => e.Usercode)
                    .HasName("PK_DEVICECODES")
                    .IsUnique();

                entity.Property(e => e.Usercode)
                    .HasColumnName("USERCODE")
                    .HasMaxLength(200);

                entity.Property(e => e.Clientid)
                    .IsRequired()
                    .HasColumnName("CLIENTID")
                    .HasMaxLength(200);

                entity.Property(e => e.Creationtime)
                    .HasColumnName("CREATIONTIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasColumnName("DATA");

                entity.Property(e => e.Devicecode)
                    .IsRequired()
                    .HasColumnName("DEVICECODE")
                    .HasMaxLength(200);

                entity.Property(e => e.Expiration)
                    .HasColumnName("EXPIRATION")
                    .HasColumnType("DATE");

                entity.Property(e => e.Subjectid)
                    .HasColumnName("SUBJECTID")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<EditableColumn>(entity =>
            {
                entity.ToTable("EDITABLE_COLUMN");

                entity.HasIndex(e => e.EditableColumnId)
                    .HasName("EDITABLE_COLUMN_PK")
                    .IsUnique();

                entity.Property(e => e.EditableColumnId)
                    .HasColumnName("EDITABLE_COLUMN_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.ColumName)
                    .HasColumnName("COLUM_NAME")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.PageId)
                    .HasColumnName("PAGE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.TableName)
                    .HasColumnName("TABLE_NAME")
                    .HasColumnType("VARCHAR2(50)");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.EditableColumn)
                    .HasForeignKey(d => d.PageId)
                    .HasConstraintName("EDITABLE_COLUMN_FK1");
            });

            modelBuilder.Entity<ErrorInfo>(entity =>
            {
                entity.ToTable("ERROR_INFO");

                entity.HasIndex(e => e.ErrorInfoId)
                    .HasName("ERROR_INFO_PK")
                    .IsUnique();

                entity.Property(e => e.ErrorInfoId)
                    .HasColumnName("ERROR_INFO_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.ErrorCode)
                    .HasColumnName("ERROR_CODE")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.ErrorMessage)
                    .HasColumnName("ERROR_MESSAGE")
                    .HasColumnType("VARCHAR2(500)");

                entity.Property(e => e.ObjectName)
                    .HasColumnName("OBJECT_NAME")
                    .HasColumnType("VARCHAR2(150)");
            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.ToTable("ERROR_LOG");

                entity.HasIndex(e => e.ErrorLogId)
                    .HasName("ERROR_LOG_PK")
                    .IsUnique();

                entity.Property(e => e.ErrorLogId)
                    .HasColumnName("ERROR_LOG_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.ErrorDate)
                    .HasColumnName("ERROR_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.ErrorInfoId)
                    .HasColumnName("ERROR_INFO_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.ExceptionMessage)
                    .HasColumnName("EXCEPTION_MESSAGE")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.PageId)
                    .HasColumnName("PAGE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.PrivilegeId)
                    .HasColumnName("PRIVILEGE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.ErrorInfo)
                    .WithMany(p => p.ErrorLog)
                    .HasForeignKey(d => d.ErrorInfoId)
                    .HasConstraintName("ERROR_LOG_FK4");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.ErrorLog)
                    .HasForeignKey(d => d.PageId)
                    .HasConstraintName("ERROR_LOG_FK1");

                entity.HasOne(d => d.Privilege)
                    .WithMany(p => p.ErrorLog)
                    .HasForeignKey(d => d.PrivilegeId)
                    .HasConstraintName("ERROR_LOG_FK2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ErrorLog)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("ERROR_LOG_FK3");
            });

            modelBuilder.Entity<Gateway>(entity =>
            {
                entity.ToTable("GATEWAY");

                entity.HasIndex(e => e.GatewayId)
                    .HasName("GATEWAY_PK")
                    .IsUnique();

                entity.Property(e => e.GatewayId)
                    .HasColumnName("GATEWAY_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.ApprovedDate)
                    .HasColumnName("APPROVED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.ApprovedUserId).HasColumnName("APPROVED_USER_ID");

                entity.Property(e => e.Comments)
                    .HasColumnName("COMMENTS")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.ContractId)
                    .HasColumnName("CONTRACT_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.CoordLat)
                    .HasColumnName("COORD_LAT")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.CoordLon)
                    .HasColumnName("COORD_LON")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.FloorNo)
                    .HasColumnName("FLOOR_NO")
                    .HasColumnType("VARCHAR2(10)");

                entity.Property(e => e.FtpDirectory)
                    .HasColumnName("FTP_DIRECTORY")
                    .HasColumnType("VARCHAR2(200)");

                entity.Property(e => e.FtpGatewayFolderName)
                    .HasColumnName("FTP_GATEWAY_FOLDER_NAME")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.FtpGatewayNameSuffix)
                    .HasColumnName("FTP_GATEWAY_NAME_SUFFIX")
                    .HasColumnType("VARCHAR2(5)");

                entity.Property(e => e.FtpHostName)
                    .HasColumnName("FTP_HOST_NAME")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.FtpIsFolderCreated).HasColumnName("FTP_IS_FOLDER_CREATED");

                entity.Property(e => e.FtpPassword)
                    .HasColumnName("FTP_PASSWORD")
                    .HasColumnType("VARCHAR2(200)");

                entity.Property(e => e.FtpPort)
                    .HasColumnName("FTP_PORT")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.FtpUsername)
                    .HasColumnName("FTP_USERNAME")
                    .HasColumnType("VARCHAR2(30)");

                entity.Property(e => e.GatewayCode)
                    .HasColumnName("GATEWAY_CODE")
                    .HasColumnType("VARCHAR2(30)");

                entity.Property(e => e.GroupId)
                    .HasColumnName("GROUP_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.HouseNo)
                    .HasColumnName("HOUSE_NO")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.InventoryStatus)
                    .HasColumnName("INVENTORY_STATUS")
                    .HasColumnType("NUMBER(2)");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.IsApproved).HasColumnName("IS_APPROVED");

                entity.Property(e => e.IsFixed).HasColumnName("IS_FIXED");

                entity.Property(e => e.LastDatasetDate)
                    .HasColumnName("LAST_DATASET_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.LastOnlineDate)
                    .HasColumnName("LAST_ONLINE_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.LocationName)
                    .HasColumnName("LOCATION_NAME")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.LocationTypeId)
                    .HasColumnName("LOCATION_TYPE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.ModelId).HasColumnName("MODEL_ID");

                entity.Property(e => e.OnlineStatus).HasColumnName("ONLINE_STATUS");

                entity.Property(e => e.PostalCode)
                    .HasColumnName("POSTAL_CODE")
                    .HasColumnType("VARCHAR2(10)");

                entity.Property(e => e.SimCardNo)
                    .HasColumnName("SIM_CARD_NO")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.StreetName)
                    .HasColumnName("STREET_NAME")
                    .HasColumnType("VARCHAR2(200)");

                entity.Property(e => e.UpdateUserId).HasColumnName("UPDATE_USER_ID");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.VendorId).HasColumnName("VENDOR_ID");

                entity.HasOne(d => d.ApprovedUser)
                    .WithMany(p => p.GatewayApprovedUser)
                    .HasForeignKey(d => d.ApprovedUserId)
                    .HasConstraintName("GATEWAY_FK7");

                entity.HasOne(d => d.CreatedUser)
                    .WithMany(p => p.GatewayCreatedUser)
                    .HasForeignKey(d => d.CreatedUserId)
                    .HasConstraintName("GATEWAY_FK4");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Gateway)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("GATEWAY_FK1");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Gateway)
                    .HasForeignKey(d => d.ModelId)
                    .HasConstraintName("GATEWAY_FK3");

                entity.HasOne(d => d.SimCardNoNavigation)
                    .WithMany(p => p.Gateway)
                    .HasForeignKey(d => d.SimCardNo)
                    .HasConstraintName("GATEWAY_FK6");

                entity.HasOne(d => d.UpdateUser)
                    .WithMany(p => p.GatewayUpdateUser)
                    .HasForeignKey(d => d.UpdateUserId)
                    .HasConstraintName("GATEWAY_FK5");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Gateway)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("GATEWAY_FK2");
            });

            modelBuilder.Entity<GatewayConnection>(entity =>
            {
                entity.HasKey(e => e.ConnectionId)
                    .HasName("GATEWAY_CONNECTION_PK");

                entity.ToTable("GATEWAY_CONNECTION");

                entity.HasIndex(e => e.ConnectionId)
                    .HasName("GATEWAY_CONNECTION_PK")
                    .IsUnique();

                entity.Property(e => e.ConnectionId)
                    .HasColumnName("CONNECTION_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.ConnectionDatetime)
                    .HasColumnName("CONNECTION_DATETIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.FileName)
                    .HasColumnName("FILE_NAME")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.FileSize)
                    .HasColumnName("FILE_SIZE")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.GatewayId)
                    .HasColumnName("GATEWAY_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.ReadingsCount)
                    .HasColumnName("READINGS_COUNT")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedUserId).HasColumnName("UPDATED_USER_ID");

                entity.HasOne(d => d.CreatedUser)
                    .WithMany(p => p.GatewayConnectionCreatedUser)
                    .HasForeignKey(d => d.CreatedUserId)
                    .HasConstraintName("GATEWAY_CONNECTION_FK2");

                entity.HasOne(d => d.Gateway)
                    .WithMany(p => p.GatewayConnection)
                    .HasForeignKey(d => d.GatewayId)
                    .HasConstraintName("GATEWAY_CONNECTION_FK1");

                entity.HasOne(d => d.UpdatedUser)
                    .WithMany(p => p.GatewayConnectionUpdatedUser)
                    .HasForeignKey(d => d.UpdatedUserId)
                    .HasConstraintName("GATEWAY_CONNECTION_FK3");
            });

            modelBuilder.Entity<GatewayProfile>(entity =>
            {
                entity.HasKey(e => e.GatewayProfile1)
                    .HasName("GATEWAY_PROFILE_PK");

                entity.ToTable("GATEWAY_PROFILE");

                entity.HasIndex(e => e.GatewayProfile1)
                    .HasName("GATEWAY_PROFILE_PK")
                    .IsUnique();

                entity.Property(e => e.GatewayProfile1)
                    .HasColumnName("GATEWAY_PROFILE")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.GatewayId)
                    .HasColumnName("GATEWAY_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.ProfileTypeId)
                    .HasColumnName("PROFILE_TYPE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.TransactionDate)
                    .HasColumnName("TRANSACTION_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.Value)
                    .HasColumnName("VALUE")
                    .HasColumnType("VARCHAR2(2000)");

                entity.HasOne(d => d.ProfileType)
                    .WithMany(p => p.GatewayProfile)
                    .HasForeignKey(d => d.ProfileTypeId)
                    .HasConstraintName("GATEWAY_PROFILE_FK1");
            });

            modelBuilder.Entity<GlobalFilter>(entity =>
            {
                entity.ToTable("GLOBAL_FILTER");

                entity.HasIndex(e => e.GlobalFilterId)
                    .HasName("GLOBAL_FILTER_PK")
                    .IsUnique();

                entity.Property(e => e.GlobalFilterId)
                    .HasColumnName("GLOBAL_FILTER_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.FilterDescription)
                    .HasColumnName("FILTER_DESCRIPTION")
                    .HasColumnType("VARCHAR2(200)");

                entity.Property(e => e.FilterName)
                    .HasColumnName("FILTER_NAME")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.LookupTableQuery)
                    .HasColumnName("LOOKUP_TABLE_QUERY")
                    .HasColumnType("VARCHAR2(20)");
            });

            modelBuilder.Entity<GlobalFilterPage>(entity =>
            {
                entity.ToTable("GLOBAL_FILTER_PAGE");

                entity.HasIndex(e => e.GlobalFilterPageId)
                    .HasName("GLOBAL_FILTER_PAGE_PK")
                    .IsUnique();

                entity.Property(e => e.GlobalFilterPageId)
                    .HasColumnName("GLOBAL_FILTER_PAGE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.FilterQuery)
                    .HasColumnName("FILTER_QUERY")
                    .HasColumnType("VARCHAR2(1000)");

                entity.Property(e => e.GlobalFilterId)
                    .HasColumnName("GLOBAL_FILTER_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.PageId)
                    .HasColumnName("PAGE_ID")
                    .HasColumnType("NUMBER");

                entity.HasOne(d => d.GlobalFilter)
                    .WithMany(p => p.GlobalFilterPage)
                    .HasForeignKey(d => d.GlobalFilterId)
                    .HasConstraintName("GLOBAL_FILTER_PAGE_FK1");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.GlobalFilterPage)
                    .HasForeignKey(d => d.PageId)
                    .HasConstraintName("GLOBAL_FILTER_PAGE_FK2");
            });

            modelBuilder.Entity<Meter>(entity =>
            {
                entity.ToTable("METER");

                entity.HasIndex(e => e.MeterId)
                    .HasName("DEVICE_DATA_PK")
                    .IsUnique();

                entity.Property(e => e.MeterId)
                    .HasColumnName("METER_ID")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.AreaId).HasColumnName("AREA_ID");

                entity.Property(e => e.BadgeNo)
                    .IsRequired()
                    .HasColumnName("BADGE_NO")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.Column2)
                    .HasColumnName("COLUMN2")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.Comments)
                    .HasColumnName("COMMENTS")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.ConnectingStatus)
                    .HasColumnName("CONNECTING_STATUS")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.CoordLat)
                    .HasColumnName("COORD_LAT")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.CoordLon)
                    .HasColumnName("COORD__LON")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.CoordinatesType)
                    .HasColumnName("COORDINATES_TYPE")
                    .HasColumnType("VARCHAR2(10)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.DeleteStaus).HasColumnName("DELETE_STAUS");

                entity.Property(e => e.DmaId)
                    .HasColumnName("DMA_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.FloorNo)
                    .HasColumnName("FLOOR_NO")
                    .HasColumnType("VARCHAR2(1)");

                entity.Property(e => e.GroupId)
                    .HasColumnName("GROUP_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Hcn)
                    .IsRequired()
                    .HasColumnName("HCN")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.InStore).HasColumnName("IN_STORE");

                entity.Property(e => e.InstalationDate)
                    .HasColumnName("INSTALATION_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.InventoryStaus)
                    .HasColumnName("INVENTORY_STAUS")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.IsBulk).HasColumnName("IS_BULK");

                entity.Property(e => e.IsDriveby).HasColumnName("IS_DRIVEBY");

                entity.Property(e => e.IsInControlRoom).HasColumnName("IS_IN_CONTROL_ROOM");

                entity.Property(e => e.IsSmartInCcb).HasColumnName("IS_SMART_IN_CCB");

                entity.Property(e => e.LastDatasetDate)
                    .HasColumnName("LAST_DATASET_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.LastOnlineDate)
                    .HasColumnName("LAST_ONLINE_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.MeterStatusId)
                    .HasColumnName("METER_STATUS_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.MeterTypeId).HasColumnName("METER_TYPE_ID");

                entity.Property(e => e.ModelId).HasColumnName("MODEL_ID");

                entity.Property(e => e.ModuleId).HasColumnName("MODULE_ID");

                entity.Property(e => e.ModuleModelId).HasColumnName("MODULE_MODEL_ID");

                entity.Property(e => e.ModulePort).HasColumnName("MODULE_PORT");

                entity.Property(e => e.ModuleVendorId).HasColumnName("MODULE_VENDOR_ID");

                entity.Property(e => e.OnlineStatusLastChangeDate)
                    .HasColumnName("ONLINE_STATUS_LAST_CHANGE_DATE")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.PostalCode)
                    .HasColumnName("POSTAL_CODE")
                    .HasColumnType("VARCHAR2(10)");

                entity.Property(e => e.SerialNo)
                    .HasColumnName("SERIAL_NO")
                    .HasColumnType("VARCHAR2(25)");

                entity.Property(e => e.SimCardNo)
                    .HasColumnName("SIM_CARD_NO")
                    .HasColumnType("VARCHAR2(30)");

                entity.Property(e => e.StreetName)
                    .HasColumnName("STREET_NAME")
                    .HasColumnType("VARCHAR2(200)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedUserId).HasColumnName("UPDATED_USER_ID");

                entity.Property(e => e.VendorId).HasColumnName("VENDOR_ID");

                entity.HasOne(d => d.CreatedUser)
                    .WithMany(p => p.MeterCreatedUser)
                    .HasForeignKey(d => d.CreatedUserId)
                    .HasConstraintName("METER_FK8");

                entity.HasOne(d => d.Dma)
                    .WithMany(p => p.Meter)
                    .HasForeignKey(d => d.DmaId)
                    .HasConstraintName("METER_FK6");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Meter)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("METER_FK1");

                entity.HasOne(d => d.MeterStatus)
                    .WithMany(p => p.Meter)
                    .HasForeignKey(d => d.MeterStatusId)
                    .HasConstraintName("METER_FK5");

                entity.HasOne(d => d.MeterType)
                    .WithMany(p => p.Meter)
                    .HasForeignKey(d => d.MeterTypeId)
                    .HasConstraintName("METER_FK3");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Meter)
                    .HasForeignKey(d => d.ModelId)
                    .HasConstraintName("METER_FK4");

                entity.HasOne(d => d.SimCardNoNavigation)
                    .WithMany(p => p.Meter)
                    .HasForeignKey(d => d.SimCardNo)
                    .HasConstraintName("METER_FK7");

                entity.HasOne(d => d.UpdatedUser)
                    .WithMany(p => p.MeterUpdatedUser)
                    .HasForeignKey(d => d.UpdatedUserId)
                    .HasConstraintName("METER_FK9");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Meter)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("METER_FK2");
            });

            modelBuilder.Entity<MeterAlarm>(entity =>
            {
                entity.ToTable("METER_ALARM");

                entity.HasIndex(e => e.MeterAlarmId)
                    .HasName("METER_ALARM_PK")
                    .IsUnique();

                entity.Property(e => e.MeterAlarmId)
                    .HasColumnName("METER_ALARM_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.AlarmCodeId)
                    .HasColumnName("ALARM_CODE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.AlarmInformed).HasColumnName("ALARM_INFORMED");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.MeterAlarmDatetime)
                    .HasColumnName("METER_ALARM_DATETIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.MeterGroupId)
                    .HasColumnName("METER_GROUP_ID")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.MeterId)
                    .HasColumnName("METER_ID")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.MeterTypeId).HasColumnName("METER_TYPE_ID");

                entity.Property(e => e.ModuleId)
                    .HasColumnName("MODULE_ID")
                    .HasColumnType("VARCHAR2(25)");

                entity.Property(e => e.ModulePort).HasColumnName("MODULE_PORT");

                entity.Property(e => e.TicketId)
                    .HasColumnName("TICKET_ID")
                    .HasColumnType("VARCHAR2(30)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedUserId).HasColumnName("UPDATED_USER_ID");

                entity.HasOne(d => d.AlarmCode)
                    .WithMany(p => p.MeterAlarm)
                    .HasForeignKey(d => d.AlarmCodeId)
                    .HasConstraintName("METER_ALARM_FK1");

                entity.HasOne(d => d.CreatedUser)
                    .WithMany(p => p.MeterAlarmCreatedUser)
                    .HasForeignKey(d => d.CreatedUserId)
                    .HasConstraintName("METER_ALARM_FK5");

                entity.HasOne(d => d.MeterGroup)
                    .WithMany(p => p.MeterAlarm)
                    .HasForeignKey(d => d.MeterGroupId)
                    .HasConstraintName("METER_ALARM_FK4");

                entity.HasOne(d => d.Meter)
                    .WithMany(p => p.MeterAlarm)
                    .HasForeignKey(d => d.MeterId)
                    .HasConstraintName("METER_ALARM_FK2");

                entity.HasOne(d => d.MeterType)
                    .WithMany(p => p.MeterAlarm)
                    .HasForeignKey(d => d.MeterTypeId)
                    .HasConstraintName("METER_ALARM_FK3");

                entity.HasOne(d => d.UpdatedUser)
                    .WithMany(p => p.MeterAlarmUpdatedUser)
                    .HasForeignKey(d => d.UpdatedUserId)
                    .HasConstraintName("METER_ALARM_FK6");
            });

            modelBuilder.Entity<MeterAlarmConfig>(entity =>
            {
                entity.ToTable("METER_ALARM_CONFIG");

                entity.HasIndex(e => e.MeterAlarmConfigId)
                    .HasName("DEVICE_ALARM_PK")
                    .IsUnique();

                entity.Property(e => e.MeterAlarmConfigId)
                    .HasColumnName("METER_ALARM_CONFIG_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BackFlow).HasColumnName("BACK_FLOW");

                entity.Property(e => e.BrokenPipe).HasColumnName("BROKEN_PIPE");

                entity.Property(e => e.EmptyPipe)
                    .HasColumnName("EMPTY_PIPE")
                    .HasColumnType("VARCHAR2(1)");

                entity.Property(e => e.Leak).HasColumnName("LEAK");

                entity.Property(e => e.LowBattery).HasColumnName("LOW_BATTERY");

                entity.Property(e => e.MagneticTamper).HasColumnName("MAGNETIC_TAMPER");

                entity.Property(e => e.MeterError).HasColumnName("METER_ERROR");

                entity.Property(e => e.MeterId)
                    .IsRequired()
                    .HasColumnName("METER_ID")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.SpecificError).HasColumnName("SPECIFIC_ERROR");

                entity.HasOne(d => d.Meter)
                    .WithMany(p => p.MeterAlarmConfig)
                    .HasForeignKey(d => d.MeterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("METER_ALARM_CONFIG_FK1");
            });

            modelBuilder.Entity<MeterAlarmRf>(entity =>
            {
                entity.HasKey(e => e.MeterAlarmId)
                    .HasName("METER_ALARM_RF_PK");

                entity.ToTable("METER_ALARM_RF");

                entity.HasIndex(e => e.MeterAlarmId)
                    .HasName("METER_ALARM_RF_PK")
                    .IsUnique();

                entity.Property(e => e.MeterAlarmId)
                    .HasColumnName("METER_ALARM_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.AlarmCodeId)
                    .HasColumnName("ALARM_CODE_ID")
                    .HasColumnType("VARCHAR2(11)");

                entity.Property(e => e.AlarmId)
                    .HasColumnName("ALARM_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.MeterAlarmDatetime)
                    .HasColumnName("METER_ALARM_DATETIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.MeterGroupId)
                    .HasColumnName("METER_GROUP_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.MeterId)
                    .HasColumnName("METER_ID")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.MeterTypeId).HasColumnName("METER_TYPE_ID");

                entity.Property(e => e.ModuleId)
                    .HasColumnName("MODULE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.ModulePort).HasColumnName("MODULE_PORT");

                entity.Property(e => e.TicketId)
                    .HasColumnName("TICKET_ID")
                    .HasColumnType("VARCHAR2(30)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedUserId).HasColumnName("UPDATED_USER_ID");

                entity.HasOne(d => d.Alarm)
                    .WithMany(p => p.MeterAlarmRf)
                    .HasForeignKey(d => d.AlarmId)
                    .HasConstraintName("METER_ALARM_RF_FK5");

                entity.HasOne(d => d.CreatedUser)
                    .WithMany(p => p.MeterAlarmRfCreatedUser)
                    .HasForeignKey(d => d.CreatedUserId)
                    .HasConstraintName("METER_ALARM_RF_FK3");

                entity.HasOne(d => d.Meter)
                    .WithMany(p => p.MeterAlarmRf)
                    .HasForeignKey(d => d.MeterId)
                    .HasConstraintName("METER_ALARM_RF_FK1");

                entity.HasOne(d => d.MeterType)
                    .WithMany(p => p.MeterAlarmRf)
                    .HasForeignKey(d => d.MeterTypeId)
                    .HasConstraintName("METER_ALARM_RF_FK2");

                entity.HasOne(d => d.UpdatedUser)
                    .WithMany(p => p.MeterAlarmRfUpdatedUser)
                    .HasForeignKey(d => d.UpdatedUserId)
                    .HasConstraintName("METER_ALARM_RF_FK4");
            });

            modelBuilder.Entity<MeterBillingConfig>(entity =>
            {
                entity.ToTable("METER_BILLING_CONFIG");

                entity.HasIndex(e => e.MeterBillingConfigId)
                    .HasName("METER_BILLING_CONFIG_PK")
                    .IsUnique();

                entity.Property(e => e.MeterBillingConfigId)
                    .HasColumnName("METER_BILLING_CONFIG_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.BillingCycleDate)
                    .HasColumnName("BILLING_CYCLE_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.BillingCycleId)
                    .HasColumnName("BILLING_CYCLE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.MeterId)
                    .IsRequired()
                    .HasColumnName("METER_ID")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.NextBillingCycleEndDate)
                    .HasColumnName("NEXT_BILLING_CYCLE_END_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.NextBillingCycleStartDate)
                    .HasColumnName("NEXT_BILLING_CYCLE_START_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.PreviousReadingDate)
                    .HasColumnName("PREVIOUS_READING_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.PreviousReadingValue)
                    .HasColumnName("PREVIOUS_READING_VALUE")
                    .HasColumnType("FLOAT");

                entity.HasOne(d => d.Meter)
                    .WithMany(p => p.MeterBillingConfig)
                    .HasForeignKey(d => d.MeterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("METER_BILLING_CONFIG_FK1");
            });

            modelBuilder.Entity<MeterComments>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("DEVICE_COMMENTS_PK");

                entity.ToTable("METER_COMMENTS");

                entity.HasIndex(e => e.CommentId)
                    .HasName("DEVICE_COMMENTS_PK")
                    .IsUnique();

                entity.Property(e => e.CommentId)
                    .HasColumnName("COMMENT_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.CommentDate)
                    .HasColumnName("COMMENT_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CommentStatus).HasColumnName("COMMENT_STATUS");

                entity.Property(e => e.CommentText)
                    .HasColumnName("COMMENT_TEXT")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.CommentTypeId)
                    .HasColumnName("COMMENT_TYPE_ID")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.Hcn)
                    .HasColumnName("HCN")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.MeterId)
                    .HasColumnName("METER_ID")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.ReviewText)
                    .HasColumnName("REVIEW_TEXT")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.ReviewedDate)
                    .HasColumnName("REVIEWED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.ReviewedUserId).HasColumnName("REVIEWED_USER_ID");

                entity.Property(e => e.UpdateUserId).HasColumnName("UPDATE_USER_ID");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("DATE");

                entity.HasOne(d => d.CreatedUser)
                    .WithMany(p => p.MeterCommentsCreatedUser)
                    .HasForeignKey(d => d.CreatedUserId)
                    .HasConstraintName("METER_COMMENTS_FK2");

                entity.HasOne(d => d.Meter)
                    .WithMany(p => p.MeterComments)
                    .HasForeignKey(d => d.MeterId)
                    .HasConstraintName("METER_COMMENTS_FK1");

                entity.HasOne(d => d.ReviewedUser)
                    .WithMany(p => p.MeterCommentsReviewedUser)
                    .HasForeignKey(d => d.ReviewedUserId)
                    .HasConstraintName("METER_COMMENTS_FK4");

                entity.HasOne(d => d.UpdateUser)
                    .WithMany(p => p.MeterCommentsUpdateUser)
                    .HasForeignKey(d => d.UpdateUserId)
                    .HasConstraintName("METER_COMMENTS_FK3");
            });

            modelBuilder.Entity<MeterGatewayConfig>(entity =>
            {
                entity.ToTable("METER_GATEWAY_CONFIG");

                entity.HasIndex(e => e.MeterGatewayConfigId)
                    .HasName("DEVICE_GATEWAY_CONFIG_PK")
                    .IsUnique();

                entity.Property(e => e.MeterGatewayConfigId)
                    .HasColumnName("METER_GATEWAY_CONFIG_ID")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.DrivebyAddrCoordDate)
                    .HasColumnName("DRIVEBY_ADDR_COORD_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.DrivebyAddrCoordLat)
                    .HasColumnName("DRIVEBY_ADDR_COORD_LAT")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.DrivebyAddrCoordLon)
                    .HasColumnName("DRIVEBY_ADDR_COORD_LON")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.LastDrivebyGatewayId)
                    .HasColumnName("LAST_DRIVEBY_GATEWAY_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.LastDrivebyGatewayReadDate)
                    .HasColumnName("LAST_DRIVEBY_GATEWAY_READ_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.LastDrivebyGatewayReadValue)
                    .HasColumnName("LAST_DRIVEBY_GATEWAY_READ_VALUE")
                    .HasColumnType("FLOAT");

                entity.Property(e => e.LastFixedGatewayId)
                    .HasColumnName("LAST_FIXED_GATEWAY_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.LastFixedGattewayReadDate)
                    .HasColumnName("LAST_FIXED_GATTEWAY_READ_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.LastFixedGattewayReadValue)
                    .HasColumnName("LAST_FIXED_GATTEWAY_READ_VALUE")
                    .HasColumnType("FLOAT");

                entity.Property(e => e.LastGatewayId)
                    .HasColumnName("LAST_GATEWAY_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.MeterGatewayId).HasColumnName("METER_GATEWAY_ID");

                entity.HasOne(d => d.LastDrivebyGateway)
                    .WithMany(p => p.MeterGatewayConfigLastDrivebyGateway)
                    .HasForeignKey(d => d.LastDrivebyGatewayId)
                    .HasConstraintName("METER_GATEWAY_CONFIG_FK2");

                entity.HasOne(d => d.LastFixedGateway)
                    .WithMany(p => p.MeterGatewayConfigLastFixedGateway)
                    .HasForeignKey(d => d.LastFixedGatewayId)
                    .HasConstraintName("METER_GATEWAY_CONFIG_FK3");

                entity.HasOne(d => d.LastGateway)
                    .WithMany(p => p.MeterGatewayConfigLastGateway)
                    .HasForeignKey(d => d.LastGatewayId)
                    .HasConstraintName("METER_GATEWAY_CONFIG_FK1");

                entity.HasOne(d => d.MeterGatewayConfigNavigation)
                    .WithOne(p => p.MeterGatewayConfigMeterGatewayConfigNavigation)
                    .HasForeignKey<MeterGatewayConfig>(d => d.MeterGatewayConfigId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("METER_GATEWAY_CONFIG_FK4");
            });

            modelBuilder.Entity<MeterModel>(entity =>
            {
                entity.HasKey(e => e.ModelId)
                    .HasName("DEVICE_MODEL_PK");

                entity.ToTable("METER_MODEL");

                entity.HasIndex(e => e.ModelId)
                    .HasName("DEVICE_MODEL_PK")
                    .IsUnique();

                entity.Property(e => e.ModelId)
                    .HasColumnName("MODEL_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Comments)
                    .HasColumnName("COMMENTS")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.ConnectionType)
                    .HasColumnName("CONNECTION_TYPE")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.DeviceTypeId).HasColumnName("DEVICE_TYPE_ID");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.MaxAcceptedReading)
                    .HasColumnName("MAX_ACCEPTED_READING")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.ModelName)
                    .HasColumnName("MODEL_NAME")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedUserId).HasColumnName("UPDATED_USER_ID");

                entity.Property(e => e.VendorId).HasColumnName("VENDOR_ID");

                entity.HasOne(d => d.CreatedUser)
                    .WithMany(p => p.MeterModelCreatedUser)
                    .HasForeignKey(d => d.CreatedUserId)
                    .HasConstraintName("METER_MODEL_FK3");

                entity.HasOne(d => d.DeviceType)
                    .WithMany(p => p.MeterModel)
                    .HasForeignKey(d => d.DeviceTypeId)
                    .HasConstraintName("METER_MODEL_FK2");

                entity.HasOne(d => d.UpdatedUser)
                    .WithMany(p => p.MeterModelUpdatedUser)
                    .HasForeignKey(d => d.UpdatedUserId)
                    .HasConstraintName("METER_MODEL_FK4");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.MeterModel)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("METER_MODEL_FK1");
            });

            modelBuilder.Entity<MeterPartion>(entity =>
            {
                entity.ToTable("METER_PARTION");

                entity.HasIndex(e => e.MeterPartionId)
                    .HasName("METER_PARTION_PK")
                    .IsUnique();

                entity.Property(e => e.MeterPartionId)
                    .HasColumnName("METER_PARTION_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.MaxMeterId)
                    .HasColumnName("MAX_METER_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.MinMeterId)
                    .HasColumnName("MIN_METER_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.PartitionId)
                    .HasColumnName("PARTITION_ID")
                    .HasColumnType("VARCHAR2(100)");
            });

            modelBuilder.Entity<MeterProfile>(entity =>
            {
                entity.ToTable("METER_PROFILE");

                entity.HasIndex(e => e.MeterProfileId)
                    .HasName("METER_PROFILE_PK")
                    .IsUnique();

                entity.Property(e => e.MeterProfileId)
                    .HasColumnName("METER_PROFILE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.MeterId)
                    .HasColumnName("METER_ID")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.ProfileTypeId)
                    .HasColumnName("PROFILE_TYPE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.TransactionDate)
                    .HasColumnName("TRANSACTION_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.Value)
                    .HasColumnName("VALUE")
                    .HasColumnType("VARCHAR2(1000)");

                entity.HasOne(d => d.Meter)
                    .WithMany(p => p.MeterProfile)
                    .HasForeignKey(d => d.MeterId)
                    .HasConstraintName("METER_PROFILE_FK1");

                entity.HasOne(d => d.ProfileType)
                    .WithMany(p => p.MeterProfile)
                    .HasForeignKey(d => d.ProfileTypeId)
                    .HasConstraintName("METER_PROFILE_FK2");
            });

            modelBuilder.Entity<MeterReading>(entity =>
            {
                entity.HasKey(e => e.ReadingId)
                    .HasName("DEVICE_READING_PK");

                entity.ToTable("METER_READING");

                entity.HasIndex(e => e.ReadingId)
                    .HasName("DEVICE_READING_PK")
                    .IsUnique();

                entity.Property(e => e.ReadingId)
                    .HasColumnName("READING_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.ComModuleId)
                    .HasColumnName("COM_MODULE_ID")
                    .HasColumnType("VARCHAR2(25)");

                entity.Property(e => e.ComModulePort).HasColumnName("COM_MODULE_PORT");

                entity.Property(e => e.ConsumptionFactor)
                    .HasColumnName("CONSUMPTION_FACTOR")
                    .HasColumnType("FLOAT");

                entity.Property(e => e.ConsumptionUnitId)
                    .HasColumnName("CONSUMPTION_UNIT_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.GatewayId)
                    .HasColumnName("GATEWAY_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.IsBackflow).HasColumnName("IS_BACKFLOW");

                entity.Property(e => e.IsInvalidRead).HasColumnName("IS_INVALID_READ");

                entity.Property(e => e.MeterConsumptionDatetime)
                    .HasColumnName("METER_CONSUMPTION_DATETIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.MeterConsumptionUnitId)
                    .HasColumnName("METER_CONSUMPTION_UNIT_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.MeterConsumptionValue)
                    .HasColumnName("METER_CONSUMPTION_VALUE")
                    .HasColumnType("FLOAT");

                entity.Property(e => e.MeterId)
                    .HasColumnName("METER_ID")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.ObisId)
                    .HasColumnName("OBIS_ID")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.ServerReceiveDatetime)
                    .HasColumnName("SERVER_RECEIVE_DATETIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedUserId).HasColumnName("UPDATED_USER_ID");

                entity.HasOne(d => d.ConsumptionUnit)
                    .WithMany(p => p.MeterReading)
                    .HasForeignKey(d => d.ConsumptionUnitId)
                    .HasConstraintName("METER_READING_FK4");

                entity.HasOne(d => d.CreatedUser)
                    .WithMany(p => p.MeterReadingCreatedUser)
                    .HasForeignKey(d => d.CreatedUserId)
                    .HasConstraintName("METER_READING_FK5");

                entity.HasOne(d => d.Gateway)
                    .WithMany(p => p.MeterReading)
                    .HasForeignKey(d => d.GatewayId)
                    .HasConstraintName("METER_READING_FK2");

                entity.HasOne(d => d.Meter)
                    .WithMany(p => p.MeterReading)
                    .HasForeignKey(d => d.MeterId)
                    .HasConstraintName("METER_READING_FK1");

                entity.HasOne(d => d.Obis)
                    .WithMany(p => p.MeterReading)
                    .HasForeignKey(d => d.ObisId)
                    .HasConstraintName("METER_READING_FK3");

                entity.HasOne(d => d.UpdatedUser)
                    .WithMany(p => p.MeterReadingUpdatedUser)
                    .HasForeignKey(d => d.UpdatedUserId)
                    .HasConstraintName("METER_READING_FK6");
            });

            modelBuilder.Entity<MeterReads>(entity =>
            {
                entity.HasKey(e => e.MeterReadId)
                    .HasName("TABLE1_PK");

                entity.ToTable("METER_READS");

                entity.HasIndex(e => e.MeterReadId)
                    .HasName("TABLE1_PK")
                    .IsUnique();

                entity.Property(e => e.MeterReadId)
                    .HasColumnName("METER_READ_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.FirstReadingDate)
                    .HasColumnName("FIRST_READING_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.FirstReadingValue)
                    .HasColumnName("FIRST_READING_VALUE")
                    .HasColumnType("FLOAT");

                entity.Property(e => e.LastReadingDate)
                    .HasColumnName("LAST_READING_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.LastReadingInsertDate)
                    .HasColumnName("LAST_READING_INSERT_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.LastReadingValue)
                    .HasColumnName("LAST_READING_VALUE")
                    .HasColumnType("FLOAT");

                entity.Property(e => e.MaxReadingDate)
                    .HasColumnName("MAX_READING_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.MeterId)
                    .HasColumnName("METER_ID")
                    .HasColumnType("VARCHAR2(20)");

                entity.HasOne(d => d.Meter)
                    .WithMany(p => p.MeterReads)
                    .HasForeignKey(d => d.MeterId)
                    .HasConstraintName("METER_READS_FK1");
            });

            modelBuilder.Entity<MeterStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("METER_STATUS_PK");

                entity.ToTable("METER_STATUS");

                entity.HasIndex(e => e.StatusId)
                    .HasName("METER_STATUS_PK")
                    .IsUnique();

                entity.Property(e => e.StatusId)
                    .HasColumnName("STATUS_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Comments)
                    .HasColumnName("COMMENTS")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.StatusName)
                    .HasColumnName("STATUS_NAME")
                    .HasColumnType("VARCHAR2(30)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedUserId).HasColumnName("UPDATED_USER_ID");

                entity.HasOne(d => d.CreatedUser)
                    .WithMany(p => p.MeterStatusCreatedUser)
                    .HasForeignKey(d => d.CreatedUserId)
                    .HasConstraintName("METER_STATUS_FK1");

                entity.HasOne(d => d.UpdatedUser)
                    .WithMany(p => p.MeterStatusUpdatedUser)
                    .HasForeignKey(d => d.UpdatedUserId)
                    .HasConstraintName("METER_STATUS_FK2");
            });

            modelBuilder.Entity<MeterType>(entity =>
            {
                entity.ToTable("METER_TYPE");

                entity.HasIndex(e => e.MeterTypeId)
                    .HasName("DEVICE_TYPE_PK")
                    .IsUnique();

                entity.Property(e => e.MeterTypeId).HasColumnName("METER_TYPE_ID");

                entity.Property(e => e.Comments)
                    .HasColumnName("COMMENTS")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.MeterTypeCode)
                    .HasColumnName("METER_TYPE_CODE")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.MeterTypeDescription)
                    .HasColumnName("METER_TYPE_DESCRIPTION")
                    .HasColumnType("VARCHAR2(200)");

                entity.Property(e => e.MeterTypeName)
                    .HasColumnName("METER_TYPE_NAME")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedUserId).HasColumnName("UPDATED_USER_ID");

                entity.HasOne(d => d.CreatedUser)
                    .WithMany(p => p.MeterTypeCreatedUser)
                    .HasForeignKey(d => d.CreatedUserId)
                    .HasConstraintName("METER_TYPE_FK1");

                entity.HasOne(d => d.UpdatedUser)
                    .WithMany(p => p.MeterTypeUpdatedUser)
                    .HasForeignKey(d => d.UpdatedUserId)
                    .HasConstraintName("METER_TYPE_FK2");
            });

            modelBuilder.Entity<MeterVendor>(entity =>
            {
                entity.HasKey(e => e.VendorId)
                    .HasName("DEVICE_VENDOR_PK");

                entity.ToTable("METER_VENDOR");

                entity.HasIndex(e => e.VendorId)
                    .HasName("DEVICE_VENDOR_PK")
                    .IsUnique();

                entity.Property(e => e.VendorId)
                    .HasColumnName("VENDOR_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Comments)
                    .HasColumnName("COMMENTS")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedUserId).HasColumnName("UPDATED_USER_ID");

                entity.Property(e => e.VendorName)
                    .HasColumnName("VENDOR_NAME")
                    .HasColumnType("VARCHAR2(100)");

                entity.HasOne(d => d.CreatedUser)
                    .WithMany(p => p.MeterVendorCreatedUser)
                    .HasForeignKey(d => d.CreatedUserId)
                    .HasConstraintName("METER_VENDOR_FK1");

                entity.HasOne(d => d.UpdatedUser)
                    .WithMany(p => p.MeterVendorUpdatedUser)
                    .HasForeignKey(d => d.UpdatedUserId)
                    .HasConstraintName("METER_VENDOR_FK2");
            });

            modelBuilder.Entity<ObisData>(entity =>
            {
                entity.HasKey(e => e.ObisId)
                    .HasName("OBIS_DATA_PK");

                entity.ToTable("OBIS_DATA");

                entity.HasIndex(e => e.ObisId)
                    .HasName("OBIS_DATA_PK")
                    .IsUnique();

                entity.Property(e => e.ObisId)
                    .HasColumnName("OBIS_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Comments)
                    .HasColumnName("COMMENTS")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.MeasurandId).HasColumnName("MEASURAND_ID");

                entity.Property(e => e.MeasurementChannelId).HasColumnName("MEASUREMENT_CHANNEL_ID");

                entity.Property(e => e.MeasurmentParameter).HasColumnName("MEASURMENT_PARAMETER");

                entity.Property(e => e.ObisCode)
                    .HasColumnName("OBIS_CODE")
                    .HasColumnType("VARCHAR2(30)");

                entity.Property(e => e.ObisDescription)
                    .HasColumnName("OBIS_DESCRIPTION")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.ObisLable)
                    .HasColumnName("OBIS_LABLE")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.ObisMediaId).HasColumnName("OBIS_MEDIA_ID");

                entity.Property(e => e.ObisTypeId).HasColumnName("OBIS_TYPE_ID");

                entity.Property(e => e.ReportingType).HasColumnName("REPORTING_TYPE");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedUserId).HasColumnName("UPDATED_USER_ID");

                entity.HasOne(d => d.CreatedUser)
                    .WithMany(p => p.ObisDataCreatedUser)
                    .HasForeignKey(d => d.CreatedUserId)
                    .HasConstraintName("OBIS_DATA_FK1");

                entity.HasOne(d => d.UpdatedUser)
                    .WithMany(p => p.ObisDataUpdatedUser)
                    .HasForeignKey(d => d.UpdatedUserId)
                    .HasConstraintName("OBIS_DATA_FK2");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.ToTable("PAGE");

                entity.HasIndex(e => e.PageId)
                    .HasName("PAGE_PK")
                    .IsUnique();

                entity.Property(e => e.PageId)
                    .HasColumnName("PAGE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.PaegDescription)
                    .HasColumnName("PAEG_DESCRIPTION")
                    .HasColumnType("VARCHAR2(200)");

                entity.Property(e => e.PageName)
                    .HasColumnName("PAGE_NAME")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.PageOrder)
                    .HasColumnName("PAGE_ORDER")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasColumnType("VARCHAR2(300)");
            });

            modelBuilder.Entity<PagePrivilege>(entity =>
            {
                entity.ToTable("PAGE_PRIVILEGE");

                entity.HasIndex(e => e.PagePrivilegeId)
                    .HasName("PAGE_PRIVILEGE_PK")
                    .IsUnique();

                entity.Property(e => e.PagePrivilegeId)
                    .HasColumnName("PAGE_PRIVILEGE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.PageId)
                    .HasColumnName("PAGE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.PrivilegeId)
                    .HasColumnName("PRIVILEGE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedUserId).HasColumnName("UPDATED_USER_ID");

                entity.HasOne(d => d.CreatedUser)
                    .WithMany(p => p.PagePrivilegeCreatedUser)
                    .HasForeignKey(d => d.CreatedUserId)
                    .HasConstraintName("PAGE_PRIVILEGE_FK3");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PagePrivilege)
                    .HasForeignKey(d => d.PageId)
                    .HasConstraintName("PAGE_PRIVILEGE_FK1");

                entity.HasOne(d => d.Privilege)
                    .WithMany(p => p.PagePrivilege)
                    .HasForeignKey(d => d.PrivilegeId)
                    .HasConstraintName("PAGE_PRIVILEGE_FK2");

                entity.HasOne(d => d.UpdatedUser)
                    .WithMany(p => p.PagePrivilegeUpdatedUser)
                    .HasForeignKey(d => d.UpdatedUserId)
                    .HasConstraintName("PAGE_PRIVILEGE_FK4");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("PERMISSION");

                entity.HasIndex(e => e.PermissionId)
                    .HasName("PERMISSION_PK")
                    .IsUnique();

                entity.Property(e => e.PermissionId)
                    .HasColumnName("PERMISSION_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.PageId)
                    .HasColumnName("PAGE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.PrivilegeId)
                    .HasColumnName("PRIVILEGE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.RoleId)
                    .HasColumnName("ROLE_ID")
                    .HasColumnType("NUMBER");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.Permission)
                    .HasForeignKey(d => d.PageId)
                    .HasConstraintName("PERMISSION_FK1");

                entity.HasOne(d => d.Privilege)
                    .WithMany(p => p.Permission)
                    .HasForeignKey(d => d.PrivilegeId)
                    .HasConstraintName("PERMISSION_FK2");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Permission)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("PERMISSION_FK3");
            });

            modelBuilder.Entity<Persistedgrants>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.ToTable("PERSISTEDGRANTS");

                entity.HasIndex(e => e.Key)
                    .HasName("PK_PERSISTEDGRANTS")
                    .IsUnique();

                entity.Property(e => e.Key)
                    .HasColumnName("KEY")
                    .HasMaxLength(200);

                entity.Property(e => e.Clientid)
                    .IsRequired()
                    .HasColumnName("CLIENTID")
                    .HasMaxLength(200);

                entity.Property(e => e.Creationtime)
                    .HasColumnName("CREATIONTIME")
                    .HasColumnType("DATE");

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasColumnName("DATA");

                entity.Property(e => e.Expiration)
                    .HasColumnName("EXPIRATION")
                    .HasColumnType("DATE");

                entity.Property(e => e.Subjectid)
                    .HasColumnName("SUBJECTID")
                    .HasMaxLength(200);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("TYPE")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Privilege>(entity =>
            {
                entity.ToTable("PRIVILEGE");

                entity.HasIndex(e => e.PrivilegeId)
                    .HasName("PRIVILEGE_PK")
                    .IsUnique();

                entity.Property(e => e.PrivilegeId)
                    .HasColumnName("PRIVILEGE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasColumnType("VARCHAR2(200)");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.PrivilegeName)
                    .HasColumnName("PRIVILEGE_NAME")
                    .HasColumnType("VARCHAR2(50)");
            });

            modelBuilder.Entity<ProfileType>(entity =>
            {
                entity.ToTable("PROFILE_TYPE");

                entity.HasIndex(e => e.ProfileTypeId)
                    .HasName("PROFILE_TYPE_PK")
                    .IsUnique();

                entity.Property(e => e.ProfileTypeId)
                    .HasColumnName("PROFILE_TYPE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.ProfileTypeName)
                    .HasColumnName("PROFILE_TYPE_NAME")
                    .HasColumnType("VARCHAR2(100)");
            });

            modelBuilder.Entity<QueueAction>(entity =>
            {
                entity.ToTable("QUEUE_ACTION");

                entity.HasIndex(e => e.QueueActionId)
                    .HasName("QUEUE_ACTION_PK")
                    .IsUnique();

                entity.Property(e => e.QueueActionId)
                    .HasColumnName("QUEUE_ACTION_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.CreatedUserDate)
                    .HasColumnName("CREATED_USER_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.IsAutomatic).HasColumnName("IS_AUTOMATIC");

                entity.Property(e => e.IsSingle).HasColumnName("IS_SINGLE");

                entity.Property(e => e.QueueActionComment)
                    .HasColumnName("QUEUE_ACTION_COMMENT")
                    .HasColumnType("VARCHAR2(500)");

                entity.Property(e => e.QueueActionName)
                    .HasColumnName("QUEUE_ACTION_NAME")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.UpdatedUserDate)
                    .HasColumnName("UPDATED_USER_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedUserId).HasColumnName("UPDATED_USER_ID");

                entity.HasOne(d => d.CreatedUser)
                    .WithMany(p => p.QueueActionCreatedUser)
                    .HasForeignKey(d => d.CreatedUserId)
                    .HasConstraintName("QUEUE_ACTION_FK1");

                entity.HasOne(d => d.UpdatedUser)
                    .WithMany(p => p.QueueActionUpdatedUser)
                    .HasForeignKey(d => d.UpdatedUserId)
                    .HasConstraintName("QUEUE_ACTION_FK2");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");

                entity.HasIndex(e => e.RoleId)
                    .HasName("ROLE_PK")
                    .IsUnique();

                entity.Property(e => e.RoleId)
                    .HasColumnName("ROLE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasColumnType("VARCHAR2(200)");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.RoleName)
                    .HasColumnName("ROLE_NAME")
                    .HasColumnType("VARCHAR2(50)");
            });

            modelBuilder.Entity<SimCardList>(entity =>
            {
                entity.HasKey(e => e.SimCardNo)
                    .HasName("SIM_CARD_LIST_PK");

                entity.ToTable("SIM_CARD_LIST");

                entity.HasIndex(e => e.SimCardNo)
                    .HasName("SIM_CARD_LIST_PK")
                    .IsUnique();

                entity.Property(e => e.SimCardNo)
                    .HasColumnName("SIM_CARD_NO")
                    .HasColumnType("VARCHAR2(30)");
            });

            modelBuilder.Entity<TransactionLog>(entity =>
            {
                entity.ToTable("TRANSACTION_LOG");

                entity.HasIndex(e => e.TransactionLogId)
                    .HasName("TRANSACTION_LOG_PK")
                    .IsUnique();

                entity.Property(e => e.TransactionLogId)
                    .HasColumnName("TRANSACTION_LOG_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.NewValue)
                    .HasColumnName("NEW_VALUE")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.OldValue)
                    .HasColumnName("OLD_VALUE")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.PageId)
                    .HasColumnName("PAGE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.PrivilegeId)
                    .HasColumnName("PRIVILEGE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.TransactionDate)
                    .HasColumnName("TRANSACTION_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.TransactionLog)
                    .HasForeignKey(d => d.PageId)
                    .HasConstraintName("TRANSACTION_LOG_FK1");

                entity.HasOne(d => d.Privilege)
                    .WithMany(p => p.TransactionLog)
                    .HasForeignKey(d => d.PrivilegeId)
                    .HasConstraintName("TRANSACTION_LOG_FK3");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TransactionLog)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("TRANSACTION_LOG_FK2");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.ToTable("UNIT");

                entity.HasIndex(e => e.UnitId)
                    .HasName("UNIT_PK")
                    .IsUnique();

                entity.Property(e => e.UnitId)
                    .HasColumnName("UNIT_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Column1)
                    .HasColumnName("COLUMN1")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.Comments)
                    .HasColumnName("COMMENTS")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.ConversionFactor)
                    .HasColumnName("CONVERSION_FACTOR")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.MeasurementType)
                    .HasColumnName("MEASUREMENT_TYPE")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.UnitDescription)
                    .HasColumnName("UNIT_DESCRIPTION")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.UnitLabel)
                    .HasColumnName("UNIT_LABEL")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedUserId).HasColumnName("UPDATED_USER_ID");

                entity.HasOne(d => d.CreatedUser)
                    .WithMany(p => p.UnitCreatedUser)
                    .HasForeignKey(d => d.CreatedUserId)
                    .HasConstraintName("UNIT_FK1");

                entity.HasOne(d => d.UpdatedUser)
                    .WithMany(p => p.UnitUpdatedUser)
                    .HasForeignKey(d => d.UpdatedUserId)
                    .HasConstraintName("UNIT_FK2");
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.ToTable("USER_GROUP");

                entity.HasIndex(e => e.UserGroupId)
                    .HasName("USER_GROUP_PK")
                    .IsUnique();

                entity.Property(e => e.UserGroupId)
                    .HasColumnName("USER_GROUP_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.DeviceGroupId)
                    .HasColumnName("DEVICE_GROUP_ID")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.UpdatedUserD).HasColumnName("UPDATED_USER_D");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.CreatedUser)
                    .WithMany(p => p.UserGroupCreatedUser)
                    .HasForeignKey(d => d.CreatedUserId)
                    .HasConstraintName("USER_GROUP_FK3");

                entity.HasOne(d => d.DeviceGroup)
                    .WithMany(p => p.UserGroup)
                    .HasForeignKey(d => d.DeviceGroupId)
                    .HasConstraintName("USER_GROUP_FK1");

                entity.HasOne(d => d.UpdatedUserDNavigation)
                    .WithMany(p => p.UserGroupUpdatedUserDNavigation)
                    .HasForeignKey(d => d.UpdatedUserD)
                    .HasConstraintName("USER_GROUP_FK4");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserGroupUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("USER_GROUP_FK2");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("USER_ROLE");

                entity.HasIndex(e => e.UserRoleId)
                    .HasName("USER_ROLE_PK")
                    .IsUnique();

                entity.Property(e => e.UserRoleId)
                    .HasColumnName("USER_ROLE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.RoleId)
                    .HasColumnName("ROLE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("USER_ROLE_FK2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("USER_ROLE_FK1");
            });

            modelBuilder.Entity<UserBasicData>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("USERS_PK");

                entity.ToTable("USERS");

                entity.HasIndex(e => e.UserId)
                    .HasName("USERS_PK")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnName("USER_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Column4).HasColumnName("COLUMN4");

                entity.Property(e => e.Comments)
                    .HasColumnName("COMMENTS")
                    .HasColumnType("VARCHAR2(2000)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.CreatedUserId).HasColumnName("CREATED_USER_ID");

                entity.Property(e => e.DeleteStatus).HasColumnName("DELETE_STATUS");

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.FamilyName)
                    .HasColumnName("FAMILY_NAME")
                    .HasColumnType("VARCHAR2(25)");

                entity.Property(e => e.FiledLogins)
                    .HasColumnName("FILED_LOGINS")
                    .HasColumnType("NUMBER(2)");

                entity.Property(e => e.FirstName)
                    .HasColumnName("FIRST_NAME")
                    .HasColumnType("VARCHAR2(25)");

                entity.Property(e => e.LastLoginDate)
                    .HasColumnName("LAST_LOGIN_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.LastLoginIp)
                    .HasColumnName("LAST_LOGIN_IP")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.LastName)
                    .HasColumnName("LAST_NAME")
                    .HasColumnType("VARCHAR2(25)");

                entity.Property(e => e.MiddleName)
                    .HasColumnName("MIDDLE_NAME")
                    .HasColumnType("VARCHAR2(25)");

                entity.Property(e => e.Mobile)
                    .HasColumnName("MOBILE")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.Phone)
                    .HasColumnName("PHONE")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.UpdatedUserId).HasColumnName("UPDATED_USER_ID");

                entity.Property(e => e.UserLanguageId)
                    .HasColumnName("USER_LANGUAGE_ID")
                    .HasColumnType("VARCHAR2(2)");

                entity.Property(e => e.UserName)
                    .HasColumnName("USER_NAME")
                    .HasColumnType("VARCHAR2(20)");

                entity.HasOne(d => d.UpdatedUser)
                    .WithMany(p => p.InverseUpdatedUser)
                    .HasForeignKey(d => d.UpdatedUserId)
                    .HasConstraintName("USERS_FK2");
            });

        }

    }
}
