using AMR_Server.Application.Common.Mappings;
using AMR_Server.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMR_Server.Application.UserBasicDatas.Commands
{
    public class UserBasicDataDto : IMapFrom<UserBasicData>
    {
        public short UserId { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string AspNetUserId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserBasicData, UserBasicData>();
        }

    }
}
