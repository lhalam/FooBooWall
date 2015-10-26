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
    public class ImageDAO: AbstractDAO<Image>
    {
        public override void Create(Image entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "INSERT INTO PmiDatabase.dbo.images(Name)" +
                    "VALUES(@param1)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = entity.Name;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public override Image Read(int id)
        {
            Image image = null;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "SELECT * FROM PmiDatabase.dbo.images WHERE id = @param1";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.Int).Value = id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    image = new Image
                    {
                        ID = (int)reader.GetValue(0),
                        Name = (string)reader.GetValue(1)                      
                    };
                }
            }
            return image;
        }

        public override void Update(Image entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "UPDATE PmiDatabase.dbo.images SET Name = @param1 WHERE ID = @param2";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.Text).Value = entity.Name;
                cmd.Parameters.Add("@param7", SqlDbType.Int).Value = entity.ID;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public override void Delete(Image entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "DELETE FROM PmiDatabase.dbo.images WHERE ID = @param1";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = entity.ID;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
