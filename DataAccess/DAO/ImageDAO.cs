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
                const string sql = "INSERT INTO PmiDatabase.dbo.images(Name)" +
                                   "VALUES(@param1)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = entity.Name;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public override Image Read(int id)
        {
            Image image;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string sql = "SELECT * FROM PmiDatabase.dbo.images WHERE id = @param1";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.Int).Value = id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    image = new Image
                    {
                        Id = (int)reader.GetValue(0),
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
                const string sql = "UPDATE PmiDatabase.dbo.images SET Name = @param1 WHERE ID = @param2";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.Text).Value = entity.Name;
                cmd.Parameters.Add("@param7", SqlDbType.Int).Value = entity.Id;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public override void Delete(Image entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string sql = "DELETE FROM PmiDatabase.dbo.images WHERE ID = @param1";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = entity.Id;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
