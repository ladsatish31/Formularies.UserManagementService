﻿using AutoMapper;
namespace Formularies.UserManagementService.Api.Helper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Infrastructure.Entities.Role, Core.Models.Role>().ReverseMap();
            CreateMap<Infrastructure.Entities.User, Core.Models.User>().ReverseMap();
        }
    }
}
