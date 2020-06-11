using AMR_Server.Application.Common.Mappings;
using AMR_Server.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace AMR_Server.Application.MeterVendors.Queries
{
    public partial class MeterVendorDto : IMapFrom<MeterVendor>
    {
        public short VendorId { get; set; }
        public string VendorName { get; set; }
        public bool? IsActive { get; set; }
        public string Comments { get; set; }
        public bool? DeleteStatus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MeterVendor, MeterVendorDto>();
        }

    }

}
