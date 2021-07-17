using System.Threading.Tasks;

namespace CustomIdentityWebApi.Repos
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();
    }
}