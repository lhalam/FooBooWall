using DataAccess.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ExternalLoginDAO : AbstractDAO<ExternalLogin>
    {
        public override void Create(ExternalLogin entity)
        {
            throw new NotImplementedException();
        }

        public override ExternalLogin Read(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(ExternalLogin entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(ExternalLogin entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddLoginAsync(User user, UserLoginInfo info)
        {
            using(SqlConnection conn = GetConnection())
            {
                string sql = "INSERT INTO externalLogins(User_id, LoginProvider, ProviderKey) values(@userId, @loginProvider, @providerKey)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@userId", System.Data.SqlDbType.Int).Value = user.Id;
                cmd.Parameters.Add("@loginProvider", System.Data.SqlDbType.VarChar, -1).Value = info.LoginProvider;
                cmd.Parameters.Add("@providerKey", System.Data.SqlDbType.VarChar, -1).Value = info.ProviderKey;
                await conn.OpenAsync();
               var t =  await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<User> FindAsync(UserLoginInfo info)
        {
            if (info == null)
            {
                throw new ArgumentNullException("login");
            }

            using (SqlConnection conn = GetConnection())
            {
                await conn.OpenAsync();
                string sql = "select u.* from users u inner join externalLogins l on l.User_id = u.ID where l.LoginProvider = @loginProvider and l.ProviderKey = @providerKey";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@loginProvider", System.Data.SqlDbType.VarChar, -1).Value = info.LoginProvider;
                cmd.Parameters.Add("@providerKey", System.Data.SqlDbType.VarChar, -1).Value = info.ProviderKey;
          
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if(reader.HasRows)
                {
                    await reader.ReadAsync();
                    User user = new User
                    {
                        Id = (int)reader["id"],
                        FirstName = Convert(reader["FirstName"]),
                        LastName = Convert(reader["LastName"]),
                        Login = Convert(reader["Login"]),
                        EMail = Convert(reader["Email"]),
                        PasswordHash = Convert(reader["PasswordHash"]),
                        Birthday = (reader["BirthDate"] is DBNull) ? DateTime.Now : (DateTime)reader["BirthDate"],
                        SecurityStamp = Convert(reader["SecurityStamp"]),
                        Skype = Convert(reader["SkypeName"]),
                        ImageId = (int)reader["Image_id"]
                    };
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            using (SqlConnection conn = GetConnection())
            {
                string sql = "select LoginProvider, ProviderKey from externalLogins where User_id = @userId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@userId", System.Data.SqlDbType.Int).Value = user.Id;
                await conn.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                List<UserLoginInfo> result = new List<UserLoginInfo>();
                while(await reader.ReadAsync())
                {
                    UserLoginInfo currInfo = 
                        new UserLoginInfo((string)reader["LoginProvider"], (string)reader["ProviderKey"]);
                    result.Add(currInfo);
                }

                return result;
            }
        }

        public async Task RemoveLoginAsync(User user, UserLoginInfo info)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (info == null)
                throw new ArgumentNullException("UserLoginInfo");

            using(SqlConnection conn = GetConnection())
            {
                string sql = "delete from ExternalLogins where User_id = @userId and LoginProvider = @loginProvider and ProviderKey = @providerKey";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@userId", System.Data.SqlDbType.Int).Value = user.Id;
                cmd.Parameters.Add("@loginProvider", System.Data.SqlDbType.VarChar, -1).Value = info.LoginProvider;
                cmd.Parameters.Add("@providerKey", System.Data.SqlDbType.VarChar, -1).Value = info.ProviderKey;
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
