using CustomIdentityWebApi.Models;
using CustomIdentityWebApi.Res.RoleRes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomIdentityWebApi.Repos
{
    public interface IRoleRepo
    {
        Task<Role> CreateRoleAsync(SaveRoleRes res);
        Task<Role> GetRoleAsync(long id);
        Task<List<SelectRoleRes>> GetRolesAsync();
        Task<Role> UpdateRoleAsync(long id, SaveRoleRes res);
        Role RemoveRole(Role role);
    }
}