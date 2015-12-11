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
                    "VALUES(@User_Id, @Url, @Image_Id, @Comment, @Name)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add(new SqlParameter("@User_Id", entity.OwnerUserID));
                cmd.Parameters.Add(new SqlParameter("@Url", entity.Url ?? SqlString.Null));
                cmd.Parameters.Add(new SqlParameter("@Image_Id", entity.ImageId ?? SqlInt32.Null));
                cmd.Parameters.Add(new SqlParameter("@Comment", entity.Comment ?? SqlString.Null));
                cmd.Parameters.Add(new SqlParameter("@Name", entity.Name ?? SqlString.Null));
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
                string sql = "SELECT * FROM usefulLinks WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    usefulLink = new UsefulLink
                    {
                        Id = id,
                        Name = Convert(reader["Name"]),
                        OwnerUserID = (int) reader["User_Id"],
                        Url = Convert(reader["Url"]),
                        ImageId = (int) reader["Image_Id"],
                        Comment = Convert(reader["Comment"])
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
                string sql = "UPDATE usefulLinks SET OwnerUserID = @OwnerUserID, Url = @Url, ImageId = @ImageId," +
                             " Comment = @Comment, Name = @Name WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add(new SqlParameter("@OwnerUserID", entity.OwnerUserID));
                cmd.Parameters.Add(new SqlParameter("@Url", entity.Url));
                cmd.Parameters.Add(new SqlParameter("@ImageId", entity.ImageId));
                cmd.Parameters.Add(new SqlParameter("@Comment", Convert(entity.Comment)));
                cmd.Parameters.Add(new SqlParameter("@Name", entity.Name));
                cmd.Parameters.Add(new SqlParameter("@Id", entity.Id));
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public override void Delete(UsefulLink entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "DELETE FROM usefulLinks WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add(new SqlParameter("@Id", entity.Id));
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
                            Id = (int)reader["Id"],
                            Name = Convert(reader["Name"]),
                            OwnerUserID = (int)reader["User_Id"],
                            Url = Convert(reader["Url"]),
                            ImageId = (int)reader["Image_Id"],
                            Comment = Convert(reader["Comment"])
                        };
                        result.Add(usefulLink);
                    }
                }
            }
            return result;
        }
    }
}
