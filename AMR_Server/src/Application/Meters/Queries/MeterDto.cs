using AMR_Server.Application.Common.Mappings;
using AMR_Server.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace AMR_Server.Application.Meters.Queries
{
    public partial class MeterDto : IMapFrom<Meter>
    {
        public string MeterId { get; set; }
        public string BadgeNo { get; set; }
        public string Hcn { get; set; }
        public decimal? GroupId { get; set; }
        public short? VendorId { get; set; }
        public short? ModelId { get; set; }
        public decimal? InventoryStaus { get; set; }
        public decimal? MeterStatusId { get; set; }
        public string SerialNo { get; set; }
        public DateTime? InstalationDate { get; set; }
        public DateTime? LastDatasetDate { get; set; }
        public DateTime? LastOnlineDate { get; set; }
        public bool? IsActive { get; set; }
        public string Comments { get; set; }
        public bool? DeleteStaus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDriveby { get; set; }
        public string OnlineStatusLastChangeDate { get; set; }
        public string SimCardNo { get; set; }
        public string CoordinatesType { get; set; }
        public bool? InStore { get; set; }
        public bool? IsSmartInCcb { get; set; }
        public bool? IsInControlRoom { get; set; }
        public bool? IsBulk { get; set; }
        public short? ModuleId { get; set; }
        public bool? ModulePort { get; set; }
        public short? ModuleVendorId { get; set; }
        public short? ModuleModelId { get; set; }
        public short? AreaId { get; set; }
        public string StreetName { get; set; }
        public string FloorNo { get; set; }
        public string PostalCode { get; set; }
        public decimal? CoordLat { get; set; }
        public decimal? CoordLon { get; set; }
        public decimal? DmaId { get; set; }
        public bool? MeterTypeId { get; set; }
        public decimal? ConnectingStatus { get; set; }
        public string Column2 { get; set; }
        public string VendorName { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Meter, MeterDto>();
            //.ForMember(d => d.Priority, opt => opt.MapFrom(s => (int)s.Priority));
        }

    }
}
