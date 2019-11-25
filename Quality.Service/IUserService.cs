using System.Threading.Tasks;
using Quality.Common.Dto.User;
using Quality.DataAccess.Entities;

namespace Quality.Service
{
    public interface IUserService
    {
        Task<User> Login(LoginDto credentials);
    }
}