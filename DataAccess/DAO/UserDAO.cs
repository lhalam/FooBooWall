using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
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
                string sql =  "INSERT INTO users(FirstName, LastName, Login, Email, PasswordHash, BirthDate, Image_id, SecurityStamp, SkypeName)" +
                    " OUTPUT Inserted.ID " +
                    "VALUES(@FirstName, @LastName, @Login, @Email, @PasswordHash, @BirthDate, @Image_id, @SecurityStamp, @SkypeName)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 255).Value = entity.FirstName ?? SqlString.Null;
                cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 255).Value = entity.LastName ?? SqlString.Null;
                cmd.Parameters.Add("@Login", SqlDbType.VarChar, 255).Value = entity.Login;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 255).Value = entity.EMail ?? SqlString.Null;
                cmd.Parameters.Add("@PasswordHash", SqlDbType.VarChar, -1).Value = entity.PasswordHash ?? SqlString.Null;
                cmd.Parameters.Add("@BirthDate", SqlDbType.DateTime).Value = entity.Birthday;
                cmd.Parameters.Add("@Image_id", SqlDbType.Int).Value = entity.ImageId;
                cmd.Parameters.Add("@SecurityStamp", SqlDbType.VarChar, -1).Value = entity.SecurityStamp ?? SqlString.Null;
                cmd.Parameters.Add("@SkypeName", SqlDbType.VarChar, 120).Value = entity.Skype ?? SqlString.Null;
                cmd.CommandType = CommandType.Text;
                entity.Id = (int)cmd.ExecuteScalar();
                
            }
        }
        public override User Read(int id)
        {
            User user;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "SELECT * FROM users WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();

                    user = new User
                    {
                        Id = id,
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
                }
            }
            return user;
        }

        public override void Update(User entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "UPDATE users SET FirstName = @FirstName, LastName = @LastName, Email = @Email," +
                    " PasswordHash = @PasswordHash, BirthDate = @BirthDate, Image_id = @Image_id, " +
                    " SecurityStamp = @SecurityStamp, SkypeName = @SkypeName WHERE Login = @Login";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 255).Value = entity.FirstName ?? SqlString.Null;
                cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 255).Value = entity.LastName ?? SqlString.Null;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 255).Value = entity.EMail ?? SqlString.Null;
                cmd.Parameters.Add("@PasswordHash", SqlDbType.VarChar, -1).Value = entity.PasswordHash ?? SqlString.Null;
                cmd.Parameters.Add("@BirthDate", SqlDbType.Date).Value = entity.Birthday;
                cmd.Parameters.Add("@Image_id", SqlDbType.Int).Value = entity.ImageId;
                cmd.Parameters.Add("@SecurityStamp", SqlDbType.VarChar, -1).Value = entity.SecurityStamp ?? SqlString.Null;
                cmd.Parameters.Add("@SkypeName", SqlDbType.VarChar, 120).Value = entity.Skype ?? SqlString.Null;
                cmd.Parameters.Add("@Login", SqlDbType.VarChar, 255).Value = entity.Login;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public override void Delete(User entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "DELETE FROM users WHERE Login = @Login";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@Login", SqlDbType.VarChar, 255).Value = entity.Login;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public override List<User> ReadAll()
        {
            var result = new List<User>();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string commandString = "SELECT * FROM users";
                SqlCommand cmd = new SqlCommand(commandString, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new User
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
                        });
                    }
                }
            }
            return result;
        }

        public User GetUserByLoginAndPassword(string login, string passwordHash)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "SELECT * FROM users WHERE Login = @Login AND PasswordHash = @PasswordHash";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@Login", SqlDbType.VarChar, 255).Value = login;
                cmd.Parameters.Add("@PasswordHash", SqlDbType.VarChar, -1).Value = passwordHash ?? SqlString.Null;
                User user;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    user = new User
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
                }
                return user;
            }
        }

        public User GetUserByLogin(string login)
        {
            User user = null;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "SELECT * FROM users WHERE Login = @param1";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = login;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();

                        user = new User
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
                    }

                    return user;
                }
            }
        }

        public string GetPasswordByLogin(string login)
        {
            string pass;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "SELECT PasswordHash FROM users WHERE Login = @Login";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@Login", SqlDbType.VarChar, 255).Value = login;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    pass = Convert(reader["PasswordHash"]);
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
                string sql = "SELECT * FROM users WHERE Login = @Login";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@Login", SqlDbType.VarChar, 255).Value = login;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    result = reader.HasRows;                   
                }
            }
            return result;
        }
    }
}
