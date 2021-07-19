using AutoMapper;
using CustomIdentityWebApi.Models;
using CustomIdentityWebApi.Res.RoleRes;
using CustomIdentityWebApi.Res.UserRes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomIdentityWebApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SaveRoleRes, Role>();
            CreateMap<CreateUserRes, User>();
            CreateMap<UpdateUserRes, User>();

            CreateMap<Role, SelectRoleRes>();
            CreateMap<User, SelectUserRes>();
        }
    }
}
