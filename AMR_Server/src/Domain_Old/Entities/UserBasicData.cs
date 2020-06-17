using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class UserBasicData
    {
        public UserBasicData()
        {
            DeviceDmaCreatedUser = new HashSet<DeviceDma>();
            DeviceDmaUpdatedUser = new HashSet<DeviceDma>();
            DeviceGroupCreatedUser = new HashSet<DeviceGroup>();
            DeviceGroupUpdatedUser = new HashSet<DeviceGroup>();
            DeviceQueueActionCreatedUser = new HashSet<DeviceQueueAction>();
            DeviceQueueActionUpdatedUser = new HashSet<DeviceQueueAction>();
            ErrorLog = new HashSet<ErrorLog>();
            GatewayApprovedUser = new HashSet<Gateway>();
            GatewayConnectionCreatedUser = new HashSet<GatewayConnection>();
            GatewayConnectionUpdatedUser = new HashSet<GatewayConnection>();
            GatewayCreatedUser = new HashSet<Gateway>();
            GatewayUpdateUser = new HashSet<Gateway>();
            InverseUpdatedUser = new HashSet<UserBasicData>();
            MeterAlarmCreatedUser = new HashSet<MeterAlarm>();
            MeterAlarmRfCreatedUser = new HashSet<MeterAlarmRf>();
            MeterAlarmRfUpdatedUser = new HashSet<MeterAlarmRf>();
            MeterAlarmUpdatedUser = new HashSet<MeterAlarm>();
            MeterCommentsCreatedUser = new HashSet<MeterComments>();
            MeterCommentsReviewedUser = new HashSet<MeterComments>();
            MeterCommentsUpdateUser = new HashSet<MeterComments>();
            MeterCreatedUser = new HashSet<Meter>();
            MeterModelCreatedUser = new HashSet<MeterModel>();
            MeterModelUpdatedUser = new HashSet<MeterModel>();
            MeterReadingCreatedUser = new HashSet<MeterReading>();
            MeterReadingUpdatedUser = new HashSet<MeterReading>();
            MeterStatusCreatedUser = new HashSet<MeterStatus>();
            MeterStatusUpdatedUser = new HashSet<MeterStatus>();
            MeterTypeCreatedUser = new HashSet<MeterType>();
            MeterTypeUpdatedUser = new HashSet<MeterType>();
            MeterUpdatedUser = new HashSet<Meter>();
            MeterVendorCreatedUser = new HashSet<MeterVendor>();
            MeterVendorUpdatedUser = new HashSet<MeterVendor>();
            ObisDataCreatedUser = new HashSet<ObisData>();
            ObisDataUpdatedUser = new HashSet<ObisData>();
            PagePrivilegeCreatedUser = new HashSet<PagePrivilege>();
            PagePrivilegeUpdatedUser = new HashSet<PagePrivilege>();
            QueueActionCreatedUser = new HashSet<QueueAction>();
            QueueActionUpdatedUser = new HashSet<QueueAction>();
            TransactionLog = new HashSet<TransactionLog>();
            UnitCreatedUser = new HashSet<Unit>();
            UnitUpdatedUser = new HashSet<Unit>();
            UserGroupCreatedUser = new HashSet<UserGroup>();
            UserGroupUpdatedUserDNavigation = new HashSet<UserGroup>();
            UserGroupUser = new HashSet<UserGroup>();
            UserRole = new HashSet<UserRole>();
        }

        public short UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FamilyName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string UserLanguageId { get; set; }
        public byte? FiledLogins { get; set; }
        public string LastLoginIp { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool? Column4 { get; set; }
        public string Comments { get; set; }
        public bool? DeleteStatus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Aspnetuserid { get; set; }

        public virtual Aspnetusers Aspnetuser { get; set; }
        public virtual UserBasicData UpdatedUser { get; set; }
        public virtual ICollection<DeviceDma> DeviceDmaCreatedUser { get; set; }
        public virtual ICollection<DeviceDma> DeviceDmaUpdatedUser { get; set; }
        public virtual ICollection<DeviceGroup> DeviceGroupCreatedUser { get; set; }
        public virtual ICollection<DeviceGroup> DeviceGroupUpdatedUser { get; set; }
        public virtual ICollection<DeviceQueueAction> DeviceQueueActionCreatedUser { get; set; }
        public virtual ICollection<DeviceQueueAction> DeviceQueueActionUpdatedUser { get; set; }
        public virtual ICollection<ErrorLog> ErrorLog { get; set; }
        public virtual ICollection<Gateway> GatewayApprovedUser { get; set; }
        public virtual ICollection<GatewayConnection> GatewayConnectionCreatedUser { get; set; }
        public virtual ICollection<GatewayConnection> GatewayConnectionUpdatedUser { get; set; }
        public virtual ICollection<Gateway> GatewayCreatedUser { get; set; }
        public virtual ICollection<Gateway> GatewayUpdateUser { get; set; }
        public virtual ICollection<UserBasicData> InverseUpdatedUser { get; set; }
        public virtual ICollection<MeterAlarm> MeterAlarmCreatedUser { get; set; }
        public virtual ICollection<MeterAlarmRf> MeterAlarmRfCreatedUser { get; set; }
        public virtual ICollection<MeterAlarmRf> MeterAlarmRfUpdatedUser { get; set; }
        public virtual ICollection<MeterAlarm> MeterAlarmUpdatedUser { get; set; }
        public virtual ICollection<MeterComments> MeterCommentsCreatedUser { get; set; }
        public virtual ICollection<MeterComments> MeterCommentsReviewedUser { get; set; }
        public virtual ICollection<MeterComments> MeterCommentsUpdateUser { get; set; }
        public virtual ICollection<Meter> MeterCreatedUser { get; set; }
        public virtual ICollection<MeterModel> MeterModelCreatedUser { get; set; }
        public virtual ICollection<MeterModel> MeterModelUpdatedUser { get; set; }
        public virtual ICollection<MeterReading> MeterReadingCreatedUser { get; set; }
        public virtual ICollection<MeterReading> MeterReadingUpdatedUser { get; set; }
        public virtual ICollection<MeterStatus> MeterStatusCreatedUser { get; set; }
        public virtual ICollection<MeterStatus> MeterStatusUpdatedUser { get; set; }
        public virtual ICollection<MeterType> MeterTypeCreatedUser { get; set; }
        public virtual ICollection<MeterType> MeterTypeUpdatedUser { get; set; }
        public virtual ICollection<Meter> MeterUpdatedUser { get; set; }
        public virtual ICollection<MeterVendor> MeterVendorCreatedUser { get; set; }
        public virtual ICollection<MeterVendor> MeterVendorUpdatedUser { get; set; }
        public virtual ICollection<ObisData> ObisDataCreatedUser { get; set; }
        public virtual ICollection<ObisData> ObisDataUpdatedUser { get; set; }
        public virtual ICollection<PagePrivilege> PagePrivilegeCreatedUser { get; set; }
        public virtual ICollection<PagePrivilege> PagePrivilegeUpdatedUser { get; set; }
        public virtual ICollection<QueueAction> QueueActionCreatedUser { get; set; }
        public virtual ICollection<QueueAction> QueueActionUpdatedUser { get; set; }
        public virtual ICollection<TransactionLog> TransactionLog { get; set; }
        public virtual ICollection<Unit> UnitCreatedUser { get; set; }
        public virtual ICollection<Unit> UnitUpdatedUser { get; set; }
        public virtual ICollection<UserGroup> UserGroupCreatedUser { get; set; }
        public virtual ICollection<UserGroup> UserGroupUpdatedUserDNavigation { get; set; }
        public virtual ICollection<UserGroup> UserGroupUser { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
