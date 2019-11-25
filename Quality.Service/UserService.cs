using Quality.Common.Dto.User;
using Quality.DataAccess;
using Quality.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quality.Service
{
    public class UserService : IUserService
    {
        private readonly IDbContext _dbContext;
        public UserService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Login(LoginDto credentials)
        {
            string username = credentials.Username;
            string password = credentials.Password;
            return await _dbContext.User.GetSingleFilteredByAsync(m =>
                m.Username == username && m.Password == password);
        }
    }
}
