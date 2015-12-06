using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Entities;
using System.Data.SqlTypes;

namespace DataAccess.DAO
{
    public class EventCommentsDao : AbstractDAO<Comment>
    {
        public override void Create(Comment entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "INSERT INTO comments(Event_id, Author_id, Comment_time, Comment)" +
                    " OUTPUT Inserted.ID " +
                    "VALUES(@param1, @param2, @param3, @param4)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.Int).Value = entity.EventId;
                cmd.Parameters.Add("@param2", SqlDbType.Int).Value = entity.AuthorId;
                cmd.Parameters.Add("@param3", SqlDbType.Date).Value = entity.WritingDate;
                cmd.Parameters.Add("@param4", SqlDbType.Text).Value = entity.Text ?? SqlString.Null;
                cmd.Parameters.Add("@param5", SqlDbType.Int).Value = entity.Id;
                cmd.CommandType = CommandType.Text;
                entity.Id = (int)cmd.ExecuteScalar();

            }
        }

        public override void Delete(Comment entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "DELETE FROM comments WHERE Id = @param1";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.Int).Value = entity.Id;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public override Comment Read(int id)
        {
            Comment comment = null;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "SELECT * FROM comments WHERE id = @param1";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.Int).Value = id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();

                    comment = new Comment
                    {
                        Id = id,
                        EventId = (int)reader.GetValue(1),
                        AuthorId = (int)reader.GetValue(2),
                        WritingDate = (reader.GetValue(3) is DBNull) ? DateTime.Now : (DateTime)reader[3]
                    };
                }
            }
            return comment;
        }

        public override void Update(Comment entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "UPDATE comments SET Event_id = @param1, Author_id = @param2, Comment_time = @param3," +
                    " Comment = @param4 WHERE Id = @param5";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.Int).Value = entity.EventId;
                cmd.Parameters.Add("@param2", SqlDbType.Int).Value = entity.AuthorId;
                cmd.Parameters.Add("@param3", SqlDbType.Date).Value = entity.WritingDate;
                cmd.Parameters.Add("@param4", SqlDbType.Text).Value = entity.Text ?? SqlString.Null;
                cmd.Parameters.Add("@param5", SqlDbType.Int).Value = entity.Id;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public List<Comment> GetCommentsByEventId(int eventId) 
        {
            var list = new List<Comment>();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string sql = "SELECT * FROM comments";
                SqlCommand cmd = new SqlCommand(sql, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Comment comment = new Comment
                        {
                            Id = (int)reader.GetValue(0),
                            EventId = (int)reader.GetValue(1),
                            AuthorId = (int)reader.GetValue(2),
                            WritingDate = (reader.GetValue(3) is DBNull) ? DateTime.Now : (DateTime)reader[3]
                    
                        };
                        list.Add(comment);
                    }
                }
            }
            return list;
        }
    }
}
