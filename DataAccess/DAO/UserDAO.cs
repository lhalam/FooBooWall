using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess.DAO
{
    public class UserDAO: AbstractDAO<User>
    {
        public override void Create(User entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql =  "INSERT INTO PmiDatabase.dbo.users(FirstName, LastName, Login, Email, Password, BirthDate, Image_id, VK_ID, FB_ID)" +
                    "VALUES(@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = entity.FirstName;
                cmd.Parameters.Add("@param2", SqlDbType.VarChar, 255).Value = entity.LastName;
                cmd.Parameters.Add("@param3", SqlDbType.VarChar, 255).Value = entity.Login;
                cmd.Parameters.Add("@param4", SqlDbType.VarChar, 255).Value = entity.EMail;
                cmd.Parameters.Add("@param5", SqlDbType.VarChar, 20).Value = entity.Password;
                cmd.Parameters.Add("@param6", SqlDbType.Date).Value = entity.Birthday;
                cmd.Parameters.Add("@param7", SqlDbType.Int).Value = entity.Image_ID;
                cmd.Parameters.Add("@param8", SqlDbType.VarChar, 50).Value = entity.VK_ID;
                cmd.Parameters.Add("@param9", SqlDbType.VarChar, 50).Value = entity.FB_ID;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }
        public override User Read(int id)
        {
            User user = null;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "SELECT * FROM PmiDatabase.dbo.users WHERE id = @param1";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.Int).Value = id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    user = new User
                    {
                        FirstName = (string)reader.GetValue(1),
                        LastName = (string)reader.GetValue(2),
                        Login = (string)reader.GetValue(3),
                        EMail = (string)reader.GetValue(4),
                        Password = (string)reader.GetValue(5),
                        Birthday = (DateTime)reader.GetValue(6),
                        Image_ID = (int)reader.GetValue(7),
                        VK_ID = (String)reader.GetValue(8),
                        FB_ID = (String)reader.GetValue(9)
                    };
                }
            }
            return user;
        }

        public override void Update(User entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "UPDATE PmiDatabase.dbo.users SET FirstName = @param1, LastName = @param2, Email = @param3," +
                    " Password = @param4, BirthDate = @param5, Image_id = @param6, VK_ID = @param7, FB_ID = @param8 WHERE Login = @param9";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = entity.FirstName;
                cmd.Parameters.Add("@param2", SqlDbType.VarChar, 255).Value = entity.LastName;
                cmd.Parameters.Add("@param3", SqlDbType.VarChar, 255).Value = entity.EMail;
                cmd.Parameters.Add("@param4", SqlDbType.VarChar, 20).Value = entity.Password;
                cmd.Parameters.Add("@param5", SqlDbType.Date).Value = entity.Birthday;
                cmd.Parameters.Add("@param6", SqlDbType.Int).Value = entity.Image_ID;
                cmd.Parameters.Add("@param7", SqlDbType.VarChar, 50).Value = entity.VK_ID;
                cmd.Parameters.Add("@param8", SqlDbType.VarChar, 50).Value = entity.FB_ID;
                cmd.Parameters.Add("@param9", SqlDbType.VarChar, 255).Value = entity.Login;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public override void Delete(User entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "DELETE FROM PmiDatabase.dbo.users WHERE Login = @param1";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = entity.Login;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public User GetUserByLoginAndPassword(string login, string password)
        {
            User user = null;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "SELECT * FROM PmiDatabase.dbo.users WHERE Login = @param1 AND Password = @param2";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = login;
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 20).Value = password;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    user = new User
                    {
                        ID = (int)reader.GetValue(0),
                        FirstName = (string)reader.GetValue(1),
                        LastName = (string)reader.GetValue(2),
                        Login = (string)reader.GetValue(3),
                        EMail = (string)reader.GetValue(4),
                        Password = (string)reader.GetValue(5),
                        Birthday = (DateTime)reader.GetValue(6),
                        Image_ID = (int)reader.GetValue(7),
                        VK_ID = (String)reader.GetValue(8),
                        FB_ID = (String)reader.GetValue(9)
                    };
                }
                return user;
            }
        }

        public string GetPasswordByLogin(string login)
        {
            string pass;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "SELECT Password FROM PmiDatabase.dbo.users WHERE login = @param1";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = login;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    pass = (string)reader.GetValue(0);
                }
            }
            return pass;
        }
    }
}
