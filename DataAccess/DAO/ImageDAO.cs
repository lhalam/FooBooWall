using System.Data;
using System.Data.SqlClient;
using DataAccess.Entities;

namespace DataAccess.DAO
{
    public class ImageDAO : AbstractDAO<Image>
    {
        public override void Create(Image entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string sql = "INSERT INTO PmiDatabase.dbo.images(Name, PathToLocalImage)" +
                                   " OUTPUT Inserted.ID " +
                                   "VALUES(@Name, @PathToLocalImage)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 255).Value = entity.Name ?? string.Empty;
                cmd.Parameters.Add("@PathToLocalImage", SqlDbType.VarChar, 255).Value = entity.PathToLocalImage ?? string.Empty;
                cmd.CommandType = CommandType.Text;
                entity.Id = (int)cmd.ExecuteScalar();
            }
        }

        public override Image Read(int id)
        {
            Image image;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string sql = "SELECT * FROM PmiDatabase.dbo.images WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    image = new Image
                    {
                        Id = (int)reader["id"],
                        Name = Convert(reader["Name"]),
                        PathToLocalImage = Convert(reader["PathToLocalImage"])
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
                const string sql = "UPDATE PmiDatabase.dbo.images SET Name = @Name WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@Name", SqlDbType.Text).Value = entity.Name;
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = entity.Id;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public override void Delete(Image entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string sql = "DELETE FROM PmiDatabase.dbo.images WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@ID", SqlDbType.VarChar, 255).Value = entity.Id;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
