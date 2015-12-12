using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class MessageDAO : AbstractDAO<Message>
    {
        public override void Create(Message entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "INSERT INTO messages(DateTime, Text, AuthorId, RecipientId)" +
                    " OUTPUT Inserted.ID " +
                    "VALUES(@dateTime, @text, @author, @recipient)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@dateTime", SqlDbType.DateTime).Value = entity.Time;
                cmd.Parameters.Add("@text", SqlDbType.NVarChar, -1).Value = entity.Text;
                cmd.Parameters.Add("@author", SqlDbType.Int).Value = entity.AuthorId;
                cmd.Parameters.Add("@recipient", SqlDbType.Int).Value = entity.RecipientId ?? SqlInt32.Null;
                cmd.CommandType = CommandType.Text;
                entity.Id = (int)cmd.ExecuteScalar();

            }
        }

        public List<Message> GetGlobal()
        {
            var result = new List<Message>();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string commandString = "SELECT * FROM messages WHERE RecipientId IS NULL";
                SqlCommand cmd = new SqlCommand(commandString, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Message
                        {
                            Id = (int)reader["Id"],
                            Text = (string)reader["Text"],
                            AuthorId = (int)reader["AuthorId"],
                            Time = (DateTime)reader["DateTime"]
                        });
                    }
                }
            }
            return result;
        }

        public List<Message> GetAllPrivate(int id, int? amount = null)
        {
            var result = new List<Message>();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string commandString = "SELECT " + (amount.HasValue ? "TOP(@amount)" : String.Empty )
                    + " Id, Text, AuthorId, RecipientId, DateTime FROM messages WHERE RecipientId = @id OR AuthorId = @id" +
                    " ORDER BY DateTime DESC ";
                SqlCommand cmd = new SqlCommand(commandString, connection);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                if (amount.HasValue)
                {
                    cmd.Parameters.Add("@amount", SqlDbType.Int).Value = amount.Value;
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Message
                        {
                            Id = (int)reader[0],
                            Text = (string)reader[1],
                            AuthorId = (int)reader[2],
                            RecipientId = reader.IsDBNull(3) ? (int?)null :(int)reader[3],
                            Time = (DateTime)reader[4]
                        });
                    }
                }
            }
            return result;
        }

        public List<Message> GetChat(int firstUser, int secondUser, int? amount = null)
        {
            var result = new List<Message>();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string commandString = "SELECT " + (amount.HasValue ? "TOP(@amount) " : String.Empty ) +
                   "* FROM messages WHERE (RecipientId = @firstId AND AuthorId = @secondId) " +
                   "OR (RecipientId = @secondId AND AuthorId = @firstId) " +
                   " ORDER BY DateTime ";
                SqlCommand cmd = new SqlCommand(commandString, connection);
                cmd.Parameters.Add("@firstId", SqlDbType.Int).Value = firstUser;
                cmd.Parameters.Add("@secondId", SqlDbType.Int).Value = secondUser;
                if(amount.HasValue)
                {
                    cmd.Parameters.Add("@amount", SqlDbType.Int).Value = amount.Value;
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Message
                        {
                            Id = (int)reader["Id"],
                            Text = (string)reader["Text"],
                            AuthorId = (int)reader["AuthorId"],
                            RecipientId = (int)reader["RecipientId"],
                            Time = (DateTime)reader["DateTime"]
                        });
                    }
                }
            }
            return result;
        }

        public override Message Read(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(Message entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Message entity)
        {
            throw new NotImplementedException();
        }


    }
}
