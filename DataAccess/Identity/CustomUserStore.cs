using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using DataAccess.Entities;
using DataAccess.DAO;

namespace DataAccess.Identity
{
    public class CustomUserStore : IUserStore<User, int>, IUserLoginStore<User, int>, IUserPasswordStore<User, int>, IUserSecurityStampStore<User, int>
    {
        private readonly UserDAO userDAO = new UserDAO();
        #region IUserStore
        public Task CreateAsync(User user)
        {
            userDAO.Create(user);
            return Task.FromResult(1);
        }

        public Task DeleteAsync(User user)
        {
            userDAO.Delete(user);
            return Task.FromResult(1);
        }

        public Task<User> FindByIdAsync(int userId)
        {
            return Task<User>.FromResult(userDAO.Read(userId));
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return Task<User>.FromResult(userDAO.GetUserByLogin(userName));
        }

        public Task UpdateAsync(User user)
        {
            userDAO.Update(user);
            return Task.FromResult(1);
        }

        public void Dispose()
        {

        }
        #endregion

        #region IUserLoginStore
        public Task AddLoginAsync(User user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }


        public Task<User> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
        #endregion
        
        #region IUserPasswordStore
        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(1);
        }
        #endregion

        #region IUserSecurityStampStore
        public Task<string> GetSecurityStampAsync(User user)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(User user, string stamp)
        {
            user.SecurityStamp = stamp;
            return Task.FromResult(1);
        }
        #endregion
        
    }
}
