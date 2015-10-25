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
                string sql =  "INSERT INTO PmiDatabase.dbo.users(FirstName, LastName, Login, Email, Password, Age, Image_Id, VK_ID, FB_ID)" +
                    "VALUES(@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9)";
                SqlCommand cmd = new SqlCommand(sql,connection);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = entity.FirstName;
                cmd.Parameters.Add("@param2", SqlDbType.VarChar, 255).Value = entity.LastName;
                cmd.Parameters.Add("@param3", SqlDbType.VarChar, 255).Value = entity.Login;
                cmd.Parameters.Add("@param4", SqlDbType.VarChar, 255).Value = entity.EMail;
                cmd.Parameters.Add("@param5", SqlDbType.VarChar, 20).Value = entity.Password;
                // TODO: change when entity or table will be fixed.
                cmd.Parameters.Add("@param6", SqlDbType.Int).Value = 42;
                cmd.Parameters.Add("@param7", SqlDbType.Int).Value = entity.Image_ID;
                cmd.Parameters.Add("@param8", SqlDbType.Int).Value = 42;
                cmd.Parameters.Add("@param9", SqlDbType.Int).Value = 42;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }
        public override User Read(int id)
        {
            return new User();
        }
        public override void Update(User entity)
        {

        }
        public override void Delete(User entity)
        {

        }
    }
}
