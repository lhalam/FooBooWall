using System;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Entities;

namespace DataAccess.DAO
{
    public class UserDAO : AbstractDAO<User>
    {
        public override void Create(User entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string sql = "INSERT INTO users(FirstName, LastName, Login, Email, PasswordHash, BirthDate, Image_id, VK_ID, FB_ID, SecurityStamp)" +
                                   "VALUES(@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9, @SecurityStamp)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = entity.FirstName;
                cmd.Parameters.Add("@param2", SqlDbType.VarChar, 255).Value = entity.LastName;
                cmd.Parameters.Add("@param3", SqlDbType.VarChar, 255).Value = entity.Login;
                cmd.Parameters.Add("@param4", SqlDbType.VarChar, 255).Value = entity.EMail;
                cmd.Parameters.Add("@param5", SqlDbType.VarChar, -1).Value = entity.PasswordHash;
                cmd.Parameters.Add("@param6", SqlDbType.DateTime).Value = entity.Birthday;
                cmd.Parameters.Add("@param7", SqlDbType.Int).Value = entity.ImageId;
                cmd.Parameters.Add("@param8", SqlDbType.VarChar, 50).Value = entity.VkId;
                cmd.Parameters.Add("@param9", SqlDbType.VarChar, 50).Value = entity.FbId;
                cmd.Parameters.Add("@SecurityStamp", SqlDbType.VarChar, -1).Value = entity.SecurityStamp;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }
        public override User Read(int id)
        {
            User user;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string sql = "SELECT * FROM users WHERE id = @param1";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.Int).Value = id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    user = new User
                    {
<<<<<<< HEAD
                        FirstName = reader.GetValue(1) is DBNull ? null : (string)reader.GetValue(1),
                        LastName = reader.GetValue(2) is DBNull ? null : (string)reader.GetValue(2),
                        Login = reader.GetValue(3) is DBNull ? null : (string)reader.GetValue(3),
                        EMail = reader.GetValue(4) is DBNull ? null : (string)reader.GetValue(4),
                        PasswordHash = reader.GetValue(5) is DBNull ? null : (string)reader.GetValue(5),
                        Birthday = reader.GetValue(6) is DBNull ? DateTime.Now : (DateTime)reader.GetValue(6),
                        Image_ID = reader.GetValue(7) is DBNull ? 1 : (int)reader.GetValue(7),
                        VK_ID = reader.GetValue(8) is DBNull ? null : (String)reader.GetValue(8),
                        FB_ID = reader.GetValue(9) is DBNull ? null : (String)reader.GetValue(9),
                        SecurityStamp = reader.GetValue(10) is DBNull ? null : (string)reader.GetValue(10)
=======
                        FirstName = (string)reader.GetValue(1),
                        LastName = (string)reader.GetValue(2),
                        Login = (string)reader.GetValue(3),
                        EMail = (string)reader.GetValue(4),
                        PasswordHash = (string)reader.GetValue(5),
                        Birthday = (DateTime)reader.GetValue(6),
                        ImageId = (int)reader.GetValue(7),
                        VkId = (String)reader.GetValue(8),
                        FbId = (String)reader.GetValue(9),
                        SecurityStamp = (string)reader.GetValue(10)
>>>>>>> 318b6ae6d38342be5a6ee09d47601d28ded2f735
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
                const string sql = "UPDATE users SET FirstName = @param1, LastName = @param2, Email = @param3," +
                                   " PasswordHash = @param4, BirthDate = @param5, Image_id = @param6, VK_ID = @param7, FB_ID = @param8, SecurityStamp = @SecurityStamp WHERE Login = @param9";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = entity.FirstName;
                cmd.Parameters.Add("@param2", SqlDbType.VarChar, 255).Value = entity.LastName;
                cmd.Parameters.Add("@param3", SqlDbType.VarChar, 255).Value = entity.EMail;
                cmd.Parameters.Add("@param4", SqlDbType.VarChar, -1).Value = entity.PasswordHash;
                cmd.Parameters.Add("@param5", SqlDbType.Date).Value = entity.Birthday;
                cmd.Parameters.Add("@param6", SqlDbType.Int).Value = entity.ImageId;
                cmd.Parameters.Add("@param7", SqlDbType.VarChar, 50).Value = entity.VkId;
                cmd.Parameters.Add("@param8", SqlDbType.VarChar, 50).Value = entity.FbId;
                cmd.Parameters.Add("@param9", SqlDbType.VarChar, 255).Value = entity.Login;
                cmd.Parameters.Add("@SecurityStamp", SqlDbType.VarChar, -1).Value = entity.SecurityStamp;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public override void Delete(User entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string sql = "DELETE FROM users WHERE Login = @param1";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = entity.Login;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public User GetUserByLoginAndPassword(string login, string passwordHash)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string sql = "SELECT * FROM users WHERE Login = @param1 AND PasswordHash = @param2";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = login;
                cmd.Parameters.Add("@param2", SqlDbType.VarChar, -1).Value = passwordHash;
                User user;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    user = new User
                    {
                        Id = (int)reader.GetValue(0),
                        FirstName = (string)reader.GetValue(1),
                        LastName = (string)reader.GetValue(2),
                        Login = (string)reader.GetValue(3),
                        EMail = (string)reader.GetValue(4),
                        PasswordHash = (string)reader.GetValue(5),
                        Birthday = (DateTime)reader.GetValue(6),
                        ImageId = (int)reader.GetValue(7),
                        VkId = (String)reader.GetValue(8),
                        FbId = (String)reader.GetValue(9),
                        SecurityStamp = (string)reader.GetValue(10)
                    };
                }
                return user;
            }
        }

        public User GetUserByLogin(string login)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string sql = "SELECT * FROM users WHERE Login = @param1";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = login;
                User user;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        user = new User
                        {
                            Id = (int)reader.GetValue(0),
                            FirstName = (string)reader.GetValue(1),
                            LastName = (string)reader.GetValue(2),
                            Login = (string)reader.GetValue(3),
                            EMail = (string)reader.GetValue(4),
                            PasswordHash = (string)reader.GetValue(5),
                            Birthday = (DateTime)reader.GetValue(6),
                            ImageId = (int)reader.GetValue(7),
                            VkId = (String)reader.GetValue(8),
                            FbId = (String)reader.GetValue(9),
                            SecurityStamp = (string)reader.GetValue(10)
                        };
                    }
                    else
                    {
                        return new User();
                    }

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
                const string sql = "SELECT PasswordHash FROM users WHERE login = @param1";
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

        public bool UserWithSpecifiedLoginExists(string login)
        {
            bool result;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string sql = "SELECT * FROM users WHERE login = @param1";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = login;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    result = reader.HasRows;
                }
            }
            return result;

        }
    }
}
