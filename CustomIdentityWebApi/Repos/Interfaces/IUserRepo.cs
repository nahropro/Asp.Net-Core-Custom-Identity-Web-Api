using CustomIdentityWebApi.Models;
using CustomIdentityWebApi.Res.UserRes;
using System.Threading.Tasks;

namespace CustomIdentityWebApi.Repos
{
    public interface IUserRepo
    {
        Task<User> CreateUserAsync(CreateUserRes res);
        Task IncludeRelatedTables(User user);
        Task<string> LoginAsync(LoginRes res);
    }
}