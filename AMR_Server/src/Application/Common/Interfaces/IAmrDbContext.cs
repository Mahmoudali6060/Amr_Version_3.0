
using System.Threading;
using System.Threading.Tasks;
using AMR_Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AMR_Server.Application.Common.Interfaces
{
    public interface IAmrDbContext
    {

        public  DbSet<AlarmCategory> AlarmCategory { get; set; }
        public  DbSet<AlarmCode> AlarmCode { get; set; }
        public  DbSet<AlarmLevel> AlarmLevel { get; set; }
        public  DbSet<Area> Area { get; set; }
        public  DbSet<Aspnetroleclaims> Aspnetroleclaims { get; set; }
        public  DbSet<Aspnetroles> Aspnetroles { get; set; }
        public  DbSet<Aspnetuserclaims> Aspnetuserclaims { get; set; }
        public  DbSet<Aspnetuserlogins> Aspnetuserlogins { get; set; }
        public  DbSet<Aspnetuserroles> Aspnetuserroles { get; set; }
        public  DbSet<Aspnetusers> Aspnetusers { get; set; }
        public  DbSet<Aspnetusertokens> Aspnetusertokens { get; set; }
        public  DbSet<CcbMeters> CcbMeters { get; set; }
        public  DbSet<City> City { get; set; }
        public  DbSet<ConnectingStatus> ConnectingStatus { get; set; }
        public  DbSet<DeviceDma> DeviceDma { get; set; }
        public  DbSet<DeviceGroup> DeviceGroup { get; set; }
        public  DbSet<DeviceQueueAction> DeviceQueueAction { get; set; }
        public  DbSet<Devicecodes> Devicecodes { get; set; }
        public  DbSet<EditableColumn> EditableColumn { get; set; }
        public  DbSet<ErrorInfo> ErrorInfo { get; set; }
        public  DbSet<ErrorLog> ErrorLog { get; set; }
        public  DbSet<Gateway> Gateway { get; set; }
        public  DbSet<GatewayConnection> GatewayConnection { get; set; }
        public  DbSet<GatewayProfile> GatewayProfile { get; set; }
        public  DbSet<GlobalFilter> GlobalFilter { get; set; }
        public  DbSet<GlobalFilterPage> GlobalFilterPage { get; set; }
        public  DbSet<Meter> Meter { get; set; }
        public  DbSet<MeterAlarm> MeterAlarm { get; set; }
        public  DbSet<MeterAlarmConfig> MeterAlarmConfig { get; set; }
        public  DbSet<MeterAlarmRf> MeterAlarmRf { get; set; }
        public  DbSet<MeterBillingConfig> MeterBillingConfig { get; set; }
        public  DbSet<MeterComments> MeterComments { get; set; }
        public  DbSet<MeterGatewayConfig> MeterGatewayConfig { get; set; }
        public  DbSet<MeterModel> MeterModel { get; set; }
        public  DbSet<MeterPartion> MeterPartion { get; set; }
        public  DbSet<MeterProfile> MeterProfile { get; set; }
        public  DbSet<MeterReading> MeterReading { get; set; }
        public  DbSet<MeterReads> MeterReads { get; set; }
        public  DbSet<MeterStatus> MeterStatus { get; set; }
        public  DbSet<MeterType> MeterType { get; set; }
        public  DbSet<MeterVendor> MeterVendor { get; set; }
        public  DbSet<ObisData> ObisData { get; set; }
        public  DbSet<Page> Page { get; set; }
        public  DbSet<PagePrivilege> PagePrivilege { get; set; }
        public  DbSet<Permission> Permission { get; set; }
        public  DbSet<Persistedgrants> Persistedgrants { get; set; }
        public  DbSet<Privilege> Privilege { get; set; }
        public  DbSet<ProfileType> ProfileType { get; set; }
        public  DbSet<QueueAction> QueueAction { get; set; }
        public  DbSet<Role> Role { get; set; }
        public  DbSet<SimCardList> SimCardList { get; set; }
        public  DbSet<TransactionLog> TransactionLog { get; set; }
        public  DbSet<Unit> Unit { get; set; }
        public  DbSet<UserBasicData> UserBasicData { get; set; }
        public  DbSet<UserGroup> UserGroup { get; set; }
        public  DbSet<UserRole> UserRole { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
