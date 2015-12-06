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
    public class VerificationDAO:AbstractDAO<VerificationLetter>
    {
        public override void Create(VerificationLetter entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                int code = int.Parse(entity.Code);
                string email = entity.Email;
                string sql = "DELETE FROM Verification; INSERT INTO Verification(Id,Code,Email,Verified) "+
                    "VALUES(1,"  + code  + "," + "'" + email + "'" + "," +"0)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                //cmd.Parameters.Add("@param1", SqlDbType.Int).Value = entity.Code ?? SqlString.Null;
                //cmd.Parameters.Add("@param2", SqlDbType.VarChar, 255).Value = entity.Email ?? SqlString.Null;
                cmd.CommandType = CommandType.Text;
                //entity.Id = (int)cmd.ExecuteScalar();
                cmd.ExecuteNonQuery();
            }
        }
        public override VerificationLetter  Read(int id)
        {
            VerificationLetter vlt = null;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "SELECT * FROM Verification WHERE id = @param1";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.Int).Value = id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();

                   vlt = new VerificationLetter
                    {
                        Id = id,
                        Code = Convert(reader.GetValue(1).ToString()),
                        Email = Convert(reader.GetValue(2)),
                        Verified = (bool)(reader.GetValue(3)),
                    };
                }
            }
            return vlt;
        }
        public override void Update(VerificationLetter entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string sql = "UPDATE Verification SET Verified = 1";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }
        public override void Delete(VerificationLetter entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "DELETE FROM Verification WHERE Email = @param1";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = entity.Email;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
