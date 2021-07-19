using AutoMapper;
using CustomIdentityWebApi.Models;
using CustomIdentityWebApi.Res.RoleRes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomIdentityWebApi.Repos
{
    public class RoleRepo : IRoleRepo
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RoleRepo(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Role> CreateRoleAsync(SaveRoleRes res)
        {
            Role role = mapper.Map<Role>(res);

            await context.Roles.AddAsync(role);

            return role;
        }

        public async Task<Role> UpdateRoleAsync(long id, SaveRoleRes res)
        {
            Role role = await GetRoleAsync(id);

            if (role is null)
                return null;

            mapper.Map(res, role);

            return role;
        }

        public async Task<Role> GetRoleAsync(long id)
        {
            return await context.Roles.FindAsync(id);
        }

        public async Task<List<SelectRoleRes>> GetRolesAsync()
        {
            return await context.Roles.Select(r => new SelectRoleRes
            {
                Id = r.Id,
                Name = r.Name
            }).ToListAsync();
        }

        public Role RemoveRole(Role role)
        {
            context.Roles.Remove(role);

            return role;
        }

        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            return await context.Roles.SingleOrDefaultAsync(r => r.Name == roleName);
        }
    }
}
