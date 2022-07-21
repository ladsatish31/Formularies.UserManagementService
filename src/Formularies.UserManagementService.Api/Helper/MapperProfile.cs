using AutoMapper;
using System.Linq;

namespace Formularies.UserManagementService.Api.Helper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Infrastructure.Entities.Role, Core.Models.Role>().ReverseMap();            
            CreateMap<Infrastructure.Entities.Role, Core.Request.RoleRequest>().ReverseMap();
            CreateMap<Infrastructure.Entities.User, Core.Models.User>().ReverseMap();
            CreateMap<Infrastructure.Entities.User, Core.Request.UserCreateRequest>().ReverseMap();
            CreateMap<Infrastructure.Entities.User, Core.Request.UserUpdateRequest>().ReverseMap();
            CreateMap<Infrastructure.Entities.RefreshToken, Core.Models.RefreshToken>().ReverseMap();
            CreateMap<Core.Models.User, Core.Response.AuthenticateResponse>();
        }
    }
}
