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
                    "INSERT INTO usefulLinks(User_Id, Url, Image_Id, Comment, Name)" +
                    " OUTPUT Inserted.Id " +
                    "VALUES(@param1, @param2, @param3, @param4, @param5)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add(new SqlParameter("@param1", entity.OwnerUserID));
                cmd.Parameters.Add(new SqlParameter("@param2", entity.Url ?? SqlString.Null));
                cmd.Parameters.Add(new SqlParameter("@param3", entity.ImageId ?? SqlInt32.Null));
                cmd.Parameters.Add(new SqlParameter("@param4", entity.Comment ?? SqlString.Null));
                cmd.Parameters.Add(new SqlParameter("@param5", entity.Name ?? SqlString.Null));
                cmd.CommandType = CommandType.Text;
                entity.Id = (int) cmd.ExecuteScalar();
                return;
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
                        Name = (string) reader.GetValue(1),
                        OwnerUserID = (int) reader.GetValue(2),
                        Url = (string) reader.GetValue(3),
                        ImageId = (int) reader.GetValue(4),
                        Comment = (string) reader.GetValue(5)
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
                             " Comment = @param4, Name = @param5 WHERE Id = @param6";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add(new SqlParameter("@param1", entity.OwnerUserID));
                cmd.Parameters.Add(new SqlParameter("@param2", entity.Url));
                cmd.Parameters.Add(new SqlParameter("@param3", entity.ImageId));
                cmd.Parameters.Add(new SqlParameter("@param4", Convert(entity.Comment)));
                cmd.Parameters.Add(new SqlParameter("@param5", entity.Name));
                cmd.Parameters.Add(new SqlParameter("@param6", entity.Id));
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

        public override List<UsefulLink> ReadAll()
        {
            var result = new List<UsefulLink>();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string commandString = "SELECT * FROM usefulLinks";
                SqlCommand cmd = new SqlCommand(commandString, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UsefulLink usefulLink = new UsefulLink
                        {
                            Id = (int)reader.GetValue(0),
                            Name = Convert(reader.GetValue(1)),
                            OwnerUserID = (int)reader.GetValue(2),
                            Url = Convert(reader.GetValue(3)),
                            ImageId = (int)reader.GetValue(4),
                            Comment = Convert(reader.GetValue(5))
                        };
                        result.Add(usefulLink);
                    }
                }
            }
            return result;
        }
    }
}
