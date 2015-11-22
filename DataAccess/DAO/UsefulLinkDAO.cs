using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

namespace DataAccess.DAO
{
    public class UsefulLinkDAO : AbstractDAO<UsefulLink>
    {
        public override void Create(UsefulLink entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql =
                    "INSERT INTO usefulLinks(OwnerUserId, Url, ImageId, Comment)" +
                    " OUTPUT Inserted.ID " +
                    "VALUES(@param1, @param2, @param3, @param4)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add(new SqlParameter("@param1", entity.OwnerUserID));
                cmd.Parameters.Add(new SqlParameter("@param2", entity.Url));
                cmd.Parameters.Add(new SqlParameter("@param3", entity.ImageId));
                cmd.Parameters.Add(new SqlParameter("@param4", Convert(entity.Comment)));
                cmd.CommandType = CommandType.Text;
                entity.Id = (int) cmd.ExecuteScalar();

            }
        }

        public override UsefulLink Read(int id)
        {
            UsefulLink usefulLink = null;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "SELECT * FROM usefulLinks WHERE Id = @param1";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add(new SqlParameter("@param1", id));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    usefulLink = new UsefulLink
                    {
                        Id = id,
                        OwnerUserID = (int) reader.GetValue(1),
                        Url = (string) reader.GetValue(2),
                        ImageId = (int) reader.GetValue(3),
                        Comment = (string) reader.GetValue(4)
                    };
                }
            }
            return usefulLink;
        }

        public override void Update(UsefulLink entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "UPDATE usefulLinks SET OwnerUserID = @param1, Url = @param2, ImageId = @param3," +
                             " Comment = @param4 WHERE Id = @param5";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add(new SqlParameter("@param1", entity.OwnerUserID));
                cmd.Parameters.Add(new SqlParameter("@param2", entity.Url));
                cmd.Parameters.Add(new SqlParameter("@param3", entity.ImageId));
                cmd.Parameters.Add(new SqlParameter("@param4", Convert(entity.Comment)));
                cmd.Parameters.Add(new SqlParameter("@param5", entity.Id));
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public override void Delete(UsefulLink entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "DELETE FROM usefulLinks WHERE Id = @param1";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add(new SqlParameter("@param1", entity.Id));
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
